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
    public class Dodaci2Presenter
    {
        private IDodaci2View view;
        private IDodaci2Repository repository;
        private BindingSource dodaci2BindingSource;
        private IEnumerable<Dodaci2Model> dodaci2List;

        public Dodaci2Presenter(IDodaci2View view, IDodaci2Repository repository)
        {
            this.dodaci2BindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            this.view.SearchEvent += SearchDodaci2;
            this.view.AddNewEvent += AddNewDodaci2;
            this.view.EditEvent += LoadSelectedDodaci2ToEdit;
            this.view.DeleteEvent += DeleteSelectedDodaci2;
            this.view.SaveEvent += SavehDodaci2;
            this.view.CancelEvent += CancleAction;

            this.view.SetDodaci2ListBindingSource(this.dodaci2BindingSource);
            // Dodajte ovu liniju da inicijalno popunite ComboBox sa tipovima naočara
            PopuniComboBoxSaTipovima();

            LoadAllDodaci2List();

            this.view.Show();
        }
        public void PopuniComboBoxSaTipovima()
        {
            List<string> tipoviNaočara = repository.GetTipoviNaočara();
            // Pozovite odgovarajuću metodu u vašem View interfejsu da biste popunili ComboBox sa tipovima
            this.view.PopuniComboBoxSaTipovima(tipoviNaočara);
        }

        private void LoadAllDodaci2List()
        {
            dodaci2List = repository.GetAll();
            dodaci2BindingSource.DataSource = dodaci2List;
        }
        private void SearchDodaci2(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                dodaci2List = repository.GetByValue(this.view.SearchValue);
            else dodaci2List = repository.GetAll();
            dodaci2BindingSource.DataSource = dodaci2List;
        }

        private void CancleAction(object sender, EventArgs e)
        {
            CleanviewFields();
        }

        private void SavehDodaci2(object sender, EventArgs e)
        {
            var model = new Dodaci2Model();
            model.Ime = view.Dodaci2Ime;
            model.Boja = view.Dodaci2Boja;
            model.Cena = Convert.ToDecimal(view.Dodaci2Cena);
            model.Slika = view.Slika;
            model.TipNaočara = view.SelectedTip;

            try
            {
                new Common.ModelDataValidation().Validate(model);

                if (view.IsEdit)
                {
                    model.Id = Convert.ToInt32(view.Dodaci2Id);
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
                LoadAllDodaci2List();
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
            view.Dodaci2Id = "0";
            view.Dodaci2Ime = "";
            view.Dodaci2Boja = "";
            view.Dodaci2Cena = "";
            view.Slika = new byte[0];
        }

        private void DeleteSelectedDodaci2(object sender, EventArgs e)
        {
            try
            {
                var dodaci2 = (Dodaci2Model)dodaci2BindingSource.Current;
                repository.Delete(dodaci2.Id);
                view.IsSuccessful = true;
                view.Message = "Dodaci2 deleted successfully";
                LoadAllDodaci2List();
            }
            catch
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete dodaci2";
            }
        }

        private void LoadSelectedDodaci2ToEdit(object sender, EventArgs e)
        {
            var dodaci2 = (Dodaci2Model)dodaci2BindingSource.Current;
            view.Dodaci2Id = dodaci2.Id.ToString();
            view.Dodaci2Ime = dodaci2.Ime;
            view.Dodaci2Boja = dodaci2.Boja;
            view.Dodaci2Cena = dodaci2.Cena.ToString();

            // Sačuvajte originalnu sliku u privremenu promenljivu
            byte[] originalSlika = view.Slika;

            view.Slika = dodaci2.Slika;
            view.IsEdit = true;
        }


        private void AddNewDodaci2(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }


    }
}
