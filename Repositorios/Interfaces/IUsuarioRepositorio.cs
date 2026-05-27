using MinhaPrimeiraApi.Models;

namespace MinhaPrimeiraApi.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        public Task<List<UsuarioModel>> ListarUsuarios();
        public Task<UsuarioModel> ListarUsuarioPorId(int id);
        public Task<UsuarioModel> CriarUsuario(UsuarioModel usuario);
        public Task<UsuarioModel> AtualizarUsuario(UsuarioModel usuario);
        public Task<bool> ExcluirUsuario(int id);
    }
}
