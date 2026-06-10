using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Models
{
    [Table("tb_usuario")]
    public class UsuarioModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("nome")]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("senha")]
        [MaxLength(255)]
        public string Senha { get; set; } = string.Empty;

        [Required]
        [Column("funcao")]
        [MaxLength(50)]
        public string Funcao { get; set; } = string.Empty;
    }
}
