using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski_rad___s21_20.Models
{
    public interface IDodaciRepository
    {
        void Add(DodaciModel dodaciModel);
        void Edit(DodaciModel dodaciModel);
        void Delete(int id);
        IEnumerable<DodaciModel> GetAll();
        IEnumerable<DodaciModel> GetByValue(string value);//Searchs
        List<string> GetTipoviNaočara();
    }
}
