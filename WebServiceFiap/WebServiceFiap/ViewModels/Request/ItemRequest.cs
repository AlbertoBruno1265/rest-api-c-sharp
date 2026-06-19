using System.ComponentModel.DataAnnotations;
 
namespace WebServiceFiap.ViewModels.Request
{
    /// <summary>
    /// DTO de entrada para criar ou atualizar um Item reciclável.
    /// Valida nome obrigatório e volume positivo.
    /// </summary>
    public class ItemRequest
    {
        [Required(ErrorMessage = "O nome do item é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode ultrapassar 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
 
        [Range(0.001, double.MaxValue, ErrorMessage = "O volume deve ser maior que zero.")]
        public float Volume { get; set; }
    }
}