// using PodCastPipocaAgilApi.Migrations;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Interfaces
{
    public interface ILivrosRepository
    {
        ICollection<Livro> GetALL();
        Livro GetById(int id);
        Livro Insert(Livro livro);
        Livro Update(int id, Livro livro);
        bool Delete(int id);
    }
}
