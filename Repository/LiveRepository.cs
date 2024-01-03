using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PodCastPipocaAgilApi.Context;
using PodCastPipocaAgilApi.Interfaces;
using PodCastPipocaAgilApi.Migrations;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Repository
{
    public class LiveRepository : ILiveRepository
    {
        PodCastPipocaAgilApiContext _contextLive;

        public LiveRepository(PodCastPipocaAgilApiContext contextLive)
        {
            _contextLive = contextLive;
        }

        public ICollection<Live> Search(string termoPesquisa)
        {
            return _contextLive.Lives
                .Where(
                    l =>
                        EF.Functions.Like(l.Titulo, $"%{termoPesquisa}%")
                        || EF.Functions.Like(l.Link, $"%{termoPesquisa}%")
                // Adicione mais condições conforme necessário para outros campos da entidade Live
                )
                .ToList();
        }

        public Live Insert(Live live)
        {
            _contextLive.Add(live);
            _contextLive.SaveChanges();
            return live;
        }

        public ICollection<Live> GetALL()
        {
            var liveBanco = _contextLive.Lives.ToList();
            if (liveBanco == null || liveBanco.Count == 0)
            {
                // Se nenhum usuário foi encontrado, você pode retornar uma lista vazia
                return new List<Live> { new Live { Titulo = "Nenhum usuário encontrado." } };
            }
            return liveBanco;
        }

        public Live GetById(int id)
        {
            return _contextLive.Lives.Find(id);
        }

        public Live Update(int id, Live live)
        {
            var liveBanco = _contextLive.Lives.Find(id);
            if (liveBanco == null)
                throw new Exception("Item não encontrado com o ID fornecido.");
            // 2. Atualizar os campos
            liveBanco.Titulo = live.Titulo;
            liveBanco.Data = live.Data;
            liveBanco.Link = live.Link;
            // 3. Atualizar no contexto e salvar as alterações
            _contextLive.Lives.Update(liveBanco);
            _contextLive.SaveChanges();
            // 6. Retornar o cadastro atualizado
            return liveBanco;
        }

        public bool Delete(int id)
        {
            var liveBanco = _contextLive.Lives.Find(id);
            if (liveBanco == null)
                throw new Exception("Item não encontrado com o ID fornecido.");

            _contextLive.Lives.Remove(liveBanco);
            _contextLive.SaveChanges();
            return true;
        }
    }
}
