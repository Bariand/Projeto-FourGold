namespace Projeto_FourGold.Models
{
    public class ContaPoupanca : Conta
    {
        public decimal TaxaRendimento { get; set; } = 2;


        public void AcrecentarRendimento()
        {
            Saldo += TaxaRendimento;
        }

        public override void Transferir(decimal quantia)
        {
            Saldo -= quantia;
        }

        public override void Depositar(decimal quantia)
        {
            AcrecentarRendimento();
            Saldo += quantia;
        }
    }
}
