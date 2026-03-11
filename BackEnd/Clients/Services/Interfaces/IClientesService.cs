using Clientes.Dto;
using Clientes.Models;

namespace Clientes.Services.Interfaces
{
    public interface IClientesService
    {
        Task<List<ClientesDto>> GetClientes(string _idLog);
        Task<ClientesDto?> GetClientePorId(int id, string idLog);
        Task<ClientesDto?> GetClientePorNumeroDocumento(long numeroDocumento, string idLog);
        Task<int> InsertarCliente(ClientesDto cliente, string idLog);
        Task<int> EditarCliente(ClientesDto cliente, string idLog);
        Task<int> EliminarCliente(int id, string idLog);
    }
}
