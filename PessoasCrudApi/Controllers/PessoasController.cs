using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PessoasCrudApi.Data;
using PessoasCrudApi.Models.Dtos;
using PessoasCrudApi.Models.Entities;

namespace PessoasCrudApi.Controllers
{
    [Route("pessoas")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly AplicationDbContext dbContext;

        public PessoasController(AplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult ListarPessoas()
        {
            var listaPessoas = dbContext.Pessoas
                .Include(p => p.Enderecos) // Inclui os endereços relacionados
                .ToList();

            var listaPessoasDto = listaPessoas.Select(p => new PessoaResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                DataNascimento = p.DataNascimento,
                Email = p.Email,
                Enderecos = p.Enderecos.Select(e => new EnderecoResponseDto
                {
                    Id = e.Id,
                    Logradouro = e.Logradouro,
                    Cidade = e.Cidade,
                    Estado = e.Estado,
                    CEP = e.CEP
                }).ToList()
            }).ToList();

            return Ok(listaPessoasDto);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetPessoaPorId(Guid id)
        {
            var pessoa = dbContext.Pessoas
                                   .Include(p => p.Enderecos)
                                   .FirstOrDefault(p => p.Id == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            var pessoaResponseDto = new PessoaResponseDto
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                DataNascimento = pessoa.DataNascimento,
                Email = pessoa.Email,
                Enderecos = pessoa.Enderecos.Select(e => new EnderecoResponseDto
                {
                    Id = e.Id,
                    Logradouro = e.Logradouro,
                    Cidade = e.Cidade,
                    Estado = e.Estado,
                    CEP = e.CEP
                }).ToList()
            };

            return Ok(pessoaResponseDto);
        }



        [HttpPost]
        public IActionResult AdicionarPessoa(AdicionarPessoaDto adicionarPessoaDto)
        {
            var pessoaEntity = new Pessoa()
            {
                Nome = adicionarPessoaDto.Nome,
                DataNascimento = adicionarPessoaDto.DataNascimento,
                Email = adicionarPessoaDto.Email
            };

            dbContext.Pessoas.Add(pessoaEntity);
            dbContext.SaveChanges();

            return Ok(pessoaEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePessoa(Guid id, [FromBody] AtualizarPessoaDto atualizarPessoaDto)
        {
            if (atualizarPessoaDto == null)
            {
                return BadRequest();
            }

            var existingPessoa = dbContext.Pessoas.FirstOrDefault(p => p.Id == id);
            if (existingPessoa == null)
            {
                return NotFound();
            }

            // Atualizar dados da pessoa com os valores do DTO
            existingPessoa.Nome = atualizarPessoaDto.Nome;
            existingPessoa.Email = atualizarPessoaDto.Email;
            existingPessoa.DataNascimento = atualizarPessoaDto.DataNascimento;

            dbContext.SaveChanges();

            return Ok(existingPessoa);
        }


        // Método para excluir uma pessoa
        [HttpDelete("{id}")]
        public IActionResult DeletePessoa(Guid id)
        {
            var pessoa = dbContext.Pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            // Excluir a pessoa
            dbContext.Pessoas.Remove(pessoa);

            // Se a pessoa tiver um endereço, excluir também
            var endereco = dbContext.Enderecos.FirstOrDefault(e => e.PessoaId == id);
            if (endereco != null)
            {
                dbContext.Enderecos.Remove(endereco);
            }

            dbContext.SaveChanges();

            return Ok(new { message = "Pessoa e endereço (se houver) excluídos com sucesso." });
        }
    }
}
