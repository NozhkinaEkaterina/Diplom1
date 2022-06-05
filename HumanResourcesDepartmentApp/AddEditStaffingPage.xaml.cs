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
    /// Логика взаимодействия для AddEditStaffingPage.xaml
    /// </summary>
    public partial class AddEditStaffingPage : Page
    {
        private Staffing _currentStaffing = new Staffing();
        public AddEditStaffingPage(Staffing selectedStaffing)
        {
            InitializeComponent();

            if (selectedStaffing != null)
                _currentStaffing = selectedStaffing;

            DataContext = _currentStaffing;
            CBPosition.ItemsSource = HumanResourcesDepartmentEntities.GetContext().Position.ToList();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            int right = 0;
            if (TBNumber_Of_Staff_Units.Text == "") MessageBox.Show("Введите количество специалистов!");
            else
            {
                int error = 0;
                for (int i = 0; i < TBNumber_Of_Staff_Units.Text.Length; i++)
                {
                    if (Convert.ToChar(TBNumber_Of_Staff_Units.Text[i]) < 48 || 57 < Convert.ToChar(TBNumber_Of_Staff_Units.Text[i]))
                    {
                        error++;
                    }
                }
                if (error == 0)
                {
                    right++;
                }
                else
                {
                    MessageBox.Show("Некорректно введенo количество специалистов!");
                }
            }
           
            if (TBSalary.Text == "") MessageBox.Show("Введите сумму окладa!");
            else
            {
                int error = 0;
                string[] price = TBSalary.Text.Split('.');

                for (int g = 0; g < price.Length; g++)
                {
                    for (int i = 0; i < price[g].Length; i++)
                    {
                        if (Convert.ToChar(price[g][i]) < 48 || 57 < Convert.ToChar(price[g][i]))
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
                    MessageBox.Show("Некорректно введена суммa окладa!");
                }
            }

            if (TBNight_Shift_Allowance.Text == "") MessageBox.Show("Введите сумму надбавки за ночные смены!");
            else
            {
                int error = 0;
                string[] price = TBNight_Shift_Allowance.Text.Split('.');

                for (int g = 0; g < price.Length; g++)
                {
                    for (int i = 0; i < price[g].Length; i++)
                    {
                        if (Convert.ToChar(price[g][i]) < 48 || 57 < Convert.ToChar(price[g][i]))
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
                    MessageBox.Show("Некорректно введена суммa надбавки за ночные смены!");
                }
            }

            if (TBPremium.Text == "") MessageBox.Show("Введите сумму премиальной надбавки!");
            else
            {
                int error = 0;
                string[] price = TBPremium.Text.Split('.');

                for (int g = 0; g < price.Length; g++)
                {
                    for (int i = 0; i < price[g].Length; i++)
                    {
                        if (Convert.ToChar(price[g][i]) < 48 || 57 < Convert.ToChar(price[g][i]))
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
                    MessageBox.Show("Некорректно введена суммa премиальной надбавки!");
                }
            }

            if (TBDistrict_Coefficient.Text == "") MessageBox.Show("Введите районный коэффициент!");
            else
            {
                int error = 0;
                string[] Coefficient = TBDistrict_Coefficient.Text.Split('.');

                for (int g = 0; g < Coefficient.Length; g++)
                {
                    for (int i = 0; i < Coefficient[g].Length; i++)
                    {
                        if (Convert.ToChar(Coefficient[g][i]) < 48 || 57 < Convert.ToChar(Coefficient[g][i]))
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
                    MessageBox.Show("Некорректно введен районный коэффициент");
                }
            }

            if (CBPosition.SelectedItem == null) MessageBox.Show("Выберите должность!");
            else right++;

            if(right==6)
            {
                string text = (Convert.ToDouble(TBSalary.Text.Replace('.', ',')) + Convert.ToDouble(TBNight_Shift_Allowance.Text.Replace('.', ',')) + Convert.ToDouble(TBPremium.Text.Replace('.', ',')))
                   * Convert.ToInt32(TBNumber_Of_Staff_Units.Text.Replace('.', ',')) * Convert.ToDouble(TBDistrict_Coefficient.Text.Replace('.', ',')) + "";
                TBIn_All.Text = text.Replace(',', '.');
                _currentStaffing.In_All = Convert.ToDecimal(text);

                if (_currentStaffing.Id_Staffing == 0)
                {
                    HumanResourcesDepartmentEntities.GetContext().Staffing.Add(_currentStaffing);
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