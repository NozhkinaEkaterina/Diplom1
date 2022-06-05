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
    /// Логика взаимодействия для AddEditVacationSchedulePage.xaml
    /// </summary>
    public partial class AddEditVacationSchedulePage : Page
    {
        private Vacation_Schedule _currentVacation_Schedule = new Vacation_Schedule();
        public AddEditVacationSchedulePage(Vacation_Schedule selectedVacation_Schedule)
        {
            InitializeComponent();

            if (selectedVacation_Schedule != null)
                _currentVacation_Schedule = selectedVacation_Schedule;

            DataContext = _currentVacation_Schedule;
            CBProfile.ItemsSource = HumanResourcesDepartmentEntities.GetContext().Profile.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            int right = 0;
            if (DPStart_Date.Text == "") MessageBox.Show("Введите дату начала отпуска!");
            else
            {
                int error = 0;
                string[] date = DPStart_Date.Text.Split('.');

                for (int g = 0; g < date.Length; g++)
                {
                    for (int i = 0; i < date[g].Length; i++)
                    {
                        if (Convert.ToChar(date[g][i]) < 48 || 57 < Convert.ToChar(date[g][i]))
                        {
                            error++;
                        }
                    }
                }
                if (error == 0)
                {
                    right++;
                }
                else
                {
                    MessageBox.Show("Некорректно введена дата начала отпуска!");
                }
            }
            //if (TBDuration.Text == "") MessageBox.Show("Введите продолжительность отпуска!");
            //else
            //{
            //    int error = 0;
            //    for (int i = 0; i < TBDuration.Text.Length; i++)
            //    {
            //        if (Convert.ToChar(TBDuration.Text[i]) < 48 || 57 < Convert.ToChar(TBDuration.Text[i]))
            //        {
            //            error++;
            //        }
            //    }
            //    if (error == 0)
            //    {
            //        right++;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Некорректно введенa продолжительность отпуска!");
            //    }
            //}
            if (DPEnd_Date.Text == "") MessageBox.Show("Введите дату конца отпуска!");
            else
            {
                int error = 0;
                string[] date = DPEnd_Date.Text.Split('.');

                for (int g = 0; g < date.Length; g++)
                {
                    for (int i = 0; i < date[g].Length; i++)
                    {
                        if (Convert.ToChar(date[g][i]) < 48 || 57 < Convert.ToChar(date[g][i]))
                        {
                            error++;
                        }
                    }
                }
                if (error == 0)
                {
                    right++;
                }
                else
                {
                    MessageBox.Show("Некорректно введена дата конца отпуска!");
                }
            }
            if (CBProfile.SelectedItem == null) MessageBox.Show("Выберите специалиста!");
            else right++;
            if (right==3)
            {
                string text = Convert.ToString(DPStart_Date.SelectedDate - DPEnd_Date.SelectedDate);
                text = text.Trim('-');
                string[] Duration = text.Split('.');
                TBDuration.Text = Convert.ToInt32(Duration[0])+1+"";

                _currentVacation_Schedule.Duration = Convert.ToInt32(Duration[0]) + 1;
                if (_currentVacation_Schedule.Id_Vacation_Schedule == 0)
                {
                    HumanResourcesDepartmentEntities.GetContext().Vacation_Schedule.Add(_currentVacation_Schedule);
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
