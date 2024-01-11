using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski_rad___s21_20.Models
{
    public interface IDodaci2Repository
    {
        void Add(Dodaci2Model dodaci2Model);
        void Edit(Dodaci2Model dodaci2Model);
        void Delete(int id);
        IEnumerable<Dodaci2Model> GetAll();
        IEnumerable<Dodaci2Model> GetByValue(string value);//Searchs
        List<string> GetTipoviNaočara();

    }
}
