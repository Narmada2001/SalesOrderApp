using Microsoft.AspNetCore.Mvc;
using SalesOrderApi.Domain.Entities;
using SalesOrderApp.Application.Interfaces;

namespace SalesOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IGenericRepository<Client> _repository;

        public ClientsController(IGenericRepository<Client> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.ListAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            await _repository.AddAsync(client);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }
    }
}
