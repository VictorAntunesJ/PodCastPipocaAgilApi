using Microsoft.AspNetCore.Mvc;
using PodCastPipocaAgilApi.Interfaces;


namespace PodCastPipocaAgilApi.Controllers
{
    /// <summary>
    /// Controller para realizar pesquisas em diferentes entidades.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PesquisaController : ControllerBase
    {
        private readonly ILivrosRepository _livrosRepository;
        private readonly ILiveRepository _liveRepository;

        /// <summary>
        /// Construtor da classe PesquisaController.
        /// </summary>
        /// <param name="livrosRepository">Repositório de livros.</param>
        /// <param name="liveRepository">Repositório de lives.</param>
        public PesquisaController(
            ILivrosRepository livrosRepository,
            ILiveRepository liveRepository
        )
        {
            _livrosRepository = livrosRepository;
            _liveRepository = liveRepository;
        }

        /// <summary>
        /// Pesquisa entidades com base no termo fornecido.
        /// </summary>
        /// <param name="termo">O termo de pesquisa.</param>
        /// <returns>
        /// Retorna uma lista de entidades que correspondem aos critérios de pesquisa.
        /// Retorna BadRequest se o parâmetro for inválido ou se ocorrer uma falha na conexão.
        /// Retorna StatusCode 500 se ocorrer uma falha na conexão.
        /// </returns>
        [HttpGet("")]
        public IActionResult Pesquisar(string termo)
        {
            try
            {
                if (string.IsNullOrEmpty(termo))
                {
                    return BadRequest("O parâmetro termo é obrigatório.");
                }

                var livros = _livrosRepository.Search(termo);
                var lives = _liveRepository.Search(termo);

                var resultado = new
                {
                    Livros = livros,
                    Lives = lives
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na conexão", erro = ex.Message });
            }
        }
    }
}
