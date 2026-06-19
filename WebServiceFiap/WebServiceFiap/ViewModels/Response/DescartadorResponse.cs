namespace WebServiceFiap.ViewModels.Response
{
    /// <summary>
    /// DTO de saída para um Descartador.
    /// </summary>
    public class DescartadorResponse
    {
        public long Id { get; set; }
        public string Endereco { get; set; } = string.Empty;
    }
}