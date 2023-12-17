using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Interfaces
{
    public interface ILoginRepository
    {
        Cadastro Logar(string email, string senha);
    }
}