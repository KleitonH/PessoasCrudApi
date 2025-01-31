namespace PessoasCrudApi.Models.Dtos
{
    public class AdicionarPessoaDto
    {
        public required string Nome { get; set; }
        public required DateOnly DataNascimento { get; set; }
        public required string Email { get; set; }
    }
}
