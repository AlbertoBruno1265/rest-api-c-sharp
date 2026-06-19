namespace WebServiceFiap.ViewModels.Response
{
    /// <summary>
    /// DTO de saída para uma Coleta.
    /// Expõe apenas os IDs das entidades relacionadas para evitar ciclos de serialização.
    /// </summary>
    public class ColetaResponse
    {
        public long IdColeta { get; set; }
        public DateTime Data { get; set; }
        public long IdCatador { get; set; }
        public long IdDescartador { get; set; }
        public long IdCentro { get; set; }
        public bool FoiFinalizada { get; set; }
    }
}