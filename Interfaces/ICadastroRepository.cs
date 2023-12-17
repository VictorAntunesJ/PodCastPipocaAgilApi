using Microsoft.AspNetCore.JsonPatch;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Interfaces
{
    public interface ICadastroRepository
    {
        ICollection<Cadastro> GetALL();
        Cadastro GetById(int id);
        Cadastro Insert( Cadastro cadastro);
        Cadastro Update (int id, Cadastro cadastro);
        bool Delete (int id);
        Cadastro UpdatePartial(int id, JsonPatchDocument<Cadastro> PatchCadastro);
    }
}