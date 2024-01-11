using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplomski_rad___s21_20.Views
{
    public interface IBoldView
    {
        // Properties - Fields
        string BoldId { get; set; }
        string BoldIme { get; set; }
        string BoldBoja { get; set; }
        string BoldCena { get; set; }

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
        void SetBoldListBindingSource(BindingSource boldList);
        void Show(); // slika kako bi se ovde uklopila
        void ShowImage(byte[] imageBytes); // Prikazivanje slike u PictureBox kontroli
        void PopuniComboBoxSaTipovima(List<string> tipoviNaočara);
    }
}
