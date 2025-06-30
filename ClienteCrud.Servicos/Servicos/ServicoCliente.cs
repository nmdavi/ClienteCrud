using ClienteCrud.Dominio.Interfaces;
using ClienteCrud.Dominio.Modelo;

using System.Collections.Generic;

using System;
using ClienteCrud.Servicos.Interfaces;

namespace ClienteCrud.Servicos.Servicos
{
    public class ServicoCliente : IServicoCliente
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly ICacheRedis _cacheRedis;

        public ServicoCliente(IRepositorioCliente repositorioCliente, ICacheRedis cacheRedis)
        {
            _repositorioCliente = repositorioCliente;
            _cacheRedis = cacheRedis;
        }

        public Cliente Obter(int id)
        {
            string cacheClienteKey = $"cliente:{id}";
            string cacheTelefoneKey = $"telefone:{id}";

            var cliente = _cacheRedis.Get<Cliente>(cacheClienteKey);
            var telefones = _cacheRedis.Get<List<Telefone>>(cacheTelefoneKey);
            if (cliente != null)
            {
                cliente.Telefones = telefones;
                return cliente;
            }

            cliente = _repositorioCliente.ObterPorId(id);
            if (cliente != null)
            {
                _cacheRedis.Set(cacheClienteKey, cliente, TimeSpan.FromMinutes(30));
                _cacheRedis.Set(cacheTelefoneKey, cliente.Telefones, TimeSpan.FromMinutes(30));
            }

            return cliente;
        }

        public IList<Cliente> ObterTodos()
        {
            return _repositorioCliente.ObterTodos();
        }

        public void Inserir(Cliente cliente)
        {
            AplicarRegraTelefoneUnicoAtivo(cliente);
            _repositorioCliente.Inserir(cliente);

            var cacheTelefones = cliente.Telefones;

            cliente.Telefones = null; // evitar loop de referência

            _cacheRedis.Set($"cliente:{cliente.Id}", cliente, TimeSpan.FromMinutes(30));
            _cacheRedis.Set($"telefone:{cliente.Id}", cacheTelefones, TimeSpan.FromMinutes(30));
        }

        public void Atualizar(Cliente cliente)
        {
            AplicarRegraTelefoneUnicoAtivo(cliente);
            _repositorioCliente.Atualizar(cliente);

            var cacheTelefones = cliente.Telefones;

            cliente.Telefones = null; // evitar loop de referência

            _cacheRedis.Set($"cliente:{cliente.Id}", cliente, TimeSpan.FromMinutes(30));
            _cacheRedis.Set($"telefone:{cliente.Id}", cacheTelefones, TimeSpan.FromMinutes(30));
        }

        public void Apagar(int id)
        {
            _repositorioCliente.Excluir(id);
            _cacheRedis.Remove($"cliente:{id}");
            _cacheRedis.Remove($"telefone:{id}");
        }

        private void AplicarRegraTelefoneUnicoAtivo(Cliente cliente)
        {
            if (cliente.Telefones == null)
                return;

            bool ativoJaDefinido = false;
            foreach (var telefone in cliente.Telefones)
            {
                if (telefone.Ativo)
                {
                    if (ativoJaDefinido)
                        telefone.Ativo = false;
                    else
                        ativoJaDefinido = true;
                }
            }
        }
    }
}
