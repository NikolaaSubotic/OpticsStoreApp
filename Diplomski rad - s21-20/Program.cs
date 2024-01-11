using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diplomski_rad___s21_20.Models;
using Diplomski_rad___s21_20.Presenters;
using Diplomski_rad___s21_20._Repositories;
using Diplomski_rad___s21_20.Views;
using System.Configuration;

namespace Diplomski_rad___s21_20
{
    internal static class Program
    {
        public static string CurrentUser { get; set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            IMainView view = new MainView();
            new MainPresenter(view, sqlConnectionString);

            Application.Run((Form)view);
        }
    }
}
