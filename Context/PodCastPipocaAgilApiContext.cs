using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Context
{
    public class PodCastPipocaAgilApiContext : DbContext
    {
        public PodCastPipocaAgilApiContext(DbContextOptions<PodCastPipocaAgilApiContext> options) : base(options)
        {

        }
        public DbSet<Cadastro> Cadastros { get; set; }
    }
}