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
            ApplyModernTheme();
            UpdateAuthenticationUi();

            btnLogout.Click += btnLogout_Click;

            btnNaocare.Click += delegate { ShowNaocareView?.Invoke(this, EventArgs.Empty); };
            btnBold.Click += delegate { ShowBoldView?.Invoke(this, EventArgs.Empty); };
            btnDodaci.Click += delegate { ShowDodaciView?.Invoke(this, EventArgs.Empty); };
            btnRimless.Click += delegate { ShowRimlessView?.Invoke(this, EventArgs.Empty); };
            btnDecije.Click += delegate { ShowDecijeView?.Invoke(this, EventArgs.Empty); };
            btnDodaci2.Click += delegate { ShowDodaci2View?.Invoke(this, EventArgs.Empty); };
            btnKorpa.Click += delegate { ShowKorpaView?.Invoke(this, EventArgs.Empty); };

        }

        private void ApplyModernTheme()
        {
            BackColor = Color.FromArgb(245, 247, 250);
            Text = "Optics Store | POS Dashboard";

            panel1.BackColor = Color.FromArgb(27, 38, 59);
            panel1.Padding = new Padding(12);

            var menuButtons = new[] { btnNaocare, btnBold, btnDodaci, btnRimless, btnDecije, btnDodaci2, btnKorpa };
            foreach (var button in menuButtons)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.BackColor = Color.FromArgb(65, 90, 119);
                button.ForeColor = Color.White;
                button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                button.Cursor = Cursors.Hand;
            }

            var authButtons = new[] { Login, Register, btnLogout };
            foreach (var button in authButtons)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.BackColor = Color.FromArgb(224, 225, 221);
                button.ForeColor = Color.FromArgb(27, 38, 59);
                button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                button.Cursor = Cursors.Hand;
            }
        }

        private void UpdateAuthenticationUi()
        {
            bool isLoggedIn = !string.IsNullOrWhiteSpace(Program.CurrentUser);
            btnLogout.Enabled = isLoggedIn;
            btnKorpa.Enabled = isLoggedIn;

            Login.Visible = !isLoggedIn;
            Register.Visible = !isLoggedIn;
            btnLogout.Visible = isLoggedIn;
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
            UpdateAuthenticationUi();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.ShowDialog();
            UpdateAuthenticationUi();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (Program.CurrentUser != null)
            {
                Program.CurrentUser = null;
                MessageBox.Show("Uspešno ste se odjavili.");
                UpdateAuthenticationUi();
            }
        }

    }
}
