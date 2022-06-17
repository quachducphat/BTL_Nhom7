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
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-578KINA\SQLEXPRESS;Initial Catalog=Nhom7;Integrated Security=True");
        public login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                String query = "SELECT COUNT(1) FROM login WHERE id=@id AND pass=@pass";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@id", id_txt.Text);
                sqlCmd.Parameters.AddWithValue("@pass", pass_txt.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng! Vui lòng kiểm tra lại");
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

    }
    }
