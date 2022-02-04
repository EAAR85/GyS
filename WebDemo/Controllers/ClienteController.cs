using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebDemo.Common;
using WebDemo.Entity;
using WebDemo.Repository;

namespace WebDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteValidator clienteValidator;
        private readonly ClienteRepository clienteRepository;

        public ClienteController(ClienteRepository clienteRepository, ClienteValidator clienteValidator)
        {
            this.clienteValidator = clienteValidator;
            this.clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<List<ClienteResponse>> all()
        {
            return await clienteRepository.all();
        }

        [HttpGet("{id}")]
        public async Task<ClienteResponse> find(int id)
        {
            return await clienteRepository.find(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, ClienteRequest request)
        {
            var validation = clienteValidator.Validate(request);
            if (id != request.codigoCliente || !validation.IsValid)
                return BadRequest(validation.Errors);

            await clienteRepository.update(id, request);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> save([FromBody] ClienteRequest request)
        {
            var validation = clienteValidator.Validate(request);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            await clienteRepository.save(request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task delete(int id)
        {
            await clienteRepository.delete(id);
        }
    }
}
