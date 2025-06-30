using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;

using System.Reflection;

namespace ClienteCrud.Infra.Contexto
{
    public class ContextoNHibernate
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var cfg = new Configuration();
                    cfg.Configure(); 

                    var stream = HbmSerializer.Default.Serialize(Assembly.Load("ClienteCrud.Dominio"));
                    cfg.AddInputStream(stream);

                    _sessionFactory = cfg.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
