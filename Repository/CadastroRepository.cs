using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodCastPipocaAgilApi.Context;
using PodCastPipocaAgilApi.Interfaces;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Repository
{
    public class CadastroRepository : ICadastroRepository
    {
        PodCastPipocaAgilApiContext _contextCadastro;
        public CadastroRepository(PodCastPipocaAgilApiContext contextCadastro)
        {
            _contextCadastro = contextCadastro;
        }

        public Cadastro Inserir(Cadastro cadastro)
        {
            _contextCadastro.Add(cadastro);
            _contextCadastro.SaveChanges();
            return cadastro;
        }


        public void Alterar(Cadastro cadastro)
        {
            throw new NotImplementedException();
        }

        public Cadastro BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Cadastro cadastro)
        {
            throw new NotImplementedException();
        }

        

        public ICollection<Cadastro> ListarTodos()
        {
            throw new NotImplementedException();
        }
    }
}