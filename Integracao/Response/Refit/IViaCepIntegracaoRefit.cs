using Refit;

namespace MinhaPrimeiraApi.Integracao.Response.Refit
{
    public interface IViaCepIntegracaoRefit
    {
        [Get("/ws/{cep}/json")]
        Task<ApiResponse<ViaCepResponse>> ObterDadosViaCep(string cep);

    }
}
