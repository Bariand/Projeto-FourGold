using Projeto_FourGold.Areas.Identity.Data;
using Projeto_FourGold.Models;
using System.Linq.Expressions;

namespace Projeto_FourGold.Repositories
{
    public interface IContaRepository
    {
        Conta BuscarConta(Expression<Func<Cliente, bool>> filtro);

        void CriarConta(Conta conta);

        void AtualizarConta(Conta conta);

        List<Conta> ListarContas();

        void Salvar();
    }
}
