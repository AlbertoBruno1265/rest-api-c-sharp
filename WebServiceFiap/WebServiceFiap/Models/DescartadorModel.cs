using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Models
{
    [Table("tb_descartador")]
    public class DescartadorModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("endereco")]
        [MaxLength(255)]
        public string Endereco { get; set; } = string.Empty;

        public ICollection<ColetaModel> Coletas { get; set; } = new List<ColetaModel>();
    }
}
