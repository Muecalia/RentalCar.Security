namespace RentalCar.Security.Application.Utils
{
    public class MensagemError
    {
        //NOTFOUND
        public static string NotFound(string entidade)
        {
            return $"{entidade} não encontrada";
        }
        public static string NotFound(string entidade, string key)
        {
            return $"{entidade} com a chave {key} não encontrada";
        }
        public static string NotFoundEmail(string entidade, string email)
        {
            return $"{entidade} com o e-mail {email} não encontrado";
        }
        //public static string NotFound(string entidade, string mensagem)
        //{
        //    return $"{entidade} não encontrada. Mensagem: {mensagem}";
        //}

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
        public static string ConflitoStock(int QtdMl)
        {
            return $"Não é possível eliminar, porque ainda existe {QtdMl} Ml de sangue em stock";
        }
        public static string ConflitoStock()
        {
            return $"Não é possível eliminar, porque ainda existe de sangue em stock sufiente para reposição";
        }
        public static string ConflitoEmail(string email)
        {
            return $"Erro! Email {email} já se encontra registado";
        }
        public static string ConflitoDoacao(string gender, int days)
        {
            return $"Não é possível fazer a doação, {gender} só pode doar depois de {days} dias";
        }
        public static string ConflitoMenorIdade()
        {
            return $"Não é possível fazer a doação, o doador é menor de idade";
        }


        //SEGURANÇA
        public static string SenhaErrada()
        {
            return $"Erro! Senha errada";
        }
        public static string SenhaErrada(string name)
        {
            return $"Erro! Senha do user {name} errada";
        }
        public static string TokenErro()
        {
            return $"Erro ao gerar o token, tente novamente";
        }
        public static string TokenErro(string name)
        {
            return $"Erro ao gerar o token do user {name}";
        }
    }
}
