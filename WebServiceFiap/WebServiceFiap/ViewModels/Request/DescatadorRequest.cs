using System.ComponentModel.DataAnnotations;
 
namespace WebServiceFiap.ViewModels.Request
{
    /// <summary>
    /// DTO de entrada para criar ou atualizar um Descartador (pessoa/empresa que descarta resíduos).
    /// </summary>
    public class DescartadorRequest
    {
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O endereço não pode ultrapassar 255 caracteres.")]
        public string Endereco { get; set; } = string.Empty;
    }
}