namespace Projeto_FourGold.Models
{
    public class ContaCorrente : Conta
    {
        public decimal TaxaManutencao { get; set; } = 2.50m;

        public void DescontarTaxa()
        {
            Saldo -= TaxaManutencao;
        }

        public override void Transferir(decimal quantia)
        {
            DescontarTaxa();
            Saldo -= quantia;
        }

        public override void Depositar(decimal quantia)
        {
            Saldo += quantia;
        }
    }
}
