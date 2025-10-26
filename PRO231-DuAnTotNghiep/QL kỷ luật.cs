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

namespace formQLkhenthuong
{
    public partial class FrmKyLuat : Form
    {
        public FrmKyLuat()
        {
            InitializeComponent();
        }
        private string userRole; // Lưu vai trò người dùng
        public FrmKyLuat(string role)
        {
            InitializeComponent();
            userRole = role;
            SetupInterface();
        }

        private void SetupInterface()
        {
            if (userRole == "User")
            {
                btnLamMoi.Enabled = false;
                btnSua.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            txtMaKL.Clear();
            txtMaNV.Clear();
            dtpNgayKL.Value = DateTime.Now;
            txtNoiDung.Clear();
            txtHinhThuc.Clear();
            MessageBox.Show("Thêm mới thành công.");
            LoadData();
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "SELECT * FROM KyLuat";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvKyLuat.DataSource = dt;
            }
        }
        private void dgvKyLuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKyLuat.Rows[e.RowIndex];
                txtMaKL.Text = row.Cells["MaKL"].Value.ToString();
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                dtpNgayKL.Value = Convert.ToDateTime(row.Cells["NgayKL"].Value);
                txtNoiDung.Text = row.Cells["NoiDung"].Value.ToString();
                txtHinhThuc.Text = row.Cells["HinhThuc"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaKL.Text == "")
            {
                MessageBox.Show("Vui lòng chọn bản ghi để sửa!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "UPDATE KyLuat SET MaNV=@MaNV, NgayKL=@NgayKL, NoiDung=@NoiDung, HinhThuc=@HinhThuc WHERE MaKL=@MaKL";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKL", txtMaKL.Text);
                cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                cmd.Parameters.AddWithValue("@NgayKL", dtpNgayKL.Value);
                cmd.Parameters.AddWithValue("@NoiDung", txtNoiDung.Text);
                cmd.Parameters.AddWithValue("@HinhThuc", txtHinhThuc.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Sửa kỷ luật thành công!");
                LoadData();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "INSERT INTO KyLuat (MaNV, NgayKL, NoiDung, HinhThuc) VALUES (@MaNV, @NgayKL, @NoiDung, @HinhThuc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                cmd.Parameters.AddWithValue("@NgayKL", dtpNgayKL.Value);
                cmd.Parameters.AddWithValue("@NoiDung", txtNoiDung.Text);
                cmd.Parameters.AddWithValue("@HinhThuc", txtHinhThuc.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Thêm kỷ luật thành công!");
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaKL.Text == "")
            {
                MessageBox.Show("Vui lòng chọn bản ghi để xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
                {
                    string query = "DELETE FROM KyLuat WHERE MaKL=@MaKL";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKL", txtMaKL.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Xóa kỷ luật thành công!");
                    LoadData();
                }
            }
        }

        private void FrmKyLuat_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
