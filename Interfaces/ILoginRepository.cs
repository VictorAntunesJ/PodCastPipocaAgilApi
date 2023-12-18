using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Interfaces
{
    public interface ILoginRepository
    {
        string Logar(string email, string senha);
    }
}