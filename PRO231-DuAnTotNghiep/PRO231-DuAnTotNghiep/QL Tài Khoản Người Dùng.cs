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

namespace QLTaiKhoanNguoiDung
{
    public partial class FrmTaiKhoanNguoiDung : Form
    {
        public FrmTaiKhoanNguoiDung()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {
        }
        private void label4_Click(object sender, EventArgs e)
        {
        }
        private void label3_Click(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoten.Text) ||
               string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
               string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
               string.IsNullOrWhiteSpace(txtVaiTro.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            string query = "INSERT INTO NGUOIDUNG (Hoten, tenDangNhap, matKhau, vaitro) VALUES (@Hoten, @tenDangNhap, @matKhau, @vaitro)";

            try
            {
                using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Hoten", txtHoten.Text);
                        command.Parameters.AddWithValue("@tenDangNhap", txtTenDangNhap.Text);
                        command.Parameters.AddWithValue("@matKhau", txtMatKhau.Text);
                        command.Parameters.AddWithValue("@vaitro", txtVaiTro.Text);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Người dùng đã được tạo thành công.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm người dùng: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;

            string query = "DELETE FROM NGUOIDUNG WHERE id = @id";

            try
            {
                using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Người dùng đã được xóa thành công.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa người dùng: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật.");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);

            string query = "UPDATE NGUOIDUNG SET Hoten=@Hoten, tenDangNhap=@tenDangNhap, matKhau=@matKhau, vaitro=@vaitro WHERE id=@id";

            try
            {
                using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@Hoten", txtHoten.Text);
                        command.Parameters.AddWithValue("@tenDangNhap", txtTenDangNhap.Text);
                        command.Parameters.AddWithValue("@matKhau", txtMatKhau.Text);
                        command.Parameters.AddWithValue("@vaitro", txtVaiTro.Text);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cập nhật thông tin người dùng thành công.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
                {
                    conn.Open();
                    string sql = "SELECT * FROM NGUOIDUNG";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
            }
        }

        private void FrmTaiKhoanNguoiDung_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaTaiKhoan.Text = row.Cells["id"].Value.ToString();
                txtHoten.Text = row.Cells["Hoten"].Value.ToString();
                txtTenDangNhap.Text = row.Cells["tenDangNhap"].Value.ToString();
                txtMatKhau.Text = row.Cells["matKhau"].Value.ToString();
                txtVaiTro.Text = row.Cells["vaitro"].Value.ToString();
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtHoten.Clear();
            txtMaTaiKhoan.Clear();
            txtMatKhau.Clear();
            txtVaiTro.Clear();            
        }
    }
}
