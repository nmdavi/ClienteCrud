using ClienteCrud.Dominio.Interfaces;
using ClienteCrud.Dominio.Modelo;

using System.Reflection;
using System.Web.Mvc;

namespace ClienteCrud.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IServicoCliente _servicoCliente;

        public ClienteController(IServicoCliente servicoCliente)
        {
            _servicoCliente = servicoCliente;
        }

        public ActionResult Index()
        {
            var clientes = _servicoCliente.ObterTodos();
            return View(clientes);
        }

        public ActionResult Detalhes(int id)
        {
            var cliente = _servicoCliente.Obter(id);
            if (cliente == null) return HttpNotFound();
            return View(cliente);
        }

        public ActionResult Novo()
        {
            return View(new Cliente());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Novo(Cliente cliente)
        {
            _servicoCliente.Inserir(cliente);
            return RedirectToAction("Index");
        }

        public ActionResult Atualizar(int id)
        {
            var cliente = _servicoCliente.Obter(id);
            if (cliente == null) return HttpNotFound();
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atualizar(Cliente cliente)
        {
            var obterCliente = _servicoCliente.Obter(cliente.Id);
            obterCliente.Nome = cliente.Nome;
            obterCliente.Sexo = cliente.Sexo;
            obterCliente.Endereco = cliente.Endereco;

            obterCliente.Telefones.Clear();
            foreach (var tel in cliente.Telefones)
            {
                obterCliente.Telefones.Add(new Telefone
                {
                    Numero = tel.Numero,
                    Ativo = tel.Ativo,
                    Cliente = cliente
                });
            }

            _servicoCliente.Atualizar(cliente);

            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            _servicoCliente.Apagar(id);
            return RedirectToAction("Index");
        }
    }
}