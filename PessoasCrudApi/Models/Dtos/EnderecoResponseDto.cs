using System.Globalization;

namespace PessoasCrudApi.Models.Dtos
{
    public class EnderecoResponseDto
    {
            public Guid Id { get; set; }
            public Guid PessoaId { get; set; }
            public required string Logradouro { get; set; }
            public required string Cidade { get; set; }
            public required string Estado { get; set; }
            public required string CEP { get; set; }

    }
}
