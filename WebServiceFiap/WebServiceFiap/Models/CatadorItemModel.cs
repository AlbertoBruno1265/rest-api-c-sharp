using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Models
{
    [Table("tb_catador_item")]
    public class CatadorItemModel
    {
        [Column("id_catador")]
        public int IdCatador { get; set; }

        [Column("id_item")]
        public int IdItem { get; set; }

        [Column("foi_entregue")]
        public bool FoiEntregue { get; set; }

        [ForeignKey(nameof(IdCatador))]
        public CatadorModel? Catador { get; set; }

        [ForeignKey(nameof(IdItem))]
        public ItemModel? Item { get; set; }
    }
}
