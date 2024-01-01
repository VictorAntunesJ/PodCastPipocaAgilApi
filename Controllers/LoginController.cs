using Microsoft.AspNetCore.Mvc;
using PodCastPipocaAgilApi.Interfaces;

namespace PodCastPipocaAgilApi.Controllers
{
    /// <summary>
    /// Controller para autenticação de usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        /// <summary>
        /// Realiza a autenticação do usuário.
        /// </summary>
        /// <param name="email">E-mail do usuário.</param>
        /// <param name="senha">Senha do usuário.</param>
        /// <returns>Token de autenticação.</returns>
        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            var logar = _loginRepository.Logar(email, senha);
            if (logar == null)
                return Unauthorized();
            return Ok(new { token = logar });
        }
    }
}
