using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diplomski_rad___s21_20.Models;
using Diplomski_rad___s21_20.Views;

namespace Diplomski_rad___s21_20.Presenters
{
    public class DodaciPresenter
    {
        private IDodaciView view;
        private IDodaciRepository repository;
        private BindingSource dodaciBindingSource;
        private IEnumerable<DodaciModel> dodaciList;

        public DodaciPresenter(IDodaciView view, IDodaciRepository repository)
        {
            this.dodaciBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            this.view.SearchEvent += SearchDodaci;
            this.view.AddNewEvent += AddNewDodaci;
            this.view.EditEvent += LoadSelectedDodaciToEdit;
            this.view.DeleteEvent += DeleteSelectedDodaci;
            this.view.SaveEvent += SavehDodaci;
            this.view.CancelEvent += CancleAction;

            this.view.SetDodaciListBindingSource(this.dodaciBindingSource);
            // Dodajte ovu liniju da inicijalno popunite ComboBox sa tipovima naočara
            PopuniComboBoxSaTipovima();

            LoadAllDodaciList();

            this.view.Show();
        }
        public void PopuniComboBoxSaTipovima()
        {
            List<string> tipoviNaočara = repository.GetTipoviNaočara();
            // Pozovite odgovarajuću metodu u vašem View interfejsu da biste popunili ComboBox sa tipovima
            this.view.PopuniComboBoxSaTipovima(tipoviNaočara);
        }

        private void LoadAllDodaciList()
        {
            dodaciList = repository.GetAll();
            dodaciBindingSource.DataSource = dodaciList;
        }
        private void SearchDodaci(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                dodaciList = repository.GetByValue(this.view.SearchValue);
            else dodaciList = repository.GetAll();
            dodaciBindingSource.DataSource = dodaciList;
        }

        private void CancleAction(object sender, EventArgs e)
        {
            CleanviewFields();
        }

        private void SavehDodaci(object sender, EventArgs e)
        {
            var model = new DodaciModel();
            model.Ime = view.DodaciIme;
            model.Boja = view.DodaciBoja;
            model.Cena = Convert.ToDecimal(view.DodaciCena);
            model.Slika = view.Slika;
            model.TipNaočara = view.SelectedTip;

            try
            {
                new Common.ModelDataValidation().Validate(model);

                if (view.IsEdit)
                {
                    model.Id = Convert.ToInt32(view.DodaciId);
                    repository.Edit(model);
                    view.Message = "Naocare edited successfully";
                }
                else
                {
                    // Ostavljamo model.Id prazan prilikom dodavanja novog proizvoda
                    repository.Add(model);
                    view.Message = "Naocare added successfully";
                }

                view.IsSuccessful = true;
                LoadAllDodaciList();
                CleanviewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }


        private void CleanviewFields()
        {
            view.DodaciId = "0";
            view.DodaciIme = "";
            view.DodaciBoja = "";
            view.DodaciCena = "";
            view.Slika = new byte[0];
        }

        private void DeleteSelectedDodaci(object sender, EventArgs e)
        {
            try
            {
                var naocare = (DodaciModel)dodaciBindingSource.Current;
                repository.Delete(naocare.Id);
                view.IsSuccessful = true;
                view.Message = "Dodaci deleted successfully";
                LoadAllDodaciList();
            }
            catch
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete naocare";
            }
        }

        private void LoadSelectedDodaciToEdit(object sender, EventArgs e)
        {
            var naocare = (DodaciModel)dodaciBindingSource.Current;
            view.DodaciId = naocare.Id.ToString();
            view.DodaciIme = naocare.Ime;
            view.DodaciBoja = naocare.Boja;
            view.DodaciCena = naocare.Cena.ToString();

            // Sačuvajte originalnu sliku u privremenu promenljivu
            byte[] originalSlika = view.Slika;

            view.Slika = naocare.Slika;
            view.IsEdit = true;
        }


        private void AddNewDodaci(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }
    }
}
