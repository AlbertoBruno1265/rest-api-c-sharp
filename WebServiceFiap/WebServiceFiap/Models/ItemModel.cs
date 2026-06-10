using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Models
{

    [Table("tb_itens")]
    public class ItemModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nome")]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Column("volume")]
        public float Volume { get; set; }

        public ICollection<ColetaItemModel> ColetaItens { get; set; } = new List<ColetaItemModel>();

        public ICollection<CatadorItemModel> CatadorItens { get; set; } = new List<CatadorItemModel>();
    }
}
