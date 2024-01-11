using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplomski_rad___s21_20.Views
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            btnLogout.Click += btnLogout_Click;

            btnNaocare.Click += delegate { ShowNaocareView?.Invoke(this, EventArgs.Empty); };
            btnBold.Click += delegate { ShowBoldView?.Invoke(this, EventArgs.Empty); };
            btnDodaci.Click += delegate { ShowDodaciView?.Invoke(this, EventArgs.Empty); };
            btnRimless.Click += delegate { ShowRimlessView?.Invoke(this, EventArgs.Empty); };
            btnDecije.Click += delegate { ShowDecijeView?.Invoke(this, EventArgs.Empty); };
            btnDodaci2.Click += delegate { ShowDodaci2View?.Invoke(this, EventArgs.Empty); };
            btnKorpa.Click += delegate { ShowKorpaView?.Invoke(this, EventArgs.Empty); };

        }
        public event EventHandler ShowNaocareView;
        public event EventHandler ShowBoldView;
        public event EventHandler ShowDodaciView;
        public event EventHandler ShowDecijeView;
        public event EventHandler ShowRimlessView;
        public event EventHandler ShowDodaci2View;
        public event EventHandler ShowKorpaView;
        private void btnKorpa_Click(object sender, EventArgs e)
        {
            Korpa KorpaForm = new Korpa();
            KorpaForm.ShowDialog();
        }
        private void Login_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.ShowDialog();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (Program.CurrentUser != null)
            {
                Program.CurrentUser = null;
                MessageBox.Show("Uspešno ste se odjavili.");
            }
        }

    }
}
