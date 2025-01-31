using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PessoasCrudApi.Data;
using PessoasCrudApi.Models.Dtos;
using PessoasCrudApi.Models.Entities;

namespace PessoasCrudApi.Controllers
{
    [Route("api/[controller]")]
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
            var listaPessoas = dbContext.Pessoas.ToList();

            return Ok(listaPessoas);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult ListarPessoaPorId(Guid id)
        {
            var pessoa = dbContext.Pessoas.Find(id);
            if (pessoa is null)
            {
                return NotFound();
            }
            return Ok(pessoa);
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

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult AtualizarPessoa(Guid id, AtualizarPessoaDto atualizarPessoaDto)
        {
            var pessoa = dbContext.Pessoas.Find(id);
            if (pessoa is null)
            {
                return NotFound();
            }

            pessoa.Nome = atualizarPessoaDto.Nome;
            pessoa.DataNascimento = atualizarPessoaDto.DataNascimento;
            pessoa.Email = atualizarPessoaDto.Email;

            dbContext.SaveChanges();
            return Ok(pessoa);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult ApagarPessoa(Guid id)
        {
            var pessoa = dbContext.Pessoas.Find(id);
            if (pessoa is null)
            {
                return NotFound();
            }

            dbContext.Pessoas.Remove(pessoa);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
 