using Projeto_FourGold.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_FourGold.Models
{
    [Table("Tbl_Conta")]
    public abstract class Conta
    {
        [Column("Id")]
        public int ContaId { get; set; }

        public string Numero { get; set; }

        public decimal Saldo { get; set; }

        [Required]
        public TipoConta Tipo { get; set; }

        //1:N
        public ICollection<ChavePix> ChavesPix { get; set; }

        public ICollection<Transacao> Transacoes { get; set; }

        public abstract void Transferir(decimal quantia);

        public abstract void Depositar(decimal quantia);
    }

    public enum TipoConta
    {
        Corrente, Poupanca
    }
}
