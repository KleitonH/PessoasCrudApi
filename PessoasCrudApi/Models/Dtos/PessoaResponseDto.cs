namespace PessoasCrudApi.Models.Dtos
{
    public class PessoaResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateOnly DataNascimento { get; set; }
        public string Email { get; set; } = string.Empty;
        public List<EnderecoResponseDto> Enderecos { get; set; } = new();
    }
}
