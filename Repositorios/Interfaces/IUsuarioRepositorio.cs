using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Pagination;

namespace MinhaPrimeiraApi.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        public Task<PagedList<UsuarioModel>> ListarUsuarios(int pageNumber, int pageSize);
        public Task<UsuarioModel> ListarUsuarioPorId(int id);
        public Task<UsuarioModel> CriarUsuario(UsuarioModel usuario);
        public Task<UsuarioModel> AtualizarUsuario(UsuarioModel usuario);
        public Task<bool> ExcluirUsuario(int id);
    }
}
