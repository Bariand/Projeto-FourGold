using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projeto_FourGold.Areas.Identity.Data;
using Projeto_FourGold.Models;

namespace Projeto_FourGold.Areas.Identity.Data;

public class FourGoldContext : IdentityDbContext<Cliente>
{
    public DbSet<Cliente> Clientes { get; set; }

    public DbSet<ContaPoupanca> ContasPoupanca { get; set; }

    public DbSet<ContaCorrente> ContasCorrente { get; set; }

    public DbSet<ChavePix> ChavesPix { get; set; }

    public DbSet<Transacao> Transacoes { get; set; }


    public FourGoldContext(DbContextOptions<FourGoldContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        
    }
}
