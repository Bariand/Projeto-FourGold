using Projeto_FourGold.Models;

namespace Projeto_FourGold.Repositories
{
    public interface ITransacaoRepository
    {
        List<Transacao> BuscarTransacoes(int id);

        void RegistrarTransacao(Transacao transacao);

        void Salvar();
    }
}
