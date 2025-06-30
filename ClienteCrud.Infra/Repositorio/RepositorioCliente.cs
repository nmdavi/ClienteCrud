using ClienteCrud.Infra.Contexto;

namespace ClienteCrud.Infra.Repositorio
{
    public class RepositorioCliente : IRepositorioCliente
    {
        public Cliente ObterPorId(int id)
        {
            using (var session = ContextoNHibernate.OpenSession())
            {
                return session.Get<Cliente>(id);
            }
        }

        public IList<Cliente> ObterTodos()
        {
            using (var session = ContextoNHibernate.OpenSession())
            {
                return session.Query<Cliente>().ToList();
            }
        }

        public void Inserir(Cliente cliente)
        {
            using (var session = ContextoNHibernate.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var telefones = cliente.Telefones;

                cliente.Telefones = new List<Telefone>();

                session.Save(cliente);

                cliente.Telefones = telefones;

                foreach (var tel in cliente.Telefones)
                {
                    tel.Cliente = cliente; 
                    session.Save(tel);     
                }

                transaction.Commit();
            }
        }

        public void Atualizar(Cliente cliente)
        {
            using (var session = ContextoNHibernate.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var obterCliente = session.Get<Cliente>(cliente.Id);
                if (obterCliente != null)
                {
                    obterCliente.Sexo = cliente.Sexo;  
                    session.Update(obterCliente);
                    transaction.Commit();
                }
            }

        }

        public void Excluir(int id)
        {
            using (var session = ContextoNHibernate.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var cliente = session.Get<Cliente>(id); 

                if (cliente != null)
                {
                    session.Delete(cliente);      
                    transaction.Commit();         
                }
            }

        }
    }
}
