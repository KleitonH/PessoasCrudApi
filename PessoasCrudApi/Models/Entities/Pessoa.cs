using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PessoasCrudApi.Models.Entities
{
    public class Pessoa
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required DateOnly DataNascimento { get; set; }
        public required string Email { get; set; }

        // Adicionando a lista de endereços vinculados
        public List<Endereco> Enderecos { get; set; } = new();
    }
}
