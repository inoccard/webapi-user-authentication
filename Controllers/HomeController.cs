using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Entities;
using ShopAPI.Repository;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authentication([FromBody] User user)
        {
            var _user = UserRepository.GetUser(user.Username, user.Password);

            if (user is null)
                return NotFound(new { message = "USuário ou senha inválidos" });

            var token = TokenService.GerenateToken(user);
            user.Password = null;

            /// Já retorna o usuário para o front,
            /// para não precisar fazer outro get novamente
            return new { user = user, token = token };
        }

        [HttpGet("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet("authenticated")]
        [Authorize] // somente usuários autenticados
        public string Authenticated() => string.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet("employee")]
        [Authorize(Roles = "employee, manager")]
        public string Employee() => "Funcionário";

        [HttpGet("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";
    }
}