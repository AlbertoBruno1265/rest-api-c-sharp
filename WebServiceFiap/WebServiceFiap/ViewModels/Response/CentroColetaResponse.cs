namespace WebServiceFiap.ViewModels.Response
{
    /// <summary>
    /// DTO de saída para um Centro de Coleta.
    /// Inclui PercentualOcupacao calculado para facilitar dashboards e alertas.
    /// </summary>
    public class CentroColetaResponse
    {
        public long Id { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public float VolumeItensTotal { get; set; }
        public float VolumeItensAtual { get; set; }
 
        /// <summary>
        /// Percentual de ocupação (0–100). Útil para alertas automáticos
        /// quando o centro está próximo da capacidade máxima (ESG tema 2).
        /// </summary>
        public float PercentualOcupacao =>
            VolumeItensTotal > 0
                ? (float)Math.Round(VolumeItensAtual / VolumeItensTotal * 100, 2)
                : 0f;
    }
}