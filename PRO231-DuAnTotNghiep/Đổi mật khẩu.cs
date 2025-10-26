using PRO231_DuAnTotNghiep;
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

namespace PRO231
{
    public partial class FrmDoiMatKhau : Form
    {
        public string TenDangNhap { get; set; }

        public FrmDoiMatKhau(string tenDangNhap)
        {
            InitializeComponent();
            TenDangNhap = tenDangNhap;
        }
           
        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Đóng form khi người dùng nhấn Hủy.
            this.Close();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matKhauCu = txtMatKhauCu.Text;
            string matKhauMoi = txtMatKhauMoi.Text;
            string xacNhanMK = txtXacNhan.Text;

            if (matKhauMoi != xacNhanMK)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                conn.Open();

                // Kiểm tra mật khẩu cũ
                string queryCheck = "SELECT COUNT(*) FROM NGUOIDUNG WHERE tenDangNhap = @tenDangNhap AND matKhau = @matKhauCu";
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@tenDangNhap", TenDangNhap);
                    cmdCheck.Parameters.AddWithValue("@matKhauCu", matKhauCu);

                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Cập nhật mật khẩu mới
                string queryUpdate = "UPDATE NGUOIDUNG SET matKhau = @matKhauMoi WHERE tenDangNhap = @tenDangNhap";
                using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@tenDangNhap", TenDangNhap);
                    cmdUpdate.Parameters.AddWithValue("@matKhauMoi", matKhauMoi);
                    cmdUpdate.ExecuteNonQuery();
                }

                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

       
        private void FrmDoiMatKhau_Load_1(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = TenDangNhap;
        }
    }
}
