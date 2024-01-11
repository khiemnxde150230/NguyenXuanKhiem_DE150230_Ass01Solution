using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
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

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for WindowMemberDetail.xaml
    /// </summary>
    public partial class WindowMemberDetail : Window
    {
        public IMemberRepository MemberRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public Member MemberInfo { get; set; }

        public WindowMemberDetail()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (InsertOrUpdate == true)
            {
                txtMemberID.Text = MemberInfo.MemberId.ToString();
                txtEmail.Text = MemberInfo.Email;
                txtCompanyName.Text = MemberInfo.CompanyName;
                txtCity.Text = MemberInfo.City;
                txtCountry.Text = MemberInfo.Country;
                txtPassword.Password = MemberInfo.Password;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var memberu = new Member
                //{
                //    MemberId = int.Parse(txtMemberID.Text),
                //    Email = txtEmail.Text,
                //    CompanyName = txtCompanyName.Text,
                //    City = txtCity.Text,
                //    Country = txtCountry.Text,
                //    Password = txtPassword.Password

                //};

                
                if (InsertOrUpdate == false)
                {
                    var member = new Member
                    {
                        //MemberId = int.Parse(txtMemberID.Text),
                        Email = txtEmail.Text,
                        CompanyName = txtCompanyName.Text,
                        City = txtCity.Text,
                        Country = txtCountry.Text,
                        Password = txtPassword.Password

                    };

                    MemberRepository.InsertMember(member);
                    MessageBox.Show("Member Added");
                }
                else
                {
                    var member = new Member
                    {
                        MemberId = int.Parse(txtMemberID.Text),
                        Email = txtEmail.Text,
                        CompanyName = txtCompanyName.Text,
                        City = txtCity.Text,
                        Country = txtCountry.Text,
                        Password = txtPassword.Password

                    };
                    MemberRepository.UpdateMember(member);
                    MessageBox.Show("Member Updated");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new member" : "Update a member");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
