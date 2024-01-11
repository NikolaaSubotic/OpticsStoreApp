using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski_rad___s21_20.Models
{
    public interface IDecijeRepository
    {
        void Add(DecijeModel decijeModel);
        void Edit(DecijeModel decijeModel);
        void Delete(int id);
        IEnumerable<DecijeModel> GetAll();
        IEnumerable<DecijeModel> GetByValue(string value);//Searchs
        List<string> GetTipoviNaočara();

    }
}
