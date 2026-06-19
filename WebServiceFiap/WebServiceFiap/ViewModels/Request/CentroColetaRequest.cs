using System.ComponentModel.DataAnnotations;
 
namespace WebServiceFiap.ViewModels.Request
{
    /// <summary>
    /// DTO de entrada para criar ou atualizar um Centro de Coleta.
    /// Valida que o volume atual não pode superar o total.
    /// </summary>
    public class CentroColetaRequest : IValidatableObject
    {
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O endereço não pode ultrapassar 255 caracteres.")]
        public string Endereco { get; set; } = string.Empty;
 
        [Range(0.001, 1_000_000.0, ErrorMessage = "O volume total deve ser maior que zero.")]
        public float VolumeItensTotal { get; set; }
 
        [Range(0.0, 1_000_000.0, ErrorMessage = "O volume atual não pode ser negativo.")]
        public float VolumeItensAtual { get; set; }
 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (VolumeItensAtual > VolumeItensTotal)
                yield return new ValidationResult(
                    "O volume atual não pode superar a capacidade total.",
                    new[] { nameof(VolumeItensAtual) });
        }
    }
}