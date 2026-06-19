namespace WebServiceFiap.ViewModels.Response
{
    /// <summary>
    /// DTO de saída para um Item reciclável.
    /// Não expõe navegações de EF Core (ColetaItens, CatadorItens).
    /// </summary>
    public class ItemResponse
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public float Volume { get; set; }
    }
}