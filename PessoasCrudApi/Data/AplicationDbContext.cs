using Microsoft.EntityFrameworkCore;
using PessoasCrudApi.Models.Entities;

namespace PessoasCrudApi.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

    }
}
