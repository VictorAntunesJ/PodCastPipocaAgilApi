using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodCastPipocaAgilApi.Models;

namespace PodCastPipocaAgilApi.Interfaces
{
    public interface ILiveRepository : IGenericRepository<Live>
    {
        ICollection<Live> GetALL();
        Live GetById(int id);
        Live Insert(Live live);
        Live Update(int id, Live live);
        bool Delete(int id);
    }
}
