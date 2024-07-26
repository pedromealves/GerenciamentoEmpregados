using GerenciamentoEmpregadosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoEmpregadosAPI.Data
{
    public class AppDbContext : DbContext
    {
        // Employees
        public DbSet<Employee> Employees { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Database.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
