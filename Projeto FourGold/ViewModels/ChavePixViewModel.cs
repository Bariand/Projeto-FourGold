using Projeto_FourGold.Models;

namespace Projeto_FourGold.ViewModels
{
    public class ChavePixViewModel
    {
        public Conta Conta{ get; set; }

        public ChavePix ChavePix { get; set; }

        public List<ChavePix> ListaPix { get; set; }

        public List<Transacao> Transacoes { get; set; }
    }
}
