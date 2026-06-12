using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Models
{
    [Table("tb_coletas")]
    public class ColetaModel
    {
        [Key]
        [Column("id_coleta")]
        public long IdColeta { get; set; }

        [Column("data")]
        public DateTime Data { get; set; }

        [Column("id_catador")]
        public long IdCatador { get; set; }

        [Column("id_descartador")]
        public long IdDescartador { get; set; }

        [Column("id_centro")]
        public long IdCentro { get; set; }

        [Column("foi_finalizada")]
        public bool FoiFinalizada { get; set; }

        [ForeignKey(nameof(IdCatador))]
        public CatadorModel? Catador { get; set; }

        [ForeignKey(nameof(IdDescartador))]
        public DescartadorModel? Descartador { get; set; }

        [ForeignKey(nameof(IdCentro))]
        public CentroColetaModel? CentroColeta { get; set; }

        public ICollection<ColetaItemModel> ColetaItens { get; set; } = new List<ColetaItemModel>();
    }
}
