using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Models
{
    [Table("tb_catador")]
    public class CatadorModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("capacidade_volume_total")]
        public float CapacidadeVolumeTotal { get; set; }

        public ICollection<CatadorItemModel> CatadorItens { get; set; } = new List<CatadorItemModel>();

        public ICollection<ColetaModel> Coletas { get; set; } = new List<ColetaModel>();
    }
}
