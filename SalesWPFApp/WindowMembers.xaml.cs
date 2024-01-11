using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for WindowMembers.xaml
    /// </summary>
    public partial class WindowMembers : Window
    {
        IMemberRepository memberRepository = new MemberRepository();
        public ObservableCollection<Member> Members { get; set; }

        //private int memberId;
        public WindowMembers()
        {
            InitializeComponent();
            Members = new ObservableCollection<Member>();
            dgvMemberList.ItemsSource = Members;
        }

        private void dgvMemberList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a member");
                return;
            }

            Member selectedMember = (Member)dataGrid.SelectedItem;
            int memberId = selectedMember.MemberId;

            Member memberInfo = memberRepository.GetMemberById(memberId);

            if (memberInfo != null)
            {
                WindowMemberDetail windowMemberDetail = new WindowMemberDetail();

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



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnDelete.IsEnabled = false;
            //dgvMemberList.MouseDoubleClick += dgvMemberList_CellDoubleClick;
        }


        public void LoadMemberList()
        {
            try
            {
                var members = memberRepository.GetMembers();

                Members.Clear();

                foreach (var member in members)
                {
                    Members.Add(member);
                }

                if (members.Count() == 0)
                {
                    btnDelete.IsEnabled = false;
                }
                else
                {
                    btnDelete.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Member list");
            }
        }


        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadMemberList();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            WindowMemberDetail windowMemberDetail = new WindowMemberDetail();

            windowMemberDetail.MemberRepository = memberRepository;
            windowMemberDetail.InsertOrUpdate = false;
            windowMemberDetail.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = (Member)dgvMemberList.SelectedItem;
                memberRepository.DeleteMember(member.MemberId);
                LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete member");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
