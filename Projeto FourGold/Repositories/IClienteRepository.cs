using Projeto_FourGold.Areas.Identity.Data;
using System.Linq.Expressions;

namespace Projeto_FourGold.Repositories
{
    public interface IClienteRepository
    {
        public Cliente BuscarCliente(Expression<Func<Cliente, bool>> filtro);
        public void AtualizarCliente(Cliente cliente);
        public void Salvar();
    }
}
