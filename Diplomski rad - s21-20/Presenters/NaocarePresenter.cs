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
    public class NaocarePresenter
    {
        private INaocareView view;
        private INaocareRepository repository;
        private BindingSource naocareBindingSource;
        private IEnumerable<NaocareModel> naocareList;

        public NaocarePresenter(INaocareView view, INaocareRepository repository)
        {
            this.naocareBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            this.view.SearchEvent += SearchNaocare;
            this.view.AddNewEvent += AddNewNaocare;
            this.view.EditEvent += LoadSelectedNaocareToEdit;
            this.view.DeleteEvent += DeleteSelectedNaocare;
            this.view.SaveEvent += SavehNaocare;
            this.view.CancelEvent += CancleAction;

            this.view.SetNaocareListBindingSource(this.naocareBindingSource);
            PopuniComboBoxSaTipovima();

            LoadAllNaocareList();

            this.view.Show();
        }
        public void PopuniComboBoxSaTipovima()
        {
            List<string> tipoviNaočara = repository.GetTipoviNaočara();
            this.view.PopuniComboBoxSaTipovima(tipoviNaočara);
        }

        private void LoadAllNaocareList()
        {
            naocareList = repository.GetAll();
            naocareBindingSource.DataSource = naocareList;
        }
        private void SearchNaocare(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                naocareList = repository.GetByValue(this.view.SearchValue);
            else naocareList = repository.GetAll();
            naocareBindingSource.DataSource = naocareList;
        }

        private void CancleAction(object sender, EventArgs e)
        {
            CleanviewFields();
        }

        private void SavehNaocare(object sender, EventArgs e)
        {
            var model = new NaocareModel();
            model.Ime = view.NaocareIme;
            model.Boja = view.NaocareBoja;
            model.Cena = Convert.ToDecimal(view.NaocareCena);
            model.Slika = view.Slika;
            model.TipNaočara = view.SelectedTip;

            try
            {
                new Common.ModelDataValidation().Validate(model);

                if (view.IsEdit)
                {
                    model.Id = Convert.ToInt32(view.NaocareId);
                    repository.Edit(model);
                    view.Message = "Naocare edited successfully";
                }
                else
                {
                    repository.Add(model);
                    view.Message = "Naocare added successfully";
                }

                view.IsSuccessful = true;
                LoadAllNaocareList();
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
            view.NaocareId = "0";
            view.NaocareIme = "";
            view.NaocareBoja = "";
            view.NaocareCena = "";
        }

        private void DeleteSelectedNaocare(object sender, EventArgs e)
        {
            try
            {
                var naocare = (NaocareModel)naocareBindingSource.Current;
                repository.Delete(naocare.Id);
                view.IsSuccessful = true;
                view.Message = "Naocare deleted successfully";
                LoadAllNaocareList();
            }
            catch
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete naocare";
            }
        }

        private void LoadSelectedNaocareToEdit(object sender, EventArgs e)
        {
            var naocare = (NaocareModel)naocareBindingSource.Current;
            view.NaocareId = naocare.Id.ToString();
            view.NaocareIme = naocare.Ime;
            view.NaocareBoja = naocare.Boja;
            view.NaocareCena = naocare.Cena.ToString();
            view.Slika = naocare.Slika;
            view.IsEdit = true;
        }


        private void AddNewNaocare(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        
    }
}
