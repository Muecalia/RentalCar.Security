namespace RentalCar.Security.Core.Configs;

public class MessageError
{
    //NOTFOUND
    public static string NotFound(string entidade)
    {
        return $"{entidade} não encontrado(a)";
    }
    public static string NotFound(string entidade, string key)
    {
        return $"{entidade} com a chave {key} não encontrada";
    }
    public static string NotFoundEmail(string entidade, string email)
    {
        return $"{entidade} com o e-mail {email} não encontrado";
    }

    //LISTAR
    public static string CarregamentoSucesso(string entidade)
    {
        return $"Dados do(a) {entidade} carregado com sucesso";
    }
    public static string CarregamentoSucesso(string entidade, int size)
    {
        return $"Dados do(a) {entidade} carregados com sucesso. Total Itens: {size}";
    }
    public static string CarregamentoErro(string entidade)
    {
        return $"Erro ao carregar os dados do(a) {entidade}";
    }
    public static string CarregamentoErro(string entidade, string mensagem)
    {
        return $"Erro ao carregar os dados do(a) {entidade}. Mensagem: {mensagem}";
    }

    //OPERAÇÃO
    public static string OperacaoSucesso(string entidade, string operacao)
    {
        return $"Sucesso ao {operacao} o(a) {entidade}";
    }
    public static string OperacaoErro(string entidade, string operacao)
    {
        return $"Erro ao {operacao} o(a) {entidade}";
    }
    public static string OperacaoErro(string entidade, string operacao, string mensagem)
    {
        return $"Erro ao {operacao} o(a) {entidade}. Mensagem: {mensagem}";
    }

    //CONFLICTO
    public static string Conflito(string entidade)
    {
        return $"Já existe um(a) {entidade} com este nome";
    }
    public static string ConflitoUso(string entidade)
    {
        return $"Não é possível eliminar, porque a {entidade} já se encontra em uso";
    }
    public static string ConflitoEmail(string email)
    {
        return $"Erro! Email {email} já se encontra registado";
    }
    
    //SEGURANÇA
    public static string SenhaErrada()
    {
        return "Erro! Senha errada";
    }
    public static string SenhaErrada(string name)
    {
        return $"Erro! Senha do user {name} errada";
    }
    public static string TokenErro()
    {
        return "Erro ao gerar o token, tente novamente";
    }
    public static string TokenErro(string name)
    {
        return $"Erro ao gerar o token do user {name}";
    }
    
    // CONSUMIR MENSAGEM
    public static string ConsumirMensagemSucesso(string titulo)
    {
        return $"Suceso ao consumir a mensagem do (a) {titulo}.";
    }
    public static string ConsumirMensagemErro(string titulo, string message)
    {
        return $"Erro ao consumir a mensagem do(a) {titulo}. Mensagem: {message}";
    }

    // PUBLICAR MENSAGEM
    public static string PublicarMensagemSucesso(string titulo)
    {
        return $"Suceso ao publicar a mensagem do (a) {titulo}.";
    }
    public static string PublicarMensagemErro(string titulo, string message)
    {
        return $"Erro ao publicar a mensagem do(a) {titulo}. Mensagem: {message}";
    }

    // CONEXÃO
    public static string AbrirConexão(string titulo)
    {
        return $"Suceso ao abrir a conexão com o servidor rabbitmq";
    }
    public static string FecharConexão(string titulo)
    {
        return $"Sucesso ao fechar do(a) {titulo} a conexão e o canal com o servidor rabbitmq.";
    }
    public static string FecharConexãoErro(string message)
    {
        return $"Erro ao fechar a conexão e o canal com o servidor rabbitmq. Mensagem: {message}";
    }
}