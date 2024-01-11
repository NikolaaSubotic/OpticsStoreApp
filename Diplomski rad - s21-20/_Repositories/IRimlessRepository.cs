using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski_rad___s21_20.Models
{
    public interface IRimlessRepository
    {
        void Add(RimlessModel rimlessModel);
        void Edit(RimlessModel rimlessModel);
        void Delete(int id);
        IEnumerable<RimlessModel> GetAll();
        IEnumerable<RimlessModel> GetByValue(string value);//Searchs
        List<string> GetTipoviNaočara();

    }
}
