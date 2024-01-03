using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodCastPipocaAgilApi.Interfaces
{
    public interface IGenericRepository<T>
    {
        ICollection<T> Search(string termoPesquisa);
    }
}
