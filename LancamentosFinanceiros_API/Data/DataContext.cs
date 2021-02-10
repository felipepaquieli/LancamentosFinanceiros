using LancamentosFinanceiros.Models;
using Microsoft.EntityFrameworkCore;

namespace LancamentosFinanceiros.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<LancamentoFinanceiro> LancamentosFinanceiros { get; set; }
        public DbSet<BalancoDia> BalancosDia { get; set; }
    }
}