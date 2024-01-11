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
    /// Interaction logic for MainUserWindow.xaml
    /// </summary>
    public partial class MainUserWindow : Window
    {
        private int memberId;
        IMemberRepository memberRepository = new MemberRepository();
        public MainUserWindow(int memberId)
        {
            InitializeComponent();
            this.memberId = memberId;
        }

        private void btnMemberDetail_Click(object sender, RoutedEventArgs e)
        {
            Member memberInfo = memberRepository.GetMemberById(memberId);

            if (memberInfo != null)
            {
                WindowMemberDetail windowMemberDetail = new WindowMemberDetail();

                //
                windowMemberDetail.MemberRepository = memberRepository;
                windowMemberDetail.MemberInfo = memberInfo;
                windowMemberDetail.InsertOrUpdate = true;
                windowMemberDetail.Show();
            }
            else
            {
                MessageBox.Show("Invalid user");
            }
        }

        private void btnOrderHistory_Click(object sender, RoutedEventArgs e)
        {
            WindowOrderHistory windowOrderHistory = new WindowOrderHistory(memberId);
            windowOrderHistory.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
