using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PodCastPipocaAgilApi.Models
{
    public class Livro
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Campo 'Título' é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo 'Autor' é obrigatório.")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Campo 'Ano de Publicação' é obrigatório.")]
        public string AnoPublicacao { get; set; }

        public string UrlCapa { get; set; }

        [Required(ErrorMessage = "Campo 'Link do Livro' é obrigatório.")]
        public string LinkLivro { get; set; }
    }
}
