using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraApi.Data;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Repositorios.Interfaces;

namespace MinhaPrimeiraApi.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public async Task<List<UsuarioModel>> ListarUsuarios()
        {
            return await _bancoContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> ListarUsuarioPorId(int id)
        {
            return await _bancoContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<UsuarioModel> AtualizarUsuario(UsuarioModel usuario)
        {
            UsuarioModel encontrado = await ListarUsuarioPorId(usuario.Id);
            if (encontrado == null)
            {
                throw new Exception();
            }
            encontrado.Nome = usuario.Nome;
            encontrado.Email = usuario.Email;
            _bancoContext.Update(encontrado);
            await _bancoContext.SaveChangesAsync();
            return encontrado;
        }

        public async Task<UsuarioModel> CriarUsuario(UsuarioModel usuario)
        {
            await _bancoContext.Usuarios.AddAsync(usuario);
            await _bancoContext.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> ExcluirUsuario(int id)
        {
            var apagar = await ListarUsuarioPorId(id);
            if (apagar == null)
            {
                throw new Exception();
            }
            _bancoContext.Usuarios.Remove(apagar);
            await _bancoContext.SaveChangesAsync();
            return true;
        }
    }
}
