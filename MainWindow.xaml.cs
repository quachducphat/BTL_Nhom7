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


namespace CRUD
{
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-578KINA\SQLEXPRESS;Initial Catalog=Nhom7;Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
            loadGird();
        }
        public void loadGird()
        {
            SqlCommand cmd = new SqlCommand("select * from dieukhoan", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            drv.ItemsSource = dt.DefaultView;
 
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
            if (drv.SelectedIndex.ToString() != null)
{
                DataRowView datarv = (DataRowView)drv.SelectedItem;
                if (drv != null)
                {
                    muc_txt.Text = datarv[0].ToString();
                    dieu_txt.Text = datarv[1].ToString();
                    nddieu_txt.Text = datarv[2].ToString();
                    khoan_txt.Text = datarv[3].ToString();
                    ndkhoan_txt.Text = datarv[4].ToString();

                }
            }
        }
        public bool isValid()
        {
            if (muc_txt.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN MỤC", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (dieu_txt.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN ĐIỀU", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (nddieu_txt.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN NỘI DUNG ĐIỀU", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (khoan_txt.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN KHOẢN", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (ndkhoan_txt.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN NỘI DUNG KHOẢN", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("CHẮC CHẮN THÊM? ", "ĐÃ LƯU", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (isValid())
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO dieukhoan VALUES (@muc, @ndMuc, @dieu, @ndDieuBoSung, @khoan, @ndBoSung, @mucPhatDuoi, @mucPhatTren)", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("muc", muc_txt.Text);
                        cmd.Parameters.AddWithValue("ndMuc", ndmuc_txt.Text);
                        cmd.Parameters.AddWithValue("dieu", dieu_txt.Text);
                        cmd.Parameters.AddWithValue("ndDieuBoSung", nddieu_txt.Text);
                        cmd.Parameters.AddWithValue("noiDungDieu", khoan_txt.Text);
                        cmd.Parameters.AddWithValue("khoan", khoan_txt.Text);
                        cmd.Parameters.AddWithValue("ndBoSung", ndkhoan_txt.Text);
                        cmd.Parameters.AddWithValue("mucPhatTren", phat_tren_txt.Text);
                        cmd.Parameters.AddWithValue("mucPhatDuoi", phat_duoi_txt.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        loadGird();
                        MessageBox.Show("LƯU THÀNH CÔNG", "ĐÃ LƯU", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                con.Close();

            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("CHẮC CHẮN XOÁ? ", "ĐÃ XÓA DỮ LIỆU", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM dieukhoan WHERE muc = " + muc_txt.Text + "AND dieu = " + dieu_txt.Text + "AND khoan = " + khoan_txt.Text, con);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("ĐÃ XOÁ", "ĐÃ LƯU", MessageBoxButton.OK, MessageBoxImage.Information);
                    con.Close();
                    loadGird();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                con.Close();
            }
        }

        public void clearData()
        {
            muc_txt.Clear();
            ndmuc_txt.Clear();
            khoan_txt.Clear();
            ndkhoan_txt.Clear();
            dieu_txt.Clear();
            nddieu_txt.Clear();
        }
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("CHẮC CHẮN SỬA? ", "ĐÃ SỬA", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (isValid())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE dieukhoan set ndMuc = @ndMuc, dieu = @dieu, ndDieuBoSung = @ndDieuBoSung, khoan = @khoan, ndBoSung = @ndBoSung, mucPhatDuoi=@mucPhatDuoi, mucPhatTren=@mucPhatTren WHERE muc = @muc", con);
                        cmd.Parameters.AddWithValue("muc", muc_txt.Text);
                        cmd.Parameters.AddWithValue("noiDungMuc", ndmuc_txt.Text);
                        cmd.Parameters.AddWithValue("dieu", dieu_txt.Text);
                        cmd.Parameters.AddWithValue("ndDieuBoSung", nddieu_txt.Text);
                        cmd.Parameters.AddWithValue("khoan", khoan_txt.Text);
                        cmd.Parameters.AddWithValue("ndBoSung", ndkhoan_txt.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        loadGird();
                        MessageBox.Show("FIX SUCCESS", "ĐÃ LƯU", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message); 
                }
            }
            else
            {
                con.Close();

            }
        }
    }
}
