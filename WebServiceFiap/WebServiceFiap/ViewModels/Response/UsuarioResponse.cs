namespace WebServiceFiap.ViewModels.Response
{
    /// <summary>
    /// DTO de saída para um Usuário.
    /// NUNCA inclui a senha (nem o hash). Segurança obrigatória.
    /// </summary>
    public class UsuarioResponse
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty;
    }
}