using Clientes.Dto;
using Clientes.Models;
using Clientes.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clients.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesRepository _clienteRepository;
        private readonly ILogger<ClientesController> _logger;
        private readonly string _idLog = $"{DateTime.Now:yyyyMMddhhmmss} - ";

        public ClientesController(IClientesRepository clienteRepository, ILogger<ClientesController> logger)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;

        }


        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _clienteRepository.GetClientes(_idLog);
            return Ok(clientes);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetClientePorId(int id)
        //{
        //    var cliente = await _clienteRepository.GetClientePorId(id, _idLog);

        //    if (cliente == null)
        //    {
        //        _logger.LogWarning($"{_idLog} Cliente con id={id} no encontrado");
        //        return NotFound();
        //    }

        //    return Ok(cliente);
        //}

        [HttpGet("{identificacion}")]
        public async Task<IActionResult> GetClientePorNumeroDocumento(long identificacion)
        {
            var cliente = await _clienteRepository.GetClientePorNumeroDocumento(identificacion, _idLog);

            if (cliente == null)
            {
                _logger.LogWarning($"{_idLog} Cliente con identificacion={identificacion} no encontrado");
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> InsertarCliente([FromBody] ClientesDto cliente)
        {
            var result = await _clienteRepository.InsertarCliente(cliente, _idLog);

            _logger.LogInformation($"{_idLog} Cliente insertado. Filas afectadas={result}");
            return Ok(new { FilasAfectadas = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCliente(int id, [FromBody] ClientesDto cliente)
        {
            if (id != cliente.id)
            {
                _logger.LogWarning($"{_idLog} Id en URL ({id}) no coincide con Id en body ({cliente.id})");
                return BadRequest("El id del cliente no coincide");
            }

            var result = await _clienteRepository.EditarCliente(cliente, _idLog);

            _logger.LogInformation($"{_idLog} Cliente editado id={id}. Filas afectadas={result}");
            return Ok(new { FilasAfectadas = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var result = await _clienteRepository.EliminarCliente(id, _idLog);

            _logger.LogInformation($"{_idLog} Cliente eliminado id={id}. Filas afectadas={result}");
            return Ok(new { FilasAfectadas = result });
        }

    }
}
