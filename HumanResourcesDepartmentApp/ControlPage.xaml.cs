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
    /// Логика взаимодействия для ControlPage.xaml
    /// </summary>
    public partial class ControlPage : Page
    {
        public ControlPage()
        {
            InitializeComponent();
        }


        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProfilePage());
        }

        private void BtnStaffing_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new StaffingPage());
        }

        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new SchedulePage());
        }
        private void BtnVacation_Schedule_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new VacationSchedulePage());
        }
    }
}
