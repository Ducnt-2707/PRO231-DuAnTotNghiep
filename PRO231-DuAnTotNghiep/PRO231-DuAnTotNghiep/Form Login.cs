using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRO231_DuAnTotNghiep
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

        }

      
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập Mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string Username = (txtUsername.Text).Trim(); // tên đăng nhập do người dùng nhập vào
                string Password = (txtPassword.Text).Trim();// mật khẩu do người dùng nhập vào
                using (SqlConnection con = new SqlConnection(dungchung.chuoiKetNoi)) // Khai báo kết nối đến CSDL
                {
                    try
                    {
                        con.Open(); // mở kết nối
                        string strSQL = "Select * from NGUOIDUNG " +
                                         " where tenDangNhap = @tenDangNhap AND matKhau = @matKhau";
                        SqlCommand cmd = new SqlCommand(strSQL, con);
                        cmd.Parameters.AddWithValue("@tenDangNhap", Username);
                        cmd.Parameters.AddWithValue("@matKhau", Password);
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            rd.Read();
                            int idNSD = (int)rd["id"];
                            string hoTenNSD = rd["Hoten"].ToString();
                            string vaitroNSD = rd["vaitro"].ToString();
                            // nếu đã đăng nhập ok, kiểm tra quyền tương ứng
                            dungchung.TenDangNhap = Username; // lưu lại để hổ trợ đổi mật khẩu  FrmDoiMatKhau
                            if (vaitroNSD == "admin")
                            {
                                formmain mainForm = new formmain("Admin");
                                mainForm.Show();
                                this.Hide(); // Ẩn form đăng nhập
                            }
                            else if (vaitroNSD == "user")
                            {
                                formmain mainForm = new formmain("User");
                                mainForm.Show();
                                this.Hide(); // Ẩn form đăng nhập
                            }
                            else
                            {
                                MessageBox.Show("Đăng nhập thất bại!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Kiểm tra lại thông tin người dùng");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnThoat1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
