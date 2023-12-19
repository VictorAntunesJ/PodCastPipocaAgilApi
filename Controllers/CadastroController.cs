using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PodCastPipocaAgilApi.Interfaces;
using PodCastPipocaAgilApi.Models;
using PodCastPipocaAgilApi.SendEmail;

namespace PodCastPipocaAgilApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadastroController : ControllerBase
    {
        private readonly ICadastroRepository _cadastroRepository;
        private readonly EmailService _email;

        public CadastroController(ICadastroRepository cadastroRepository, EmailService email)
        {
            _cadastroRepository = cadastroRepository;
            _email = email;
        }

        /// <summary>
        /// Cadastra um novo usuário na aplicação.
        /// </summary>
        /// <param name="cadastro">Dados do novo usuário.</param>
        /// <returns>Dados do usuário recém-cadastrado.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] Cadastro cadastro)
        {
            try
            {
                cadastro.senha = BCrypt.Net.BCrypt.HashPassword(cadastro.senha);
                _cadastroRepository.Insert(cadastro);

                if (EmailService.ValidaEnderecoEmail(cadastro.email))
                {
                    string assunto = "Bem-vindo!";
                    string corpo = $"Olá {cadastro.nome}, obrigado por se cadastrar!";

                    EmailService.EnviarEmail(cadastro.email, assunto, corpo);

                    return Ok(cadastro);
                }
                else
                {
                    return BadRequest("Endereço de e-mail inválido.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro durante o cadastro: {ex.Message}");
            }
        }

        /// <summary>
        /// Lista todos os usuários cadastrados na aplicação.
        /// </summary>
        /// <returns>Lista de todos os usuários cadastrados.</returns>
        [HttpGet]
        public IActionResult ObterTodos()
        {
            try
            {
                var cadastroBanco = _cadastroRepository.GetALL();
                return Ok(cadastroBanco);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na conexão", erro = ex.Message, });
            }
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Dados do usuário correspondente ao ID.</returns>
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var cadastroBanco = _cadastroRepository.GetById(id);
                if (cadastroBanco == null)
                {
                    return NotFound(new { Message = " Cadastro não encrontrado." });
                }
                return Ok(cadastroBanco);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na conexão", erro = ex.Message, });
            }
        }

        /// <summary>
        /// Atualiza um registro de cadastro com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do registro a ser atualizado.</param>
        /// <param name="cadastro">Os dados atualizados para o registro de cadastro.</param>
        /// <returns>
        /// Um objeto indicando o resultado da operação. Retorna:
        /// </returns>
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Cadastro cadastro)
        {
            try
            {
                if (id != cadastro.id)
                {
                    return BadRequest();
                }
                cadastro.senha = BCrypt.Net.BCrypt.HashPassword(cadastro.senha);
                var updateCadastro = _cadastroRepository.Update(id, cadastro);
                return Ok(updateCadastro);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na conexão", erro = ex.Message, });
            }
        }

        /// <summary>
        /// Atualiza parcialmente um registro de cadastro com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do registro a ser atualizado.</param>
        /// <param name="pathCadastro">Os dados parciais para atualização do registro de cadastro.</param>
        /// <returns>
        /// Um objeto indicando o resultado da operação. Retorna:
        /// </returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Cadastro> pathCadastro)
        {
            if (pathCadastro == null)
            {
                return BadRequest();
            }
            var updateCadastro = _cadastroRepository.UpdatePartial(id, pathCadastro);
            return Ok(updateCadastro);
        }

        /// <summary>
        /// Exclui um usuário com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do usuário a ser excluído.</param>
        /// <returns>
        /// Um objeto indicando o resultado da operação. Retorna:
        /// </returns>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool exclusaoBemSucedida = _cadastroRepository.Delete(id);
                if (exclusaoBemSucedida)
                {
                    return Ok("Usuário deletado com sucesso.");
                }
                else
                {
                    return NotFound("Usuário não encontrado com o ID fornecido.");
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na conexão", erro = ex.Message, });
            }
        }
    }
}
