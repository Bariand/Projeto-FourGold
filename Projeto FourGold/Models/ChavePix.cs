using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_FourGold.Models
{
    [Table("Tbl_ChavePix")]
    public class ChavePix
    {
        [Required, Column("Id")]
        public int ChavePixId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Chave { get; set; }

        [Required, Column("TipoChave")]
        public TipoChavePix Tipo { get; set; }


        //N:1
        [Required]
        public Conta Conta { get; set; }

        public int ContaId { get; set; }

        public string GerarChaveAleatoria()
        {
            Random random = new Random();
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] lista = new char[10];
            for (int i = 0; i < lista.Length; i++)
            {
                lista[i] = caracteres[random.Next(caracteres.Length)];
            }
            string chaveAleatoria = new string(lista);
            return chaveAleatoria;
        }
    }

    public enum TipoChavePix
    {
        Cpf, Email, Telefone, Aleatorio
    }
}
