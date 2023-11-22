using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PodCastPipocaAgilApi.Interfaces;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadastroController : ControllerBase
    {
        private readonly ICadastroRepository _cadastroRepository;
        public CadastroController(ICadastroRepository cadastroRepository)
        {
            _cadastroRepository = cadastroRepository;
        }

        /// <summary>
        /// Cadastrar um usuario na aplicação.
        /// </summary>
        /// <param name="cadastro">Dados do usuário.</param>
        /// <returns>Dados do usuário cadastrado.</returns>
        [HttpPost]
        public IActionResult Create(Cadastro cadastro)
        {
            try
            {
                var cadastroBanco = _cadastroRepository.Inserir(cadastro);
                return Ok(cadastroBanco);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
        }
    }
}