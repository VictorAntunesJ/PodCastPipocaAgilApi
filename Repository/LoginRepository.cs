using System.Text.RegularExpressions;
using PodCastPipocaAgilApi.Context;
using PodCastPipocaAgilApi.Interfaces;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly PodCastPipocaAgilApiContext _iLoginContext;

        public LoginRepository(PodCastPipocaAgilApiContext iLoginContext)
        {
            _iLoginContext = iLoginContext;
        }

        public Cadastro Logar(string email, string senha)
        {
            if (!IsValidEmailFormat(email))
            {
                throw new ArgumentException(
                    "Formato de e-mail inválido. Certifique-se de que o endereço esteja no formato correto."
                );
            }

            var cadastro = _iLoginContext.Cadastros.FirstOrDefault(x => x.email == email);

            if (cadastro != null && BCrypt.Net.BCrypt.Verify(senha, cadastro.senha))
            {
                // Login bem-sucedido
                return cadastro;
            }

            // Login falhou, retorna a mensagem de erro
            return null;
        }

        private bool IsValidEmailFormat(string email)
        {
            // Expressão regular para verificar o formato do e-mail
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$";
            Regex regex = new Regex(emailPattern);

            // Verifica se o e-mail corresponde ao padrão
            return regex.IsMatch(email);
        }
    }
}
