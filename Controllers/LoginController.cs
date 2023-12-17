using Microsoft.AspNetCore.Mvc;
using PodCastPipocaAgilApi.Interfaces;

namespace PodCastPipocaAgilApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            var logar = _loginRepository.Logar(email, senha);
            if(logar == null)
                return Unauthorized();

            return Ok(logar);            
        }
    }
}