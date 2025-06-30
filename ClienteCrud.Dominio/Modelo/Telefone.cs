using NHibernate.Mapping.Attributes;

namespace ClienteCrud.Dominio.Modelo
{
    [Class(Table = "Telefone")]
    public class Telefone
    {
        [Id(0, Name = "Id", Column = "Id")]
        [Generator(1, Class = "identity")]
        public virtual int Id { get; set; }
        
        [Property]
        public virtual string Numero { get; set; }
        
        [Property]
        public virtual bool Ativo { get; set; }
        
        [ManyToOne(ClassType = typeof(Cliente), Column = "ClienteId", NotNull = false)]
        public virtual Cliente Cliente { get; set; }
    }
}