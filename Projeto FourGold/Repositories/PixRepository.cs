using Microsoft.EntityFrameworkCore;
using Projeto_FourGold.Areas.Identity.Data;
using Projeto_FourGold.Models;

namespace Projeto_FourGold.Repositories
{
    public class PixRepository : IPixRepository
    {
        private FourGoldContext _context;

        public PixRepository(FourGoldContext context)
        {
            _context = context;
        }

        public void AdicionarChavePix(ChavePix chavePix)
        {
            _context.ChavesPix.Add(chavePix);
        }

        public void AtualizarChavePiX(ChavePix chavePix)
        {
            _context.ChavesPix.Update(chavePix);
        }

        public Conta BuscarConta(string chavePix)
        {
            return _context.ChavesPix.Where(chave => chave.Chave == chavePix).Include(pix => pix.Conta).Select(pix => pix.Conta).FirstOrDefault();
        }

        public ChavePix EncontrarChavePix(string chave, int chavePixTipo)
        {
            return _context.ChavesPix.Where(x => x.Chave == chave && x.Tipo == (TipoChavePix)chavePixTipo).FirstOrDefault();
        }

        public IList<ChavePix> ListarChavePix()
        {           
                return _context.ChavesPix.ToList(); 
        }

        public void Remover(int id)
        {
            ChavePix chavePix = _context.ChavesPix.Find(id);
            _context.ChavesPix.Remove(chavePix);
        }
        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
