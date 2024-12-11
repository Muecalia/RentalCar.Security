using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentalCar.Security.Application.Commands.Request.Account;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Infrastructure.MessageBus;

namespace RentalCar.Security.Application.Services;

public class AccountConsumeService : BackgroundService
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly ILoggerService _loggerService;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private RabbitMqConfig _rabbitMqConfig { get; }

    public AccountConsumeService(IRabbitMqService rabbitMqService, IOptions<RabbitMqConfig> options, ILoggerService loggerService, IServiceScopeFactory serviceScopeFactory)
    {
        _rabbitMqService = rabbitMqService;
        _loggerService = loggerService;
        _serviceScopeFactory = serviceScopeFactory;
        _rabbitMqConfig = options.Value;
    }

    protected async override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await CreateAccountConsume(cancellationToken);
            
            //System.Diagnostics.Debug.Print("AccountService -> Executando serviço em segundo plano.");
            Console.WriteLine("AccountService -> Executando serviço em segundo plano.");
            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        }
    }

    private async Task CreateAccountConsume(CancellationToken cancellationToken)
    {
        const string title = "New Account Consume";

        using var connection = await _rabbitMqService.CreateConnection(cancellationToken);
        using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        try
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            
            //Aceder a fila
            await channel.QueueDeclareAsync(RabbitQueue.NEW_ACCOUNT_QUEUE, true, false, false, null, cancellationToken: cancellationToken);

            //Garantir que seja enviado ao consumidor uma mensagem em cada processamento
            await channel.BasicQosAsync(prefetchCount: 1, prefetchSize: 0, global: false, cancellationToken: cancellationToken);

            //Definição do consumo das mensagens recebidas
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                //Converter a message para o tipo de retorno
                var account = JsonSerializer.Deserialize<CreateAccountRequest>(message);
                
                await mediator.Send(account, cancellationToken);

                // Confirmando que a mensagem foi processada com sucesso
                await channel.BasicAckAsync(ea.DeliveryTag, false, cancellationToken);

                //Aguardar o processamento da mensagem
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            };

            //Iniciar o consumo de mensagens numa file
            await channel.BasicConsumeAsync(queue: RabbitQueue.NEW_ACCOUNT_QUEUE, autoAck: true, consumer: consumer, cancellationToken);
        }
        catch (Exception ex)
        {
            _loggerService.LogError(MessageError.ConsumirMensagemErro(title, ex.Message));
            throw;
        }
        finally
        {
            await _rabbitMqService.CloseConnection(connection, channel, cancellationToken);
        }
        
    }
    
}