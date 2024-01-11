using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplomski_rad___s21_20.Views
{
    public interface IDodaci2View
    {
        // Properties - Fields
        string Dodaci2Id { get; set; }
        string Dodaci2Ime { get; set; }
        string Dodaci2Boja { get; set; }
        string Dodaci2Cena { get; set; }

        byte[] Slika { get; set; } // Dodali smo svojstvo za čuvanje slike kao niz bajtova
        List<string> TipoviNaočara { set; }
        string SelectedTip { get; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        // Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler ChooseImageEvent; // Dodali smo događaj za odabir slike

        // Methods
        void SetDodaci2ListBindingSource(BindingSource dodaci2List);
        void Show(); // slika kako bi se ovde uklopila
        void ShowImage(byte[] imageBytes); // Prikazivanje slike u PictureBox kontroli
        void PopuniComboBoxSaTipovima(List<string> tipoviNaočara);
    }
}
