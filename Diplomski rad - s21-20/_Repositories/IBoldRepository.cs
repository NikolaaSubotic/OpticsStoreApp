using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski_rad___s21_20.Models
{
    public interface IBoldRepository
    {
        void Add(BoldModel boldModel);
        void Edit(BoldModel boldModel);
        void Delete(int id);
        IEnumerable<BoldModel> GetAll();
        IEnumerable<BoldModel> GetByValue(string value);//Searchs
        List<string> GetTipoviNaočara();
    }
}
