using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski_rad___s21_20.Models
{
    public class BoldModel
    {
        //Fields
        private int id;
        private string ime;
        private string boja;
        private decimal cena;
        private byte[] slika;

        //Properties - Validations
        [DisplayName("Bold ID")]
        public int Id { get => id; set => id = value; }
        [DisplayName("Bold Ime")]
        public string Ime { get => ime; set => ime = value; }
        [DisplayName("Bold Boja")]
        public string Boja { get => boja; set => boja = value; }
        [DisplayName("Bold Cena")]
        public decimal Cena { get => cena; set => cena = value; }
        public byte[] Slika { get => slika; set => slika = value; }
        public string TipNaočara { get; set; } // Dodajte svojstvo za tip naočara
    }
}
