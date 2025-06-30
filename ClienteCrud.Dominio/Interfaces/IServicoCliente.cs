using ClienteCrud.Dominio.Modelo;

using System.Collections.Generic;

namespace ClienteCrud.Dominio.Interfaces
{
    public interface IServicoCliente
    {
        Cliente Obter(int id);
        IList<Cliente> ObterTodos();
        void Inserir(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Apagar(int id);
    }
}
