using System.ComponentModel.DataAnnotations;

namespace PodCastPipocaAgilApi.Models
{
    public class Cadastro
    {
        public int id { get; set; }

        [Required(
            ErrorMessage = "Não foi possível realizar o cadastro. O campo Nome é obrigatório."
        )]
        public string nome { get; set; }

        [Required(
            ErrorMessage = "Não foi possível realizar o cadastro. O campo Senha é obrigatório."
        )]
        public string senha { get; set; }

        [EmailAddress(
            ErrorMessage = "Não foi possível realizar o cadastro. O campo Email deve ser um endereço de e-mail válido."
        )]
        public string email { get; set; }
    }
}
