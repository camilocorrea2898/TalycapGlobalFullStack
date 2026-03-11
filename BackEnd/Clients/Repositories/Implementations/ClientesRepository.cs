using Clientes.Dto;
using Clientes.Models;
using Clientes.Repositories.Interfaces;
using Clientes.Services;
using Clientes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Runtime.CompilerServices;

namespace Clientes.Repositories.Implementations
{
    public class ClientesRepository : IClientesRepository
    {
        //Datos de la clase
        private readonly ILogger<ClientesRepository> _logger;
        private readonly IClientesService _clienteService;


        public ClientesRepository(ILogger<ClientesRepository> logger, IClientesService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;

        }
        public async Task<List<ClientesDto>> GetClientes(string _idLog)
        {
            return await _clienteService.GetClientes(_idLog);
        }
        public async Task<ClientesDto?> GetClientePorId(int id, string idLog)
        {
            return await _clienteService.GetClientePorId(id, idLog);
        }
        public async Task<ClientesDto?> GetClientePorNumeroDocumento(long numeroDocumento, string idLog)
        {
            return await _clienteService.GetClientePorNumeroDocumento(numeroDocumento, idLog);
        }
        public async Task<int> InsertarCliente(ClientesDto cliente, string idLog)
        {
            return await _clienteService.InsertarCliente(cliente, idLog);
        }
        public async Task<int> EditarCliente(ClientesDto cliente, string idLog)
        {
            return await _clienteService.EditarCliente(cliente, idLog);
        }
        public async Task<int> EliminarCliente(int id, string idLog)
        {
            return await _clienteService.EliminarCliente(id, idLog);
        }
    }
}
