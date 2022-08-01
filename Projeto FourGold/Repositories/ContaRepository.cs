using Microsoft.EntityFrameworkCore;
using Projeto_FourGold.Areas.Identity.Data;
using Projeto_FourGold.Models;
using System.Linq.Expressions;

namespace Projeto_FourGold.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private Areas.Identity.Data.FourGoldContext _context;
        public ContaRepository(Areas.Identity.Data.FourGoldContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Métodos de ContaRepository
        /// </summary>


        public void AtualizarConta(Conta conta)
        {
            if (conta.Tipo == TipoConta.Corrente)
            {
                _context.ContasCorrente.Update((ContaCorrente)conta);
            }
            else
            {
                _context.ContasPoupanca.Update((ContaPoupanca)conta);
            }
        }

        public Conta BuscarConta(Expression<Func<Cliente, bool>> filtro)
        {
            return _context.Clientes.Where(filtro).Include(cliente => cliente.Conta).Select(cliente => cliente.Conta).FirstOrDefault();
        }

        public void CriarConta(Conta conta)
        {
            if (conta.Tipo == TipoConta.Corrente)
            {
                _context.ContasCorrente.Add((ContaCorrente)conta);
            }
            else
            {
                _context.ContasPoupanca.Add((ContaPoupanca)conta);
            }
        }

        public List<Conta> ListarContas()
        {
            List<Conta> contaList = new List<Conta>();
            return contaList;
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
