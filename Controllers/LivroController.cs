using Api_SistemaCursosDistancia.Utils;
using Microsoft.AspNetCore.Mvc;
using PodCastPipocaAgilApi.Interfaces;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivrosRepository _livroRepository;

        public LivroController(ILivrosRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        /// <summary>
        /// Cadastrar Livro na aplicação.
        /// </summary>
        /// <param name="livro"> Dados do livro.</param>
        /// <param name="arquivo">Todas informações do Livro.</param>
        /// <returns>>Dados do Livro cadastrado.</returns>

        [HttpPost]
        public IActionResult Create([FromForm] Livro livro, IFormFile arquivo)
        {
            try
            {
                #region Upload de Imagem
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };

                // Verifica se tanto o arquivo quanto a UrlCapa estão vazios
                if (arquivo == null && string.IsNullOrEmpty(livro.UrlCapa))
                {
                    return BadRequest("Arquivo e UrlCapa não podem estar vazios ao mesmo tempo.");
                }

                // Se há um arquivo, realiza o upload
                if (arquivo != null)
                {
                    string uploadResultado = Upload.UploadFile(
                        arquivo,
                        extensoesPermitidas,
                        "Urls"
                    );

                    if (string.IsNullOrEmpty(uploadResultado))
                    {
                        return BadRequest("Arquivo não encontrado ou extensão não permitida");
                    }

                    livro.UrlCapa = uploadResultado;

                    if (string.IsNullOrEmpty(livro.UrlCapa))
                    {
                        // Se a UrlCapa estiver vazia após o upload, a imagem não foi carregada corretamente
                        return BadRequest("Falha ao carregar a imagem. A UrlCapa é obrigatória.");
                    }
                }
                else
                {
                    // Se não há um arquivo, a UrlCapa também deve estar preenchida
                    if (string.IsNullOrEmpty(livro.UrlCapa))
                    {
                        return BadRequest("Se não há arquivo, a UrlCapa é obrigatória.");
                    }
                }
                #endregion

                // Verifica se o livro tem pelo menos uma propriedade preenchida
                if (
                    string.IsNullOrEmpty(livro.Titulo)
                    && string.IsNullOrEmpty(livro.Autor)
                    && livro.AnoPublicacao == "" //Aqui retornava um int ( 0 )
                    && string.IsNullOrEmpty(livro.LinkLivro)
                )
                {
                    return BadRequest("Pelo menos uma propriedade do livro deve ser preenchida.");
                }

                // Insere no banco de dados somente se pelo menos uma propriedade estiver preenchida
                _livroRepository.Insert(livro);

                return Ok(livro);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na conexão", erro = ex.Message });
            }
        }

        /// <summary>
        /// Listar todos os dados da aplicação.
        /// </summary>
        /// <returns>Todos livros que foram cadastrados.</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var livro = _livroRepository.GetALL().ToList();
                return Ok(livro);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na conexão", erro = ex.Message, });
            }
        }

        /// <summary>
        /// Obtém um llivro pelo ID.
        /// </summary>
        /// <param name="id">ID do livro.</param>
        /// <returns>Dados do livro correspondente ao ID.</returns>
        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            try
            {
                var livroBanco = _livroRepository.GetById(id);
                return Ok(livroBanco);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na conexão", erro = ex.Message, });
            }
        }

        /// <summary>
        /// Listar os 4 últimos livros cadastrados.
        /// </summary>
        /// <returns>Os 4 últimos livros cadastrados.</returns>
        [HttpGet("UltimosQuatro")]
        public IActionResult ListarUltimosQuatro()
        {
            try
            {
                // Obtém todos os livros ordenados pela data de cadastro de forma decrescente
                var livros = _livroRepository
                    .GetALL()
                    .OrderByDescending(x => x.AnoPublicacao)
                    .Take(4)
                    .ToList();
                return Ok(livros);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na conexão", erro = ex.Message, });
            }
        }

        /// <summary>
        /// Atualiza um registro de livro com base no ID fornecido.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="livro">Os dados atualizados para o registro do livro.</param>
        /// <param name="arquivo"></param>
        /// <returns>
        /// Um objeto indicando o resultado da operação. Retorna:
        /// </returns>
        [HttpPut]
        public IActionResult Update(int id, [FromForm] Livro livro, IFormFile arquivo)
        {
            try
            {
                #region Upload de Imagem
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extenção não permitida");
                }

                livro.UrlCapa = uploadResultado;
                #endregion

                var livroBanco = _livroRepository.Update(id, livro);

                if (livroBanco == null)
                {
                    return NotFound($"Livro não encontrado com o ID: {id}.");
                }

                return Ok(livroBanco);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { msg = "Falha na Conexão!", erro = ex.Message, });
            }            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool exclusaoBemSucedida = _livroRepository.Delete(id);
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
