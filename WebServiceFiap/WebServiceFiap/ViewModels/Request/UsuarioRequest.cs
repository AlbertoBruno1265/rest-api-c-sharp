using System.ComponentModel.DataAnnotations;
 
namespace WebServiceFiap.ViewModels.Request
{
    /// <summary>
    /// DTO de entrada para criação de um novo Usuário.
    /// A senha será armazenada com hash — nunca em texto plano.
    /// </summary>
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode ultrapassar 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
 
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [MaxLength(150, ErrorMessage = "O e-mail não pode ultrapassar 150 caracteres.")]
        public string Email { get; set; } = string.Empty;
 
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [MaxLength(255)]
        public string Senha { get; set; } = string.Empty;
 
        [Required(ErrorMessage = "A função é obrigatória.")]
        [RegularExpression("^(Admin|Operador|Visualizador)$",
            ErrorMessage = "Função inválida. Use: Admin, Operador ou Visualizador.")]
        public string Funcao { get; set; } = string.Empty;
    }
 
    /// <summary>
    /// DTO para atualizar apenas os dados pessoais sem alterar a senha.
    /// </summary>
    public class UsuarioUpdateRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
 
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;
 
        [Required]
        [RegularExpression("^(Admin|Operador|Visualizador)$",
            ErrorMessage = "Função inválida. Use: Admin, Operador ou Visualizador.")]
        public string Funcao { get; set; } = string.Empty;
    }
}