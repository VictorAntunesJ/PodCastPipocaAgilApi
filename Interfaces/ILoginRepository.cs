using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Interfaces
{
    public interface ILoginRepository
    {
        Cadastro Logar(string email, string senha);
    }
}