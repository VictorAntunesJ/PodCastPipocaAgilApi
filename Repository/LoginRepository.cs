using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodCastPipocaAgilApi.Context;
using PodCastPipocaAgilApi.Interfaces;
using PodCastPipocaAgilApi.Models;
using BCrypt.Net;

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
            var cadastro = _iLoginContext.Cadastros.FirstOrDefault(x => x.email == email);

            if (cadastro != null)
            {
                bool confere = BCrypt.Net.BCrypt.Verify(senha, cadastro.senha);
                if (confere)
                    return cadastro;
            }
            return null;
        }
    }
}