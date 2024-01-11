using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diplomski_rad___s21_20.Views;

namespace Diplomski_rad___s21_20.Views
{
    public partial class NaocareView : Form, INaocareView
    {
        private readonly string connectionString = "Data Source=localhost;Initial Catalog=OpticarskaRadnjaDb;Integrated Security=True;";
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        public NaocareView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPage2);
            btnClose.Click += delegate { this.Close(); };
            Delete.Visible = Program.CurrentUser == "admin";
            Add.Visible = Program.CurrentUser == "admin";
            Edit.Visible = Program.CurrentUser == "admin";
            btnDodajUKorpu.Visible = Program.CurrentUser != null;
            txtKolicina.Visible = Program.CurrentUser != null;
            label9.Visible = Program.CurrentUser != null;
        }

        private void AssociateAndRaiseViewEvents()
        {
            Search.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };
            //Add new
            Add.Click += delegate 
            { 
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabPage2.Text = "Dodaj nove naocare";
            };
            //Edit
            Edit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabPage2.Text = "Izmeni naocare";
            };

            //Save
            Save.Click += delegate 
            { 
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if(IsSuccessful)
                {
                    tabControl1.TabPages.Remove(tabPage2);
                    tabControl1.TabPages.Add(tabPage1);
                }
                MessageBox.Show(Message);

            };
            //Cancle
            Cancle.Click += delegate 
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Add(tabPage1);
            };
            //Delete
            Delete.Click += delegate 
            { 
                
                var result = MessageBox.Show("Da li ste sigurni da zelite da obrisete ovaj proizvod?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            btnChooseImage.Click += delegate 
            { 
                ChooseImageEvent?.Invoke(this, EventArgs.Empty);
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Slike|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Učitaj odabranu sliku
                        Slika = File.ReadAllBytes(openFileDialog.FileName);

                        // Prikazivanje odabrane slike na PictureBox kontroli
                        ShowImage(Slika);

                        // Pozovite događaj ChooseImageEvent da obavesti ostatak aplikacije o odabiru slike.
                        ChooseImageEvent?.Invoke(this, EventArgs.Empty);
                    }
                }

            };
            btnDodajUKorpu.Click += btnDodajUKorpu_Click;

        }

        public string NaocareId { get { return txtID.Text; } set => txtID.Text = value; }
        public string NaocareIme { get { return txtIme.Text; } set => txtIme.Text = value; }
        public string NaocareBoja { get { return txtBoja.Text; } set => txtBoja.Text = value; }
        public string NaocareCena { get { return txtCena.Text; } set => txtCena.Text = value; }
        public byte[] Slika { get; set; } // Dodali smo svojstvo za čuvanje slike kao niz bajtova
        public List<string> TipoviNaočara
        {
            set
            {
                comboBoxTip.DataSource = value;
            }
        }
        public string SelectedTip
        {
            get { return comboBoxTip.SelectedItem.ToString(); }
        }
        public string SearchValue { get { return txtSearch.Text; } set => txtSearch.Text = value; }
        public bool IsEdit { get { return isEdit; } set => isEdit = value; }
        public bool IsSuccessful { get { return isSuccessful; } set => isSuccessful = value; }
        public string Message { get { return message; } set => message = value; }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler ChooseImageEvent; // Dodali smo događaj za odabir slike

        public void SetNaocareListBindingSource(BindingSource naocareList)
        {
            dataGridView1.DataSource = naocareList;
        }

        // Implementirali smo metodu za prikaz slike
        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Slike|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtrirajte dozvoljene tipove slika
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Učitaj odabranu sliku
                    Slika = File.ReadAllBytes(openFileDialog.FileName);

                    // Prikazivanje odabrane slike na PictureBox kontroli
                    ShowImage(Slika);

                    // Pozovite događaj ChooseImageEvent da obavesti ostatak aplikacije o odabiru slike.
                    ChooseImageEvent?.Invoke(this, EventArgs.Empty);
                }
            }
        }



        // Implementirajte metodu za prikaz slike na PictureBox kontroli
        public void ShowImage(byte[] imageBytes)
        {
            if (imageBytes != null && imageBytes.Length > 0)
            {
                using (var ms = new MemoryStream(imageBytes))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Postavite način prikaza na "Zoom" kako bi slika bila u okviru PictureBox-a bez promene veličine.
                }
            }
            else
            {
                pictureBox1.Image = null; // Postavite PictureBox na null ako nema slike
            }
        }

        //Singleton pattern
        private static NaocareView instance;
        public static NaocareView GetInstance(Form parentContainer)
        {
            if(instance == null || instance.IsDisposed)
            {
                instance = new NaocareView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
                
            else
            {
                if(instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }
        public void PopuniComboBoxSaTipovima(List<string> tipoviNaočara)
        {
            // Očistite ComboBox pre nego što ga popunite
            comboBoxTip.Items.Clear();

            // Dodajte sve tipove naočara u ComboBox
            foreach (var tip in tipoviNaočara)
            {
                comboBoxTip.Items.Add(tip);
            }
        }
        private void btnDodajUKorpu_Click(object sender, EventArgs e)
        {
            // Proverite da li je odabrana barem jedna vrsta u DataGridView-u
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Dohvatite ID proizvoda iz trenutno odabrane vrste
                object naocareIdObj = dataGridView1.SelectedRows[0].Cells[0].Value;

                // Provera da li je vrednost ćelije u brojnom formatu
                if (naocareIdObj != null && int.TryParse(naocareIdObj.ToString(), out int naocareId))
                {
                    // Dohvatite količinu iz TextBox-a ili nekog drugog kontrola
                    if (int.TryParse(txtKolicina.Text, out int kolicina))
                    {
                        // Dodajte proizvod u korpu
                        DodajUKorpu(Program.CurrentUser, naocareId, kolicina);
                        MessageBox.Show("Proizvod je dodat u korpu.");
                    }
                    else
                    {
                        MessageBox.Show("Unesite ispravan broj za količinu.");
                    }
                }
                else
                {
                    MessageBox.Show("Odaberite validan red sa proizvodom.");
                }
            }
            else
            {
                MessageBox.Show("Molimo odaberite ceo red pre dodavanja u korpu.");
            }
        }


        private void DodajUKorpu(string korisnikUsername, int naocareId, int kolicina)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kreirajte SQL upit za dodavanje proizvoda u korpu
                    string query = "INSERT INTO Korpa (Korisnik_Id, Naocare_Id, Kolicina) VALUES " +
                                   "((SELECT id FROM Korisnici WHERE username = @korisnikUsername), @naocareId, @kolicina)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@korisnikUsername", korisnikUsername);
                        command.Parameters.AddWithValue("@naocareId", naocareId);
                        command.Parameters.AddWithValue("@kolicina", kolicina);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri dodavanju proizvoda u korpu: {ex.Message}");
            }
        }

    }
}
 