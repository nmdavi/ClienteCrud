using System.Collections.Generic;

using NHibernate.Mapping.Attributes;


namespace ClienteCrud.Dominio.Modelo
{
    [Class(Table = "Cliente")]
    public class Cliente
    {
        [Id(0, Name = "Id", Column = "Id")]
        [Generator(1, Class = "identity")]
        public virtual int Id { get; set; }
        
        [Property]
        public virtual string Nome { get; set; }

        [Property(Column = "Sexo", Length = 20)]
        public virtual string Sexo { get; set; }
        
        [Property]
        public virtual string Endereco { get; set; }
        
        [Bag(0, Name = "Telefones", Cascade = "all-delete-orphan", Inverse = true, Lazy = CollectionLazy.False)]
        [Key(1, Column = "ClienteId")]
        [OneToMany(2, ClassType = typeof(Telefone))]
        public virtual IList<Telefone> Telefones { get; set; } = new List<Telefone>();
    }
}