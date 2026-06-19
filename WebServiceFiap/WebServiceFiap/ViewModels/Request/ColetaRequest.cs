using System.ComponentModel.DataAnnotations;
 
namespace WebServiceFiap.ViewModels.Request
{
    /// <summary>
    /// DTO de entrada para registrar uma nova Coleta de resíduos.
    /// </summary>
    public class ColetaRequest
    {
        [Required(ErrorMessage = "A data da coleta é obrigatória.")]
        public DateTime Data { get; set; }
 
        [Range(1, long.MaxValue, ErrorMessage = "O ID do catador deve ser válido.")]
        public long IdCatador { get; set; }
 
        [Range(1, long.MaxValue, ErrorMessage = "O ID do descartador deve ser válido.")]
        public long IdDescartador { get; set; }
 
        [Range(1, long.MaxValue, ErrorMessage = "O ID do centro de coleta deve ser válido.")]
        public long IdCentro { get; set; }
 
        public bool FoiFinalizada { get; set; } = false;
    }
}