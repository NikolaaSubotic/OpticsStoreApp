using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplomski_rad___s21_20.Views
{
    public interface IRimlessView
    {
        // Properties - Fields
        string RimlessId { get; set; }
        string RimlessIme { get; set; }
        string RimlessBoja { get; set; }
        string RimlessCena { get; set; }

        byte[] Slika { get; set; }
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
        event EventHandler ChooseImageEvent;

        // Methods
        void SetRimlessListBindingSource(BindingSource rimlessList);
        void Show();
        void ShowImage(byte[] imageBytes); // Prikazivanje slike u PictureBox kontroli
        void PopuniComboBoxSaTipovima(List<string> tipoviNaočara);
    }
}
