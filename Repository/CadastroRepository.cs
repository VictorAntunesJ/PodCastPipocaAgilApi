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
        public Cadastro Insert(Cadastro cadastro)
        {
            ValidateNome(cadastro.nome);
            ValidateEmail(cadastro.email);
            ValidateSenha(cadastro.senha);

            _contextCadastro.Add(cadastro);
            _contextCadastro.SaveChanges();
            return cadastro;
        }

        private void ValidateNome(string nome)
        {
            if (string.IsNullOrEmpty(nome) || nome.Length < 3 || nome.Length > 50 || !nome.All(char.IsLetter))
            {
                throw new ArgumentException("Nome inválido. Use apenas letras, com no mínimo 3 e no máximo 50 caracteres.");
            }
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || email.Length > 255 || !IsValidEmailFormat(email))
            {
                throw new ArgumentException("E-mail inválido. Certifique-se de que o endereço esteja no formato correto.");
            }
        }

        private void ValidateSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha) || senha.Length < 8)
            {
                throw new ArgumentException("Senha inválida. Certifique-se de que a senha tenha pelo menos 8 caracteres.");
            }
        }

        private bool IsValidEmailFormat(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
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
            {
                throw new Exception("Item não encontrado com o ID fornecido.");
            }

            
            if (!string.IsNullOrEmpty(cadastro.email) && !IsValidEmailFormat(cadastro.email))
            {
                throw new Exception("Formato de e-mail inválido.");
            }

            // 3. Verificar a unicidade do novo e-mail (se fornecido)
            if (!string.IsNullOrEmpty(cadastro.email) && _contextCadastro.Cadastros.Any(c => c.email == cadastro.email && c.id != id))
            {
                throw new Exception("E-mail já está sendo utilizado por outro usuário.");
            }

            // 4. Atualizar os campos
            cadastroBanco.nome = cadastro.nome;
            cadastroBanco.email = cadastro.email;
            cadastroBanco.senha = cadastro.senha; // Lembre-se de verificar a complexidade se a senha estiver sendo alterada

            // 5. Atualizar no contexto e salvar as alterações
            _contextCadastro.Cadastros.Update(cadastroBanco);
            _contextCadastro.SaveChanges();

            // 6. Retornar o cadastro atualizado
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