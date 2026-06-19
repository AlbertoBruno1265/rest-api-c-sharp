namespace WebServiceFiap.ViewModels.Response
{
    /// <summary>
    /// DTO de saída para um Catador.
    /// Não expõe as coleções de navegação do EF Core.
    /// </summary>
    public class CatadorResponse
    {
        public long Id { get; set; }
        public float CapacidadeVolumeTotal { get; set; }
    }
}