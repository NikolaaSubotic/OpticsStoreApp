using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski_rad___s21_20.Views
{
    public interface IMainView
    {
        event EventHandler ShowNaocareView;
        event EventHandler ShowBoldView;
        event EventHandler ShowDodaciView;
        event EventHandler ShowRimlessView;
        event EventHandler ShowDecijeView;
        event EventHandler ShowDodaci2View;
        event EventHandler ShowKorpaView;
    }
}
