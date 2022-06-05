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
    /// Логика взаимодействия для AddEditSchedulePage.xaml
    /// </summary>
    public partial class AddEditSchedulePage : Page
    {
        private Schedule _currentSchedule = new Schedule();
        public AddEditSchedulePage(Schedule selectedSchedule)
        {
            InitializeComponent(); 
            
            if (selectedSchedule != null)
                _currentSchedule = selectedSchedule;

            DataContext = _currentSchedule;
            CBProfile.ItemsSource = HumanResourcesDepartmentEntities.GetContext().Profile.ToList();
            CBDay.ItemsSource = HumanResourcesDepartmentEntities.GetContext().DayOfTheWeek.ToList();
            CBStatus.Items.Add("Рабочий");
            CBStatus.Items.Add("Не рабочий");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CBProfile.SelectedItem == null) MessageBox.Show("Выберите специалиста!");
            else if(CBDay.SelectedItem == null) MessageBox.Show("Выберите день недели!");
            else if(CBStatus.SelectedItem == null) MessageBox.Show("Выберите статус!"); 
            if (TBDuration.Text == "") MessageBox.Show("Введите время работы!");
            if (TBCabinet.Text == "") MessageBox.Show("Введите № кабинет!");
            else
            {

                if (_currentSchedule.Id_Schedule == 0)
                {
                    HumanResourcesDepartmentEntities.GetContext().Schedule.Add(_currentSchedule);
                }

                try
                {
                    HumanResourcesDepartmentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }

        }
    }
}
