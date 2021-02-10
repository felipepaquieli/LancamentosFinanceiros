using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancamentosFinanceiros.Models
{
    [Table("LANCAMENTO_FINANCEIRO")]
    public class LancamentoFinanceiro
    {
        [Key]
        [Column("ID_LANCAMENTO")]
        public int IdLancamento { get; set; }
        
        [Column("DATA_LANCAMENTO")]
        public DateTime? DataLancamento { get; set; }
        
        [Column("VALOR")]
        public decimal? Valor { get; set; }
        
        [Column("TIPO_LANCAMENTO")]
        public string TipoLancamento { get; set; }
        
        [Column("STATUS_LANCAMENTO")]
        public string StatusLancamento { get; set; }
        
        [Column("ID_BALANCO")]
        public int? IdBalanco { get; set; }
    }
}