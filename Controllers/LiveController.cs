using Microsoft.AspNetCore.Mvc;
using PodCastPipocaAgilApi.Interfaces;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LiveController : ControllerBase
    {
        private readonly ILiveRepository _liveRepository;

        public LiveController(ILiveRepository liveRepository)
        {
            _liveRepository = liveRepository;
        }

        /// <summary>
        /// Cadastrar uma nova live na aplicação.
        /// </summary>
        /// <param name="live">Dados da live.</param>
        /// <returns>Dados da live recém-cadastrado.</returns>
        [HttpPost]
        public IActionResult Creat(Live live)
        {
            try
            {
                _liveRepository.Insert(live);
                return Ok(live);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro durante o cadastro: {ex.Message}");
            }
        }

        /// <summary>
        /// Listar todas lives da aplicaçao.
        /// </summary>
        /// <param>Dados da live.</param>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var liveBanco = _liveRepository.GetALL().ToList();
                return Ok(liveBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro durante o cadastro: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém uma live pelo ID fornecido.
        /// </summary>
        /// <param name="id">ID da live.</param>
        /// <returns>Dados da live correspondente ao ID.</returns>
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var liveBanco = _liveRepository.GetById(id);
                if (liveBanco == null)
                {
                    return NotFound(new { Message = " Cadastro não encrontrado." });
                }
                return Ok(liveBanco);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na conexão", erro = ex.Message, });
            }
        }

        /// <summary>
        /// Atualizar um registro da live com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID da live a ser atualizado.</param>
        /// <param name="live">Os dados atualizados para o registro de live.</param>
        /// <returns>
        /// Um objeto indicando o resultado da operação. Retorna:
        /// </returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Live live)
        {
            try
            {
                var updateLive = _liveRepository.Update(id, live);
                return Ok(updateLive);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na conexão", erro = ex.Message });
            }
        }

        /// <summary>
        /// Exclui um live com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID da live a ser excluído.</param>
        /// <returns>
        /// Um objeto indicando o resultado da operação. Retorna:
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool exclusaoBemSucedida = _liveRepository.Delete(id);
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
