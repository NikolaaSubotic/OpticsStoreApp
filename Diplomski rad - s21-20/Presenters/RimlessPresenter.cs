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
    public class RimlessPresenter
    {
        private IRimlessView view;
        private IRimlessRepository repository;
        private BindingSource rimlessBindingSource;
        private IEnumerable<RimlessModel> rimlessList;

        public RimlessPresenter(IRimlessView view, IRimlessRepository repository)
        {
            this.rimlessBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            this.view.SearchEvent += SearchRimless;
            this.view.AddNewEvent += AddNewRimless;
            this.view.EditEvent += LoadSelectedRimlessToEdit;
            this.view.DeleteEvent += DeleteSelectedRimless;
            this.view.SaveEvent += SavehRimless;
            this.view.CancelEvent += CancleAction;

            this.view.SetRimlessListBindingSource(this.rimlessBindingSource);
            // Dodajte ovu liniju da inicijalno popunite ComboBox sa tipovima naočara
            PopuniComboBoxSaTipovima();

            LoadAllRimlessList();

            this.view.Show();
        }
        public void PopuniComboBoxSaTipovima()
        {
            List<string> tipoviNaočara = repository.GetTipoviNaočara();
            // Pozovite odgovarajuću metodu u vašem View interfejsu da biste popunili ComboBox sa tipovima
            this.view.PopuniComboBoxSaTipovima(tipoviNaočara);
        }

        private void LoadAllRimlessList()
        {
            rimlessList = repository.GetAll();
            rimlessBindingSource.DataSource = rimlessList;
        }
        private void SearchRimless(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                rimlessList = repository.GetByValue(this.view.SearchValue);
            else rimlessList = repository.GetAll();
            rimlessBindingSource.DataSource = rimlessList;
        }

        private void CancleAction(object sender, EventArgs e)
        {
            CleanviewFields();
        }

        private void SavehRimless(object sender, EventArgs e)
        {
            var model = new RimlessModel();
            model.Ime = view.RimlessIme;
            model.Boja = view.RimlessBoja;
            model.Cena = Convert.ToDecimal(view.RimlessCena);
            model.Slika = view.Slika;
            model.TipNaočara = view.SelectedTip;

            try
            {
                new Common.ModelDataValidation().Validate(model);

                if (view.IsEdit)
                {
                    model.Id = Convert.ToInt32(view.RimlessId);
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
                LoadAllRimlessList();
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
            view.RimlessId = "0";
            view.RimlessIme = "";
            view.RimlessBoja = "";
            view.RimlessCena = "";
        }

        private void DeleteSelectedRimless(object sender, EventArgs e)
        {
            try
            {
                var rimless = (RimlessModel)rimlessBindingSource.Current;
                repository.Delete(rimless.Id);
                view.IsSuccessful = true;
                view.Message = "Rimless deleted successfully";
                LoadAllRimlessList();
            }
            catch
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete rimless";
            }
        }

        private void LoadSelectedRimlessToEdit(object sender, EventArgs e)
        {
            var rimless = (RimlessModel)rimlessBindingSource.Current;
            view.RimlessId = rimless.Id.ToString();
            view.RimlessIme = rimless.Ime;
            view.RimlessBoja = rimless.Boja;
            view.RimlessCena = rimless.Cena.ToString();
            view.Slika = rimless.Slika;
            view.IsEdit = true;
        }


        private void AddNewRimless(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }


    }
}
