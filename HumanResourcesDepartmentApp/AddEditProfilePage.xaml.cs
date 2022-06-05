using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddEditProfilePage.xaml
    /// </summary>
    public partial class AddEditProfilePage : Page
    {
        private Profile _currentProfile = new Profile();
        public AddEditProfilePage(Profile selectedProfile)
        {
            InitializeComponent();

            if (selectedProfile != null)
                _currentProfile = selectedProfile;

            DataContext = _currentProfile;
            CBPosition.ItemsSource = HumanResourcesDepartmentEntities.GetContext().Position.ToList();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            int right = 0;
            if (TBFull_Name.Text == "") MessageBox.Show("Введите ФИО специалиста!");
            else right++;


            if (TBDate_of_Birth.Text == "") MessageBox.Show("Введите дату рождения!");
            else
            {
                int error = 0;
                string[] date = TBDate_of_Birth.Text.Split('.');

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
                    if (Convert.ToDateTime(TBDate_of_Birth.Text) > DateTime.Now)
                    {
                        MessageBox.Show("Некорректно введена дата рождения!");
                    }
                    else
                    {
                        right++;
                    }
                }
                else
                {
                    MessageBox.Show("Некорректно введена дата рождения!");
                }
            }

            if (TBPlace_of_Birth.Text == "") MessageBox.Show("Введите место рождения!");
            else right++;

            if (TBPlace_of_Registration.Text == "") MessageBox.Show("Введите место временной или постоянной регистрации!");
            else right++;

            if (TBCriminal_Record_Information.Text == "") MessageBox.Show("Введите информацию о судимостях или их отсутствии!");
            else right++;

            if (TBPhone_Number.Text == "") MessageBox.Show("Введите номер телефона!");
            else if ("+".CompareTo(Convert.ToString(TBPhone_Number.Text[0])) == 0)//проверка на правильность введенного н.телефона
            {
                TBPhone_Number.Text = TBPhone_Number.Text.Trim(' ');
                int kol = 0;
                for (int i = 1; i < TBPhone_Number.Text.Length; i++)
                {
                    if (Convert.ToChar(TBPhone_Number.Text[i]) >= 48 && 57 >= Convert.ToChar(TBPhone_Number.Text[i]))
                    {
                        kol++;
                    }
                }
                if (kol != 11)
                {
                    MessageBox.Show("Некорректно введен номер телефона!");
                }
                else right++;
            }
            else
            {
                TBPhone_Number.Text = TBPhone_Number.Text.Trim(' ');
                int kol = 0;
                for (int i = 0; i < TBPhone_Number.Text.Length; i++)
                {
                    if (Convert.ToChar(TBPhone_Number.Text[i]) >= 48 && 57 >= Convert.ToChar(TBPhone_Number.Text[i]))
                    {
                        kol++;
                    }
                }
                if (kol != 11)
                {
                    MessageBox.Show("Некорректно введен номер телефона!");
                }
                else right++;
            }


            if (TBMail_Address.Text == "") MessageBox.Show("Введите электронный адрес!");
            else 
            {
                TBMail_Address.Text = TBMail_Address.Text.Trim(' ');
                int eror = 0;
                int kol = 0;
                string[] part = TBMail_Address.Text.Split('@');
                string[] dot = part[part.Length - 1].Split('.');
                if (part.Length != 2)
                {
                    MessageBox.Show("Некорректно введен e-mail!");
                }
                else if (part[0].Length < 1 || part[1].Length < 1)
                {
                    MessageBox.Show("Некорректно введен e-mail!");
                }
                else if (dot.Length != 2)
                {
                    MessageBox.Show("Некорректно введен e-mail!");
                }
                else if (dot[0].Length < 1 || dot[1].Length < 1)
                {
                    MessageBox.Show("Некорректно введен e-mail!");
                }
                else
                {
                    for (int g = 0; g < part.Length; g++)
                    {
                        for (int i = 0; i < part[g].Length; i++)
                        {
                            kol++;
                            if (((Convert.ToChar(part[g][i]) < 97 || 122 < Convert.ToChar(part[g][i])) && (Convert.ToChar(part[g][i]) < 48 || 57 < Convert.ToChar(part[g][i]))) && part[g][i] + "" != ".")
                            {
                                MessageBox.Show("Некорректно введен e-mail!");
                                break;
                            }
                            else if (i == 0 && part[g][i] + "" == ".")
                            {
                                MessageBox.Show("Некорректно введен e-mail!");
                                break;
                            }
                            else if (i == part[g].Length - 1 && part[g][i] + "" == ".")
                            {
                                MessageBox.Show("Некорректно введен e-mail!");
                                break;
                            }
                            else
                            {
                                eror++;
                            }

                        }
                    }
                    if (kol == eror) right++;
                }
            }

            if (TBPassport_Data.Text == "") MessageBox.Show("Введите паспортные данные!");
            else
            {
                TBPassport_Data.Text = TBPassport_Data.Text.Trim(' ')+"";
                int kol = 0;
                for (int i = 0; i < TBPassport_Data.Text.Length; i++)
                {
                    if (Convert.ToChar(TBPassport_Data.Text[i]) >= 48 && 57 >= Convert.ToChar(TBPassport_Data.Text[i]))
                    {
                        kol++;
                    }
                }
                if (kol != 10)
                {
                    MessageBox.Show($"Некорректно введены паспортные данные!");
                }
                else right++;
            }

            if (TBSNILS.Text == "") MessageBox.Show("Введите номер СНИЛС!");
            else
            {
                TBSNILS.Text = TBSNILS.Text.Trim(' ') + "";
                int kol = 0;
                for (int i = 0; i < TBSNILS.Text.Length; i++)
                {
                    if (Convert.ToChar(TBSNILS.Text[i]) >= 48 && 57 >= Convert.ToChar(TBSNILS.Text[i]))
                    {
                        kol++;
                    }
                }
                if (kol != 11)
                {
                    MessageBox.Show("Некорректно введен номер СНИЛС!");
                }
                else right++;
            }
            
            if (CBPosition.SelectedItem == null) MessageBox.Show("Выберите должность!");
            else right++;

            if (right==10)
            {

                if (_currentProfile.Id_Profile == 0)
                {
                    HumanResourcesDepartmentEntities.GetContext().Profile.Add(_currentProfile);
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