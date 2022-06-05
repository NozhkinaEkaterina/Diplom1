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
    /// Логика взаимодействия для StaffingPage.xaml
    /// </summary>
    public partial class StaffingPage : Page
    {
        public StaffingPage()
        {
            InitializeComponent();
        }
        private void BtnStaffing_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditStaffingPage((Staffing)DGStaffing.Items[DGStaffing.SelectedIndex]));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditStaffingPage(null));
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = TBSearch.Text;

            var staffing = HumanResourcesDepartmentEntities.GetContext().Staffing.ToList();
            DGStaffing.ItemsSource = staffing.Where(c => c.Position.Name.Contains(search));
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HumanResourcesDepartmentEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGStaffing.ItemsSource = HumanResourcesDepartmentEntities.GetContext().Staffing.ToList();
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            var StaffingForRemoving = DGStaffing.SelectedItems.Cast<Staffing>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить слудующие {StaffingForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HumanResourcesDepartmentEntities.GetContext().Staffing.RemoveRange(StaffingForRemoving);
                    HumanResourcesDepartmentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGStaffing.ItemsSource = HumanResourcesDepartmentEntities.GetContext().Staffing.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

    }
}