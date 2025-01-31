using System.ComponentModel.DataAnnotations;

namespace PessoasCrudApi.Models.Entities
{
    public class Pessoa
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required DateOnly DataNascimento { get; set; }
        public required string Email { get; set; }
    }
}
