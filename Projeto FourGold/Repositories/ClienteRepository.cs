using Projeto_FourGold.Areas.Identity.Data;
using System.Linq.Expressions;

namespace Projeto_FourGold.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private Areas.Identity.Data.FourGoldContext _context;
        public ClienteRepository(Areas.Identity.Data.FourGoldContext context)
        {
            _context = context;
        }

        public void AtualizarCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
        }

        public Cliente BuscarCliente(Expression<Func<Cliente, bool>> filtro)
        {
            return _context.Clientes.Where(filtro).FirstOrDefault();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
