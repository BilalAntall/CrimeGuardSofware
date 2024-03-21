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
using System.Windows.Shapes;

namespace Crime_App
{
    /// <summary>
    /// Interaction logic for Login_Page.xaml
    /// </summary>
    public partial class Login_Page : Window
    {
        public Login_Page()
        {
            InitializeComponent();
        }

        public void Signup_Process(object sender,MouseButtonEventArgs e)
        {
            MainWindow newobj = new MainWindow();
            newobj.Show();
            this.Close();
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize_button(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Login_Logic(object sender,RoutedEventArgs e)
        {
            string username = lg_username.Text;
            string password = lgpassword.Password;
            SQLDB newobj = new SQLDB();
            if (!newobj.Username_Check(username))
            {
                if (newobj.Password_check(username, password))
                {
                    Dashboard newobj1 = new Dashboard();
                    newobj1.ShowDialog();
                    this.Close();
                }
                else
                    MessageBox.Show("Incorrect Password!");
            }
            else
            {
                MessageBox.Show("Username does not exist!");
            }
        }
    }
}
