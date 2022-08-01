using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_FourGold.Areas.Identity.Data;
using Projeto_FourGold.Models;
using Projeto_FourGold.Repositories;
using Projeto_FourGold.ViewModels;

namespace Projeto_FourGold.Controllers
{
    public class ContaController : Controller
    {
        private IPixRepository _pixRepository;
        private IContaRepository _contaRepository;
        private IClienteRepository _clienteRepository;
        private ITransacaoRepository _transacaoRepository;
        private UserManager<Cliente> _userManager;

        public ContaController(IPixRepository pixRepository,
                               IContaRepository contaRepository,
                               IClienteRepository clienteRepository,
                               ITransacaoRepository transacaoRepository,
                               UserManager<Cliente> userManager)
        {
            _pixRepository = pixRepository;
            _contaRepository = contaRepository;
            _clienteRepository = clienteRepository;
            _transacaoRepository = transacaoRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            Conta conta = _contaRepository.BuscarConta(cliente => cliente.Id == _userManager.GetUserId(User));
            TempData["Saldo"] = conta.Saldo;
            TempData["NumeroConta"] = conta.Numero;

            Cliente cliente = _clienteRepository.BuscarCliente(cliente => cliente.Id == _userManager.GetUserId(User));
            TempData["TipoCliente"] = cliente.Tipo;

            ChavePixViewModel viewModel = new ChavePixViewModel();

            viewModel.Transacoes = _transacaoRepository.BuscarTransacoes(conta.ContaId);

            viewModel.ListaPix = _pixRepository.ListarChavePix().Where(chave => chave.ContaId == conta.ContaId).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Transferir(decimal valor, string chavePix, int ChavePixTipo)
        {
            // Include não funciona com Find(). Tem que usar Where e FirstOrDefault.

            if (ModelState.IsValid)
            {
                Conta conta = _contaRepository.BuscarConta(cliente => cliente.Id == _userManager.GetUserId(User));
                ChavePix chaveDestino = _pixRepository.EncontrarChavePix(chavePix, ChavePixTipo);
                Conta contaDestino = _pixRepository.BuscarConta(chavePix);

                if(chaveDestino == null)
                {
                    TempData["msgErroChave"] = "Conta inválida!";
                    return RedirectToAction("Index");
                }
                
                if (conta.Saldo >= valor && valor > 0)
                {
                    conta.Transferir(valor);
                    _contaRepository.AtualizarConta(conta);

                    // Registrar transferencia
                    Transacao transferencia = new Transacao()
                    {
                        Valor = valor,
                        DataTransacao = DateTime.Now,
                        Tipo = TipoTransacao.Transferencia,
                        ContaId = conta.ContaId,
                    };
                    _transacaoRepository.RegistrarTransacao(transferencia);

                    contaDestino.Depositar(valor);
                    _contaRepository.AtualizarConta(contaDestino);
                    _contaRepository.Salvar();

                    // Registrar deposito
                    Transacao deposito = new Transacao()
                    {
                        Valor = valor,
                        DataTransacao = DateTime.Now,
                        Tipo = TipoTransacao.Deposito,
                        ContaId = contaDestino.ContaId
                    };
                    _transacaoRepository.RegistrarTransacao(deposito);
                    _transacaoRepository.Salvar();


                    Cliente cliente = _clienteRepository.BuscarCliente(cliente => cliente.Id == _userManager.GetUserId(User));
                    cliente.Tipo = Cliente.VerificarTipoCliente(conta.Saldo);
                    _clienteRepository.AtualizarCliente(cliente);
                    _clienteRepository.Salvar();
                    TempData["Transferencia"] = "Transferência realizada com sucesso!";
                }

                else
                {
                    if (valor <= 0)
                        TempData["msgErroTrans"] = "Valor de transferência inválido!";

                    else
                        TempData["SaldoInsuficiente"] = "Saldo insuficiente para operação!";
                }
            }
            else
            {
                TempData["Erro"] = "Algo deu errado!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Depositar(decimal valorDeposito)
        {
            if (ModelState.IsValid)
            {
                if (valorDeposito > 0)
                {
                    Conta conta = _contaRepository.BuscarConta(cliente => cliente.Id == _userManager.GetUserId(User));
                    conta.Depositar(valorDeposito);
                    _contaRepository.AtualizarConta(conta);
                    _contaRepository.Salvar();

                    // Registrar deposito
                    Transacao deposito = new Transacao()
                    {
                        Valor = valorDeposito,
                        DataTransacao = DateTime.Now,
                        Tipo = TipoTransacao.Deposito,
                        ContaId = conta.ContaId
                    };
                    _transacaoRepository.RegistrarTransacao(deposito);
                    _transacaoRepository.Salvar();

                    Cliente cliente = _clienteRepository.BuscarCliente(cliente => cliente.Id == _userManager.GetUserId(User));
                    cliente.Tipo = Cliente.VerificarTipoCliente(conta.Saldo);
                    _clienteRepository.AtualizarCliente(cliente);
                    _clienteRepository.Salvar();

                    TempData["Deposito"] = "Depósito realizado com sucesso!";
                }
                else
                {
                    TempData["msgErro"] = "Valor de depósito inválido!";
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CadastrarChave(ChavePixViewModel chaves)
        {
            if (chaves.ChavePix.Tipo == TipoChavePix.Aleatorio)
            {
                chaves.ChavePix.Chave = chaves.ChavePix.GerarChaveAleatoria();
            }
            else if (chaves.ChavePix.Chave == null)
            {
                return RedirectToAction("Index");
            }
            ChavePix pix = _pixRepository.EncontrarChavePix(chaves.ChavePix.Chave, (int)chaves.ChavePix.Tipo);
            if (pix != null)
            {
                TempData["ChaveExistente"] = "Desculpe, essa chave não está disponível! Tente novamente.";
                return RedirectToAction("Index");
            }
            Conta conta = _contaRepository.BuscarConta(cliente => cliente.Id == _userManager.GetUserId(User));
            chaves.ChavePix.ContaId = conta.ContaId;
            _pixRepository.AdicionarChavePix(chaves.ChavePix);
            _pixRepository.Salvar();
            TempData["ChaveCadastrada"] = "Chave pix cadastrada com sucessso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoverChave(int id)
        {
            _pixRepository.Remover(id);
            _pixRepository.Salvar();
            return RedirectToAction("Index");
        }
    }
}
