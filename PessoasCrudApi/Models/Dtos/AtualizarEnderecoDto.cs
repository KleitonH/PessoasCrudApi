namespace PessoasCrudApi.Models.Dtos
{
    public class AtualizarEnderecoDto
    {
        public Guid PessoaId { get; set; }
        public required string Logradouro { get; set; }
        public required string Cidade { get; set; }
        public required string Estado { get; set; }
        public required string CEP { get; set; }
    }
}
