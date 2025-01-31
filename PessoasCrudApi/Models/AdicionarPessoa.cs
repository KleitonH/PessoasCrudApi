namespace PessoasCrudApi.Models
{
    public class AdicionarPessoa
    {
        public required Guid Id { get; set; }
        public required string Nome { get; set; }
        public required DateOnly DataNascimento { get; set; }
        public required string Email { get; set; }
    }
}
