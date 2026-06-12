using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Models
{
    [Table("tb_centros_coleta")]
    public class CentroColetaModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("endereco")]
        [MaxLength(255)]
        public string Endereco { get; set; } = string.Empty;

        [Column("volume_itens_total")]
        public float VolumeItensTotal { get; set; }

        [Column("volume_itens_atual")]
        public float VolumeItensAtual { get; set; }

        public ICollection<ColetaModel> Coletas { get; set; } = new List<ColetaModel>();
    }
}
