using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Extensions;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Pagination;
using MinhaPrimeiraApi.Repositorios.Interfaces;

namespace MinhaPrimeiraApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        [HttpGet]
        public async Task<ActionResult<PagedList<UsuarioModel>>> ListarTodos([FromQuery]PaginationParameters paginationParameters)
        {
            var usuarios = await _usuarioRepositorio.ListarUsuarios(paginationParameters.PageNumber, paginationParameters.PageSize);
            Response.AddPagiantionHeader(new PaginationHeader(usuarios.CurrentPage, usuarios.PageSize, usuarios.TotalPages, usuarios.TotalCount));
            return Ok(usuarios);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> ListarPorId(int id)
        {
            UsuarioModel usuario = await _usuarioRepositorio.ListarUsuarioPorId(id);
            return Ok(usuario);
        }
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Adicionar([FromBody] UsuarioModel usuario)
        {
            UsuarioModel novoUsurio = await _usuarioRepositorio.CriarUsuario(usuario);
            return Ok(novoUsurio);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> Editar(int id, [FromBody]UsuarioModel usuario)
        {
            usuario.Id = id;
            UsuarioModel other = await _usuarioRepositorio.AtualizarUsuario(usuario);
            if (other == null)
            {
                return NotFound();
            }
            return Ok(other);
        }
        [HttpDelete("{id}")]
        public async Task <bool> Deletar(int id)
        {
            await _usuarioRepositorio.ExcluirUsuario(id);
            return true;

        }
    }
}
