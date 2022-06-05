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
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        public SchedulePage()
        {
            InitializeComponent();
            CBSearch.ItemsSource = HumanResourcesDepartmentEntities.GetContext().Profile.ToList();
        }

        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditSchedulePage((Schedule)DGSchedule.Items[DGSchedule.SelectedIndex]));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditSchedulePage(null));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (CBSearch.Text != "")
            {
                int search = Convert.ToInt32(CBSearch.SelectedValue);
                if (Visibility == Visibility.Visible)
                {
                    HumanResourcesDepartmentEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    DGSchedule.ItemsSource = HumanResourcesDepartmentEntities.GetContext().Schedule.Where(u => u.Id_Profile == search).ToList();
                }
            }
            else MessageBox.Show("Выберите специалиста!");
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            var ScheduleForRemoving = DGSchedule.SelectedItems.Cast<Schedule>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить слудующие {ScheduleForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HumanResourcesDepartmentEntities.GetContext().Schedule.RemoveRange(ScheduleForRemoving);
                    HumanResourcesDepartmentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    Search_Click(sender,e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
