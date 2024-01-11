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
    public class BoldPresenter
    {
        private IBoldView view;
        private IBoldRepository repository;
        private BindingSource boldBindingSource;
        private IEnumerable<BoldModel> boldList;

        public BoldPresenter(IBoldView view, IBoldRepository repository)
        {
            this.boldBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            this.view.SearchEvent += SearchBold;
            this.view.AddNewEvent += AddNewBold;
            this.view.EditEvent += LoadSelectedBoldToEdit;
            this.view.DeleteEvent += DeleteSelectedBold;
            this.view.SaveEvent += SavehBold;
            this.view.CancelEvent += CancleAction;

            this.view.SetBoldListBindingSource(this.boldBindingSource);
            // Dodajte ovu liniju da inicijalno popunite ComboBox sa tipovima naočara
            PopuniComboBoxSaTipovima();

            LoadAllBoldList();

            this.view.Show();
        }
        public void PopuniComboBoxSaTipovima()
        {
            List<string> tipoviNaočara = repository.GetTipoviNaočara();
            // Pozovite odgovarajuću metodu u vašem View interfejsu da biste popunili ComboBox sa tipovima
            this.view.PopuniComboBoxSaTipovima(tipoviNaočara);
        }

        private void LoadAllBoldList()
        {
            boldList = repository.GetAll();
            boldBindingSource.DataSource = boldList;
        }
        private void SearchBold(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                boldList = repository.GetByValue(this.view.SearchValue);
            else boldList = repository.GetAll();
            boldBindingSource.DataSource = boldList;
        }

        private void CancleAction(object sender, EventArgs e)
        {
            CleanviewFields();
        }

        private void SavehBold(object sender, EventArgs e)
        {
            var model = new BoldModel();
            model.Ime = view.BoldIme;
            model.Boja = view.BoldBoja;
            model.Cena = Convert.ToDecimal(view.BoldCena);
            model.Slika = view.Slika;
            model.TipNaočara = view.SelectedTip;

            try
            {
                new Common.ModelDataValidation().Validate(model);

                if (view.IsEdit)
                {
                    model.Id = Convert.ToInt32(view.BoldId);
                    repository.Edit(model);
                    view.Message = "Bold edited successfully";
                }
                else
                {
                    // Ostavljamo model.Id prazan prilikom dodavanja novog proizvoda
                    repository.Add(model);
                    view.Message = "Bold added successfully";
                }

                view.IsSuccessful = true;
                LoadAllBoldList();
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
            view.BoldId = "0";
            view.BoldIme = "";
            view.BoldBoja = "";
            view.BoldCena = "";
            view.Slika = new byte[0];
        }

        private void DeleteSelectedBold(object sender, EventArgs e)
        {
            try
            {
                var bold = (BoldModel)boldBindingSource.Current;
                repository.Delete(bold.Id);
                view.IsSuccessful = true;
                view.Message = "Bold deleted successfully";
                LoadAllBoldList();
            }
            catch
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete bold";
            }
        }

        private void LoadSelectedBoldToEdit(object sender, EventArgs e)
        {
            var bold = (BoldModel)boldBindingSource.Current;
            view.BoldId = bold.Id.ToString();
            view.BoldIme = bold.Ime;
            view.BoldBoja = bold.Boja;
            view.BoldCena = bold.Cena.ToString();

            // Sačuvajte originalnu sliku u privremenu promenljivu
            byte[] originalSlika = view.Slika;

            view.Slika = bold.Slika;
            view.IsEdit = true;
        }


        private void AddNewBold(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }
    }
}
