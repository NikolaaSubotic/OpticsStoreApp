using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski_rad___s21_20.Models
{
    public interface INaocareRepository
    {
        void Add(NaocareModel naocareModel);
        void Edit(NaocareModel naocareModel);
        void Delete(int id);
        IEnumerable<NaocareModel> GetAll();
        IEnumerable<NaocareModel> GetByValue(string value);//Searchs
        List<string> GetTipoviNaočara();

    }
}
