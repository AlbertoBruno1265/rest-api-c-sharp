using System.ComponentModel.DataAnnotations;
 
namespace WebServiceFiap.ViewModels.Request
{
    /// <summary>
    /// DTO de entrada para criar ou atualizar um Catador.
    /// </summary>
    public class CatadorRequest
    {
        [Range(0.001, 10000.0, ErrorMessage = "A capacidade de volume deve ser maior que zero e no máximo 10.000 m³.")]
        public float CapacidadeVolumeTotal { get; set; }
    }
}