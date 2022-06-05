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
    /// Логика взаимодействия для VacationSchedulePage.xaml
    /// </summary>
    public partial class VacationSchedulePage : Page
    {
        public VacationSchedulePage()
        {
            InitializeComponent();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HumanResourcesDepartmentEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGVacationSchedule.ItemsSource = HumanResourcesDepartmentEntities.GetContext().Vacation_Schedule.ToList();
            }
        }

        private void BtnVacationSchedule_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditVacationSchedulePage((Vacation_Schedule)DGVacationSchedule.Items[DGVacationSchedule.SelectedIndex]));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditVacationSchedulePage(null));
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = TxtSearch.Text;

            var VacationSchedule = HumanResourcesDepartmentEntities.GetContext().Vacation_Schedule.ToList();
            DGVacationSchedule.ItemsSource = VacationSchedule.Where(c => c.Profile.Full_Name.Contains(search));
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            var ScheduleForRemoving = DGVacationSchedule.SelectedItems.Cast<Vacation_Schedule>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить слудующие {ScheduleForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HumanResourcesDepartmentEntities.GetContext().Vacation_Schedule.RemoveRange(ScheduleForRemoving);
                    HumanResourcesDepartmentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGVacationSchedule.ItemsSource = HumanResourcesDepartmentEntities.GetContext().Vacation_Schedule.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
