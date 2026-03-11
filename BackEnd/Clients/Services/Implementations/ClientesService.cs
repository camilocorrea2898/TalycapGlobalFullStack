using Clientes.Dto;
using Clientes.Models;
using Clientes.Repositories.Implementations;
using Clientes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Clientes.Services.Implementations
{
    public class ClientesService : IClientesService
    {
        private readonly ILogger<ClientesService> _logger;
        private readonly string _idLog = $"{DateTime.Now:yyyyMMddhhmmss} - ";
        private readonly MyDbContext _context;

        public ClientesService(ILogger<ClientesService> logger,MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<List<ClientesDto>> GetClientes(string _idLog)
        {
            try
            {
                var clientes = await _context.Clientes.FromSqlRaw("EXEC sp_Clientes_Consultar").ToListAsync();
                _logger.LogDebug($"{_idLog} Se ejecutó SP: sp_Clientes_Consultar");
                var clientesDto = clientes.Select(c => new ClientesDto
                {
                    id = c.Id,
                    idTipoDocumento = c.IdTipoDocumento,
                    numeroDocumento = c.NumeroDocumento,
                    nombreCompleto = c.NombreCompleto,
                    fechaNacimiento = c.FechaNacimiento,
                    empresa = c.Empresa,
                    activo = c.Activo,
                    fechaCreacion = c.FechaCreacion
                }).ToList();

                return clientesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{_idLog} Error al ejecutar SP: sp_Clientes_Consultar");
                throw;
            }
        }

        public async Task<ClientesDto?> GetClientePorId(int id, string idLog)
        {
            try
            {
                var cliente = _context.Clientes
                    .FromSqlRaw("EXEC sp_Clientes_ConsultarPorId @id={0}", id)
                    .AsEnumerable()
                    .FirstOrDefault();

                _logger.LogDebug($"{idLog} Se ejecutó SP: sp_Clientes_ConsultarPorId con id={id}");
                if (cliente == null) return null;

                // Mapear Cliente a ClientesDto
                var clienteDto = new ClientesDto
                {
                    id = cliente.Id,
                    idTipoDocumento = cliente.IdTipoDocumento,
                    numeroDocumento = cliente.NumeroDocumento,
                    nombreCompleto = cliente.NombreCompleto,
                    fechaNacimiento = cliente.FechaNacimiento,
                    empresa = cliente.Empresa,
                    activo = cliente.Activo,
                    fechaCreacion = cliente.FechaCreacion
                };

                return clienteDto;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{idLog} Error al ejecutar SP: sp_Clientes_ConsultarPorId con id={id}");
                throw;
            }
        }

        public async Task<ClientesDto?> GetClientePorNumeroDocumento(long numeroDocumento, string idLog)
        {
            try
            {
                var cliente = _context.Clientes
                    .FromSqlRaw("EXEC sp_Clientes_ConsultarPorNumeroDocumento @numeroDocumento={0}", numeroDocumento)
                    .AsEnumerable()
                    .FirstOrDefault();

                _logger.LogDebug($"{idLog} Se ejecutó SP: sp_Clientes_ConsultarPorNumeroDocumento con numeroDocumento={numeroDocumento}");
                if (cliente == null) return null;

                // Mapear Cliente a ClientesDto
                var clienteDto = new ClientesDto
                {
                    id = cliente.Id,
                    idTipoDocumento = cliente.IdTipoDocumento,
                    numeroDocumento = cliente.NumeroDocumento,
                    nombreCompleto = cliente.NombreCompleto,
                    fechaNacimiento = cliente.FechaNacimiento,
                    empresa = cliente.Empresa,
                    activo = cliente.Activo,
                    fechaCreacion = cliente.FechaCreacion
                };

                return clienteDto;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{idLog} Error al ejecutar SP: sp_Clientes_ConsultarPorNumeroDocumento con numeroDocumento={numeroDocumento}");
                throw;
            }
        }

        public async Task<int> InsertarCliente(ClientesDto cliente, string idLog)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Clientes_Insertar @idTipoDocumento={0}, @numeroDocumento={1}, @nombreCompleto={2}, @fechaNacimiento={3}, @empresa={4}, @activo={5}",
                    cliente.idTipoDocumento,
                    cliente.numeroDocumento,
                    cliente.nombreCompleto,
                    cliente.fechaNacimiento,
                    cliente.empresa,
                    cliente.activo
                );

                _logger.LogDebug($"{idLog} Se ejecutó SP: sp_Clientes_Insertar para cliente {cliente.nombreCompleto}. Filas afectadas: {result}");
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{idLog} Error al ejecutar SP: sp_Clientes_Insertar");
                throw;
            }
        }

        public async Task<int> EditarCliente(ClientesDto cliente, string idLog)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Clientes_Editar @id={0}, @idTipoDocumento={1}, @numeroDocumento={2}, @nombreCompleto={3}, @fechaNacimiento={4}, @empresa={5}, @activo={6}",
                    cliente.id,
                    cliente.idTipoDocumento,
                    cliente.numeroDocumento,
                    cliente.nombreCompleto,
                    cliente.fechaNacimiento,
                    cliente.empresa,
                    cliente.activo
                );

                _logger.LogDebug($"{idLog} Se ejecutó SP: sp_Clientes_Editar para cliente id={cliente.id}. Filas afectadas: {result}");
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{idLog} Error al ejecutar SP: sp_Clientes_Editar para cliente id={cliente.id}");
                throw;
            }
        }

        public async Task<int> EliminarCliente(int id, string idLog)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlRawAsync("EXEC sp_Clientes_Eliminar @id={0}", id);

                _logger.LogDebug($"{idLog} Se ejecutó SP: sp_Clientes_Eliminar para cliente id={id}. Filas afectadas: {result}");
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{idLog} Error al ejecutar SP: sp_Clientes_Eliminar para cliente id={id}");
                throw;
            }
        }
    }
}
