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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            string login = Name.Text;
            string password = Password.Password;

            for (int i = login.Length; i < 50; i++)
            {
                login += " ";
            }
            for (int i = password.Length; i < 15; i++)
            {
                password += " ";
            }

            HumanResourcesDepartmentEntities db = new HumanResourcesDepartmentEntities();
            try
            {
                Worker user = db.Worker.Where((u) => u.Login == login && u.Password == password).Single();
                ControlWindow main = new ControlWindow();
                main.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Проверьте правильность логина или пароля!");
            }
        }
    }
}