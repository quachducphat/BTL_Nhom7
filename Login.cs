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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CRUD
{

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=User;Integrated Security=True");
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM UserLogin WHERE Username=@Username AND Password=@Password", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Username", user_txt.Text);
                cmd.Parameters.AddWithValue("@Password", password_txt.Password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow mwd = new MainWindow();
                    mwd.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("KIỂM TRA LẠI TÀI KHOẢN VÀ MẬT KHẨU", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void register_btn_Click(object sender, RoutedEventArgs e)
        {
            Register rgt = new Register();
            rgt.Show();
            this.Close();
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
