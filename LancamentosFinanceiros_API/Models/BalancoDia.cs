using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancamentosFinanceiros.Models
{
    [Table("BALANCO_DIA")]
    public class BalancoDia
    {
        [Key]
        [Column("ID_BALANCO")]
        public int IdBalanco { get; set; }
        
        [Column("DATA_BALANCO")]
        public DateTime DataBalanco { get; set; }
        
        [Column("VALOR_CREDITO")]
        public decimal ValorTotalCredito { get; set; }
        
        [Column("VALOR_DEBITO")]
        public decimal ValorTotalDebito { get; set; }
        
        [Column("VALOR_SALDO")]
        public decimal ValorSaldo { get; set; }
    }
}