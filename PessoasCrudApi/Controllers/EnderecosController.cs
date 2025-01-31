using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PessoasCrudApi.Data;
using PessoasCrudApi.Models.Dtos;
using PessoasCrudApi.Models.Entities;

namespace PessoasCrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecosController : ControllerBase
    {
        private readonly AplicationDbContext dbContext;

        public EnderecosController(AplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult ListarEnderecos()
        {
            var listaEnderecos = dbContext.Enderecos
         .Select(e => new EnderecoResponseDto
         {
             Id = e.Id,
             PessoaId = e.PessoaId,
             Logradouro = e.Logradouro,
             Cidade = e.Cidade,
             Estado = e.Estado,
             CEP = e.CEP
         })
         .ToList();

            return Ok(listaEnderecos);
        }

        [HttpGet("{id:guid}")]
        public IActionResult ListarEnderecoPorId(Guid id)
        {
            var endereco = dbContext.Enderecos
                                    .Include(e => e.Pessoa) // Inclue a propriedade Pessoa. 
                                    .FirstOrDefault(e => e.Id == id); // Em vez de Find(), FirstOrDefault pode encontrar o primeiro elemento com o id declarado; caso contr�rio, a resposta ser� nula.
            if (endereco == null)
            {
                return NotFound();
            }
            return Ok(endereco);
        }

        [HttpPost]
        public IActionResult AdicionarEndereco([FromBody] AdicionarEnderecoDto enderecoDto)
        {
            var pessoa = dbContext.Pessoas.Find(enderecoDto.PessoaId);
            if (pessoa == null)
            {
                return NotFound("Pessoa n�o encontrada.");
            }

            var novoEndereco = new Endereco
            {
                Id = Guid.NewGuid(),
                PessoaId = enderecoDto.PessoaId,
                Pessoa = pessoa,
                Logradouro = enderecoDto.Logradouro,
                Cidade = enderecoDto.Cidade,
                Estado = enderecoDto.Estado,
                CEP = enderecoDto.CEP
            };

            dbContext.Enderecos.Add(novoEndereco);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(ListarEnderecoPorId), new { id = novoEndereco.Id }, novoEndereco);
        }


        [HttpPut("{id:guid}")]
        public IActionResult AtualizarEndereco(Guid id, [FromBody] AtualizarEnderecoDto enderecoDto)
        {
            var endereco = dbContext.Enderecos.Find(id);
            if (endereco == null)
            {
                return NotFound("Endere�o n�o encontrado.");
            }

            var pessoa = dbContext.Pessoas.Find(enderecoDto.PessoaId);
            if (pessoa == null)
            {
                return NotFound("Pessoa n�o encontrada.");
            }

            endereco.PessoaId = enderecoDto.PessoaId;
            endereco.Pessoa = pessoa;
            endereco.Logradouro = enderecoDto.Logradouro;
            endereco.Cidade = enderecoDto.Cidade;
            endereco.Estado = enderecoDto.Estado;
            endereco.CEP = enderecoDto.CEP;

            dbContext.SaveChanges();

            return Ok(endereco);
        }


        [HttpDelete("{id:guid}")]
        public IActionResult ApagarEndereco(Guid id)
        {
            var endereco = dbContext.Enderecos.Find(id);
            if (endereco == null)
            {
                return NotFound("Endere�o n�o encontrado.");
            }

            dbContext.Enderecos.Remove(endereco);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
