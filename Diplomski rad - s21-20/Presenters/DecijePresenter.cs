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
    public class DecijePresenter
    {
        private IDecijeView view;
        private IDecijeRepository repository;
        private BindingSource decijeBindingSource;
        private IEnumerable<DecijeModel> decijeList;

        public DecijePresenter(IDecijeView view, IDecijeRepository repository)
        {
            this.decijeBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            this.view.SearchEvent += SearchDecije;
            this.view.AddNewEvent += AddNewDecije;
            this.view.EditEvent += LoadSelectedDecijeToEdit;
            this.view.DeleteEvent += DeleteSelectedDecije;
            this.view.SaveEvent += SavehDecije;
            this.view.CancelEvent += CancleAction;

            this.view.SetDecijeListBindingSource(this.decijeBindingSource);
            // Dodajte ovu liniju da inicijalno popunite ComboBox sa tipovima naočara
            PopuniComboBoxSaTipovima();

            LoadAllDecijeList();

            this.view.Show();
        }
        public void PopuniComboBoxSaTipovima()
        {
            List<string> tipoviNaočara = repository.GetTipoviNaočara();
            // Pozovite odgovarajuću metodu u vašem View interfejsu da biste popunili ComboBox sa tipovima
            this.view.PopuniComboBoxSaTipovima(tipoviNaočara);
        }

        private void LoadAllDecijeList()
        {
            decijeList = repository.GetAll();
            decijeBindingSource.DataSource = decijeList;
        }
        private void SearchDecije(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                decijeList = repository.GetByValue(this.view.SearchValue);
            else decijeList = repository.GetAll();
            decijeBindingSource.DataSource = decijeList;
        }

        private void CancleAction(object sender, EventArgs e)
        {
            CleanviewFields();
        }

        private void SavehDecije(object sender, EventArgs e)
        {
            var model = new DecijeModel();
            model.Ime = view.DecijeIme;
            model.Boja = view.DecijeBoja;
            model.Cena = Convert.ToDecimal(view.DecijeCena);
            model.Slika = view.Slika;
            model.TipNaočara = view.SelectedTip;

            try
            {
                new Common.ModelDataValidation().Validate(model);

                if (view.IsEdit)
                {
                    model.Id = Convert.ToInt32(view.DecijeId);
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
                LoadAllDecijeList();
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
            view.DecijeId = "0";
            view.DecijeIme = "";
            view.DecijeBoja = "";
            view.DecijeCena = "";
            view.Slika = new byte[0];
        }

        private void DeleteSelectedDecije(object sender, EventArgs e)
        {
            try
            {
                var decije = (DecijeModel)decijeBindingSource.Current;
                repository.Delete(decije.Id);
                view.IsSuccessful = true;
                view.Message = "Decije deleted successfully";
                LoadAllDecijeList();
            }
            catch
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete decije";
            }
        }

        private void LoadSelectedDecijeToEdit(object sender, EventArgs e)
        {
            var decije = (DecijeModel)decijeBindingSource.Current;
            view.DecijeId = decije.Id.ToString();
            view.DecijeIme = decije.Ime;
            view.DecijeBoja = decije.Boja;
            view.DecijeCena = decije.Cena.ToString();

            // Sačuvajte originalnu sliku u privremenu promenljivu
            byte[] originalSlika = view.Slika;

            view.Slika = decije.Slika;
            view.IsEdit = true;
        }


        private void AddNewDecije(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }


    }
}
