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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HumanResourcesDepartmentApp
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditProfilePage((Profile)DGProfile.Items[DGProfile.SelectedIndex]));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditProfilePage(null));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string search = TBSearch.Text;

            var profile = HumanResourcesDepartmentEntities.GetContext().Profile.ToList();
            DGProfile.ItemsSource = profile.Where(c => c.Full_Name.Contains(search));
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HumanResourcesDepartmentEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGProfile.ItemsSource = HumanResourcesDepartmentEntities.GetContext().Profile.ToList();
            }
        }
    }
}
