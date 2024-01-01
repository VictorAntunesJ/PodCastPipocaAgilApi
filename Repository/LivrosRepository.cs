using Microsoft.AspNetCore.Mvc;
using PodCastPipocaAgilApi.Context;
using PodCastPipocaAgilApi.Interfaces;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Repository
{
    public class LivrosRepository : ILivrosRepository
    {
        PodCastPipocaAgilApiContext _contextLivro;

        public LivrosRepository(PodCastPipocaAgilApiContext contextLivro)
        {
            _contextLivro = contextLivro;
        }

        public Livro Insert(Livro livro)
        {
            ValidateTitulo(livro.Titulo);
            ValidateAutor(livro.Autor);
            ValidateAnoPublicacao(livro.AnoPublicacao);

            if (LinkLivroJaCadastrado(livro.LinkLivro))
            {
                throw new ArgumentException("Link do Livro já cadastrado. Escolha outro link.");
            }

            _contextLivro.Livros.Add(livro);
            _contextLivro.SaveChanges();
            return livro;
        }

        private bool LinkLivroJaCadastrado(string linkLivro)
        {
            // Verifica se o link do livro já está cadastrado no contexto de livros
            var existeLivro = _contextLivro.Livros.Any(x => x.LinkLivro == linkLivro);
            return existeLivro;
        }

        private void ValidateTitulo(string titulo)
        {
            if (string.IsNullOrEmpty(titulo) || titulo.Length < 3 || titulo.Length > 50)
            {
                throw new ArgumentException(
                    "Título inválido. Use no mínimo 3 e no máximo 50 caracteres."
                );
            }
        }

        private void ValidateAutor(string autor)
        {
            if (string.IsNullOrEmpty(autor) || autor.Length < 3 || autor.Length > 50)
            {
                throw new ArgumentException(
                    "Autor inválido. Use no mínimo 3 e no máximo 50 caracteres."
                );
            }
        }

        private void ValidateAnoPublicacao(string anoPublicacao)
        {
            if (string.IsNullOrEmpty(anoPublicacao) || !int.TryParse(anoPublicacao, out _))
            {
                throw new ArgumentException(
                    "Ano de Publicação inválido. Certifique-se de que o valor seja um número válido."
                );
            }
        }

        public ICollection<Livro> GetALL()
        {
            var livro = _contextLivro.Livros.ToList();
            if (livro == null || livro.Count == 0)
            {
                return new List<Livro> { new Livro { Titulo = "Nenhum usúario cadastrado." } };
            }
            return livro;
        }

        public Livro GetById(int id)
        {
            var livroBanco = _contextLivro.Livros.Find(id);
            return livroBanco;
        }

        public Livro Update(int id, Livro livro)
        {
            ValidateTitulo(livro.Titulo);
            ValidateAutor(livro.Autor);
            ValidateAnoPublicacao(livro.AnoPublicacao);

            if (LinkLivroJaCadastrado(livro.LinkLivro))
            {
                throw new ArgumentException("Link do Livro já cadastrado. Escolha outro link.");
            }

            if (livro == null)
            {
                throw new ArgumentNullException(nameof(livro), "O livro não pode ser nulo.");
            }

            var livroBanco = _contextLivro.Livros.Find(id);
            if (livroBanco == null)
            {
                throw new Exception("Item não encontrado com o ID fornecido.");
            }
            livroBanco.Titulo = livro.Titulo;
            livroBanco.Autor = livro.Autor;
            livroBanco.AnoPublicacao = livro.AnoPublicacao;
            livroBanco.UrlCapa = livro.UrlCapa;
            livroBanco.LinkLivro = livro.LinkLivro;

            _contextLivro.Livros.Update(livroBanco);
            _contextLivro.SaveChanges();

            return livroBanco;
        }

        public bool Delete(int id)
        {
            var cadastroBanco = _contextLivro.Livros.Find(id);

            if (cadastroBanco == null)
                throw new Exception("Item não encontrado com o ID fornecido.");

            _contextLivro.Livros.Remove(cadastroBanco);
            _contextLivro.SaveChanges();

            return true;
        }
    }
}
