using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace Crime_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SQLDB dbobj = new SQLDB();
           // dbobj.Initialize_Database();
            //dbobj.Create_Tables();
        }
        private void LoginProcess_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login_Page lgpgobj = new Login_Page();
            lgpgobj.Show();
            this.Close();
        }
        private void SignUpInfo(object sender,RoutedEventArgs e)
        {
            string username=Username_Textbox.Text;
            string email = Email_Textbox.Text;
            string password = PasswordTextbox.Password;
            SQLDB newobj = new SQLDB();
            if(newobj.Username_Check(username))
            {
                newobj.InsertValues(username,email, password);
                //Crime_Dashboard obj = new Crime_Dashboard();
                //obj.Show();
                //this.Close();
            }
            else
            {
                MessageBox.Show("Username already taken!");
            }
      
        }

        private void Exit_Button(object sender,RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize_button(object sender, RoutedEventArgs e)
        {
            WindowState=WindowState.Minimized;  
        }
    }
}
