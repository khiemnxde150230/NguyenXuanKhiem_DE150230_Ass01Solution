using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessObject;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;


namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private IConfiguration config;
        IMemberRepository memberRepository = new MemberRepository();
        public WindowLogin()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            config = builder.Build();
        }

        bool checkLoginAdmin(string email, string password)
        {
            string configEmail = config["account:adminAccount:email"];
            string configPassword = config["account:adminAccount:password"];

            if (email == configEmail && password == configPassword)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Invalid Login", "Admin Login");
            }
            return false;
        }

        bool checkLogin(string email, string password, out int memberId)
        {
            Member member = memberRepository.CheckLogin(email, password);

            if (member != null)
            {
                memberId = member.MemberId;
                return true;
            }
            else
            {
                MessageBox.Show("Invalid Login", "User Login");
                memberId = -1;
            }
            return false;
        }

        private void btnAdminLogin_Click(object sender, RoutedEventArgs e)
        {
            if (checkLoginAdmin(textBoxEmail.Text, passBoxPassword.Password))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Hide();
            }
        }

        private void btnUserLogin_Click(object sender, RoutedEventArgs e)
        {
            int memberId;
            if (checkLogin(textBoxEmail.Text, passBoxPassword.Password, out memberId))
            {
                MainUserWindow mainUserWindow = new MainUserWindow(memberId);
                mainUserWindow.Show();
                this.Hide();
            }
        }
    }
}
