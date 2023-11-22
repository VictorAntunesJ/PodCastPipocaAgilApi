using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Interfaces
{
    public interface ICadastroRepository
    {
        Cadastro Inserir(Cadastro cadastro);
        ICollection<Cadastro> ListarTodos();
        Cadastro BuscarPorId(int id);
        void Alterar(Cadastro cadastro);
        void Excluir(Cadastro cadastro);
    }
}