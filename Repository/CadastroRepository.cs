using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
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
        public Cadastro Insert( Cadastro cadastro)
        {
            _contextCadastro.Add(cadastro);
            _contextCadastro.SaveChanges();
            return cadastro;
        }
        public ICollection<Cadastro> GetALL()
        {
            return _contextCadastro.Cadastros.ToList();
        }

        public Cadastro GetById(int id)
        {
            return _contextCadastro.Cadastros.Find(id);
        }
        public Cadastro Update(int id, Cadastro cadastro)
        {
            var cadastroBanco = _contextCadastro.Cadastros.Find(id);
            if (cadastroBanco == null)
                throw new Exception("Item não encontrado com o ID fornecido.");

            cadastroBanco.nome = cadastro.nome;
            cadastroBanco.email = cadastro.email;
            cadastroBanco.senha = cadastro.senha;

            _contextCadastro.Cadastros.Update(cadastroBanco);
            _contextCadastro.SaveChanges();

            return cadastroBanco;
        }
        public bool Delete(int id)
        {
            var cadastroBanco = _contextCadastro.Cadastros.Find(id);

            if (cadastroBanco == null)
                throw new Exception("Item não encontrado com o ID fornecido.");

            _contextCadastro.Cadastros.Remove(cadastroBanco);
            _contextCadastro.SaveChanges();

            return true;
        }
        

    public Cadastro UpdatePartial(int id, JsonPatchDocument<Cadastro> PatchCadastro)
    {
        var cadastroBanco = _contextCadastro.Cadastros.Find(id);
        if (cadastroBanco == null)
            throw new Exception("Item não encontrado com o ID fornecido.");

        PatchCadastro.ApplyTo(cadastroBanco);

        _contextCadastro.Cadastros.Update(cadastroBanco);
        _contextCadastro.SaveChanges();

        return cadastroBanco;
    }
}
}