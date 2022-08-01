using Projeto_FourGold.Models;

namespace Projeto_FourGold.Repositories
{
    public interface IPixRepository
    {
        Conta BuscarConta(string chavePix);


        ChavePix EncontrarChavePix(string chave, int chavePixTipo);

        IList<ChavePix> ListarChavePix();

        void AtualizarChavePiX(ChavePix chavePix);

        void AdicionarChavePix(ChavePix chavePix);

        void Remover(int id);

        void Salvar();
    }
}
