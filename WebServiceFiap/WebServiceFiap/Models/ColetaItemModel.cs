using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Models
{
    [Table("tb_coleta_itens")]
    public class ColetaItemModel
    {
        [Column("id_coleta")]
        public int IdColeta { get; set; }

        [Column("id_item")]
        public int IdItem { get; set; }

        [ForeignKey(nameof(IdColeta))]
        public ColetaItemModel? Coleta { get; set; }

        [ForeignKey(nameof(IdItem))]
        public ItemModel? Item { get; set; }
    }
}
