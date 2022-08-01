using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Projeto_FourGold.Models;

namespace Projeto_FourGold.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Cliente class
public class Cliente : IdentityUser
{

    [Required]
    public string Cpf { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required, Column("Dt_Nascimento")]
    public DateTime DataNascimento { get; set; }

    [Required]
    public TipoCliente Tipo { get; set; }

    //1:1
    public Conta Conta { get; set; }

    public int ContaId { get; set; }

    public static TipoCliente VerificarTipoCliente(decimal saldo)
    {
        TipoCliente tipoCliente;

        if (saldo >= 15000)
        {
            tipoCliente = Data.TipoCliente.Premium;
        }

        else if (saldo < 5000)
        {
            tipoCliente = Data.TipoCliente.Comum;
        }

        else
        {
            tipoCliente = Data.TipoCliente.Super;
        }

        return tipoCliente;
    }
}

public enum TipoCliente
{
    Comum, Super, Premium
}

