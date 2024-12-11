namespace RentalCar.Security.Infrastructure.MessageBus;

public class RabbitMqConfig
{
    public string HostName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}