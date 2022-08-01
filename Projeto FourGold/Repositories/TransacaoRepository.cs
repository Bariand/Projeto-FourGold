using Projeto_FourGold.Areas.Identity.Data;
using Projeto_FourGold.Models;

namespace Projeto_FourGold.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private FourGoldContext _context;

        public TransacaoRepository(FourGoldContext context)
        {
            _context = context;
        }

        public List<Transacao> BuscarTransacoes(int id)
        {
            return _context.Transacoes.Where(t => t.ContaId == id).OrderByDescending(t => t.DataTransacao).ToList();
        }

        public void RegistrarTransacao(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
