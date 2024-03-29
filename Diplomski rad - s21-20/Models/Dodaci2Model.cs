﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Diplomski_rad___s21_20.Models
{
    public class Dodaci2Model
    {
        //Fields
        private int id;
        private string ime;
        private string boja;
        private decimal cena;
        private byte[] slika;

        //Properties - Validations
        [DisplayName("Dodaci2 ID")]
        public int Id { get => id; set => id = value; }
        [DisplayName("Dodaci2 Ime")]
        public string Ime { get => ime; set => ime = value; }
        [DisplayName("Dodaci2 Boja")]
        public string Boja { get => boja; set => boja = value; }
        [DisplayName("Dodaci2 Cena")]
        public decimal Cena { get => cena; set => cena = value; }
        public byte[] Slika { get => slika; set => slika = value; }
        [DisplayName("Tip ID")]
        public string TipNaočara { get; set; } // Dodajte svojstvo za tip naočara
    }
}
