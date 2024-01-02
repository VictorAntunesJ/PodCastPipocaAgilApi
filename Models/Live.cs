using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodCastPipocaAgilApi.Models
{
    public class Live
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Data { get; set; }
        public string Link { get; set; }
    }
}
