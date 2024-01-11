using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Diplomski_rad___s21_20.Models
{
    public class NaocareModel
    {
        // Fields
        private int id;
        private string ime;
        private string boja;
        private decimal cena;
        private byte[] slika;

        // Properties - Validations
        [DisplayName("Naocare ID")]
        public int Id { get => id; set => id = value; }
        [DisplayName("Naocare Ime")]
        [Required(ErrorMessage = "Ime je obavezno.")]
        public string Ime { get => ime; set => ime = value; }
        [DisplayName("Naocare Boja")]
        [Required(ErrorMessage = "Boja je obavezno.")]
        public string Boja { get => boja; set => boja = value; }
        [DisplayName("Naocare Cena")]
        [Required(ErrorMessage = "Cena je obavezno.")]
        public decimal Cena { get => cena; set => cena = value; }
        public byte[] Slika { get => slika; set => slika = value; }
        [DisplayName("Tip ID")]
        [Required(ErrorMessage = "Tip je obavezan.")]
        public string TipNaočara { get; set; }
    }
}
