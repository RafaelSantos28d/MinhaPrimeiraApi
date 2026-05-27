using MinhaPrimeiraApi.Integracao.Response;

namespace MinhaPrimeiraApi.Integracao.Interfaces
{
    public interface IViaCepIntegracao
    {
        Task<ViaCepResponse> ObterDadosViaCep(string cep);
    }
}
