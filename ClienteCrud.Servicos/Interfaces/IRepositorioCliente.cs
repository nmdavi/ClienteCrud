using ClienteCrud.Dominio.Modelo;

using System.Collections.Generic;

namespace ClienteCrud.Servicos.Interfaces
{
    public interface IRepositorioCliente
    {
        void Atualizar(Cliente cliente);
        void Excluir(int id);
        Cliente ObterPorId(int id);
        IList<Cliente> ObterTodos();
        void Inserir(Cliente cliente);
    }
}
