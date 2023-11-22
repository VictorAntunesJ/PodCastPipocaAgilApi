using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PodCastPipocaAgilApi.Models
{
    public class Cadastro
    {
        public int id { get; set; }
        
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Nome deve ter no máximo 50 caracteres.")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A Senha deve ter entre 6 e 20 caracteres.")]
        public string senha { get; set; }

        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de e-mail válido.")]
        public string email { get; set; }        
    }
}