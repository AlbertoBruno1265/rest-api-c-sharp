using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Models
{
    [Table("tb_coleta_itens")]
    public class ColetaItemModel
    {
        [Column("id_coleta")]
        public long IdColeta { get; set; }

        [Column("id_item")]
        public long IdItem { get; set; }

        [ForeignKey(nameof(IdColeta))]
        public ColetaModel? Coleta { get; set; }

        [ForeignKey(nameof(IdItem))]
        public ItemModel? Item { get; set; }
    }
}
