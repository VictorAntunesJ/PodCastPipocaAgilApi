using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using PodCastPipocaAgilApi.Context;
using PodCastPipocaAgilApi.Interfaces;

namespace PodCastPipocaAgilApi.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly PodCastPipocaAgilApiContext _iLoginContext;
        public LoginRepository(PodCastPipocaAgilApiContext iLoginContext)
        {
            _iLoginContext = iLoginContext;
        }
        public string Logar(string email, string senha)
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
                //Criando Credenciais do JWT

                //Definimos as Claims
                var minhasClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, cadastro.email),
                    new Claim(JwtRegisteredClaimNames.Jti, cadastro.id.ToString()),
                    new Claim(ClaimTypes.Role, "Adm"),
                    new Claim("Cargo", "Adm")
                };
                //Criando as chaves
                var Key = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes("PodCastPipocaAgilDb-chave-autenticacao")
                );
                //Criando credenciais
                var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
                // Gerando token (string)
                var meuToken = new JwtSecurityToken(
                    issuer: "PodCastPipocaAgilDbAPI.Web",
                    audience: "PodCastPipocaAgilDbAPI.Web",
                    claims: minhasClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );
                // Login bem-sucedido
                return new JwtSecurityTokenHandler().WriteToken(meuToken);
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
