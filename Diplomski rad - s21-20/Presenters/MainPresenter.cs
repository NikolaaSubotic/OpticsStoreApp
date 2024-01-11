using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplomski_rad___s21_20.Views;
using Diplomski_rad___s21_20.Models;
using Diplomski_rad___s21_20._Repositories;
using System.Windows.Forms;

namespace Diplomski_rad___s21_20.Presenters
{
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;

        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowNaocareView += ShowNaocareView;
            this.mainView.ShowBoldView += ShowBoldView;
            this.mainView.ShowDodaciView += ShowDodaciView;
            this.mainView.ShowRimlessView += ShowRimlessView;
            this.mainView.ShowDecijeView += ShowDecijeView;
            this.mainView.ShowDodaci2View += ShowDodaci2View;

        }

        private void ShowNaocareView(object sender, EventArgs e)
        {
            INaocareView view = NaocareView.GetInstance((MainView)mainView);
            INaocareRepository repository = new NaocareRepository(sqlConnectionString);
            new NaocarePresenter(view, repository);
        }

        private void ShowBoldView(object sender, EventArgs e)
        {
            IBoldView view = BoldView.GetInstance((MainView)mainView);
            IBoldRepository repository = new BoldRepository(sqlConnectionString);
            new BoldPresenter(view, repository);
        }
        private void ShowDodaciView(object sender, EventArgs e)
        {
            IDodaciView view = DodaciView.GetInstance((MainView)mainView);
            IDodaciRepository repository = new DodaciRepository(sqlConnectionString);
            new DodaciPresenter(view, repository);
        }
        private void ShowRimlessView(object sender, EventArgs e)
        {
            IRimlessView view = RimlessView.GetInstance((MainView)mainView);
            IRimlessRepository repository = new RimlessRepository(sqlConnectionString);
            new RimlessPresenter(view, repository);
        }
        private void ShowDecijeView(object sender, EventArgs e)
        {
            IDecijeView view = DecijeView.GetInstance((MainView)mainView);
            IDecijeRepository repository = new DecijeRepository(sqlConnectionString);
            new DecijePresenter(view, repository);
        }
        private void ShowDodaci2View(object sender, EventArgs e)
        {
            IDodaci2View view = Dodaci2View.GetInstance((MainView)mainView);
            IDodaci2Repository repository = new Dodaci2Repository(sqlConnectionString);
            new Dodaci2Presenter(view, repository);
        }
    }

}
