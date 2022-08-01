using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_FourGold.Models
{
    [Table("Transacoes")]
    public class Transacao
    {
        [Key]
        public int TransacaoId { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        [Display(Name = "Data da transação")]
        public DateTime DataTransacao { get; set; }
        [Required]
        public TipoTransacao Tipo { get; set; }

        // 1 : n
        public int ContaId { get; set; }
        public Conta Conta { get; set; }
    }

    public enum TipoTransacao
    {
        [Display(Name = "Depósito")]
        Deposito, 
        [Display(Name = "Transferência")]
        Transferencia
    }
}
