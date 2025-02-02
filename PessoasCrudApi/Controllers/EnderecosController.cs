using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PessoasCrudApi.Data;
using PessoasCrudApi.Models.Dtos;
using PessoasCrudApi.Models.Entities;

namespace PessoasCrudApi.Controllers
{
    [Route("enderecos")]
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
                                    .Include(e => e.Pessoa)
                                    .FirstOrDefault(e => e.Id == id);

            if (endereco == null)
            {
                return NotFound();
            }

            var enderecoDto = new EnderecoResponseDto
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                CEP = endereco.CEP,
                PessoaId = endereco.PessoaId
            };

            return Ok(enderecoDto);
        }


        [HttpPost]
        public IActionResult AdicionarEndereco([FromBody] AdicionarEnderecoDto enderecoDto)
        {
            var pessoa = dbContext.Pessoas.Find(enderecoDto.PessoaId);
            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada.");
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

            var enderecoResponse = new EnderecoResponseDto
            {
                Id = novoEndereco.Id,
                PessoaId = novoEndereco.PessoaId,
                Logradouro = novoEndereco.Logradouro,
                Cidade = novoEndereco.Cidade,
                Estado = novoEndereco.Estado,
                CEP = novoEndereco.CEP
            };

            return CreatedAtAction(nameof(ListarEnderecoPorId), new { id = novoEndereco.Id }, enderecoResponse);
        }



        [HttpPut("{id:guid}")]
        public IActionResult AtualizarEndereco(Guid id, [FromBody] AtualizarEnderecoDto enderecoDto)
        {
            var endereco = dbContext.Enderecos.Find(id);
            if (endereco == null)
            {
                return NotFound("Endereço não encontrado.");
            }

            var pessoa = dbContext.Pessoas.Find(enderecoDto.PessoaId);
            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada.");
            }

            endereco.PessoaId = enderecoDto.PessoaId;
            endereco.Pessoa = pessoa;
            endereco.Logradouro = enderecoDto.Logradouro;
            endereco.Cidade = enderecoDto.Cidade;
            endereco.Estado = enderecoDto.Estado;
            endereco.CEP = enderecoDto.CEP;

            dbContext.SaveChanges();

            var enderecoResponse = new EnderecoResponseDto
            {
                Id = endereco.Id,
                PessoaId = endereco.PessoaId,
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                CEP = endereco.CEP
            };

            return Ok(enderecoResponse);
        }



        [HttpDelete("{id:guid}")]
        public IActionResult ApagarEndereco(Guid id)
        {
            var endereco = dbContext.Enderecos.Find(id);
            if (endereco == null)
            {
                return NotFound("Endereço não encontrado.");
            }

            dbContext.Enderecos.Remove(endereco);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
