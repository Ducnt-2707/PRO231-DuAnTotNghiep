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
    public partial class FrmKhenThuong : Form
    {
        public FrmKhenThuong()
        {
            InitializeComponent();
        }

        private string userRole; // Lưu vai trò người dùng
        public FrmKhenThuong(string role)
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


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKT.Clear();
            txtMaNV.Clear();
            dtpNgayKT.Value = DateTime.Now;
            txtNoiDung.Clear();
            txtHinhThuc.Clear();
            MessageBox.Show("Thêm mới thành công.");
            LoadData();
        }

        private void dgvKhenThuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhenThuong.Rows[e.RowIndex];
                txtMaKT.Text = row.Cells["MaKT"].Value.ToString();
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                dtpNgayKT.Value = Convert.ToDateTime(row.Cells["NgayKT"].Value);
                txtNoiDung.Text = row.Cells["NoiDung"].Value.ToString();
                txtHinhThuc.Text = row.Cells["HinhThuc"].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaKT.Text == "")
            {
                MessageBox.Show("Vui lòng chọn bản ghi để xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
                {
                    string query = "DELETE FROM KhenThuong WHERE MaKT=@MaKT";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKT", txtMaKT.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Xóa khen thưởng thành công!");
                    LoadData();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaKT.Text == "")
            {
                MessageBox.Show("Vui lòng chọn bản ghi để sửa!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "UPDATE KhenThuong SET MaNV=@MaNV, NgayKT=@NgayKT, NoiDung=@NoiDung, HinhThuc=@HinhThuc WHERE MaKT=@MaKT";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKT", txtMaKT.Text);
                cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                cmd.Parameters.AddWithValue("@NgayKT", dtpNgayKT.Value);
                cmd.Parameters.AddWithValue("@NoiDung", txtNoiDung.Text);
                cmd.Parameters.AddWithValue("@HinhThuc", txtHinhThuc.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Sửa khen thưởng thành công!");
                LoadData();
            }
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "SELECT * FROM KhenThuong";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvKhenThuong.DataSource = dt;

                // Thiết lập tên cột bằng tiếng Việt
                dgvKhenThuong.Columns["MaKT"].HeaderText = "Mã Khen Thưởng";
                dgvKhenThuong.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvKhenThuong.Columns["NgayKT"].HeaderText = "Ngày Khen Thưởng";
                dgvKhenThuong.Columns["NoiDung"].HeaderText = "Nội Dung";
                dgvKhenThuong.Columns["HinhThuc"].HeaderText = "Hình Thức";
                // Tự động co giãn cột
                dgvKhenThuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "INSERT INTO KhenThuong (MaNV, NgayKT, NoiDung, HinhThuc) VALUES (@MaNV, @NgayKT, @NoiDung, @HinhThuc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                cmd.Parameters.AddWithValue("@NgayKT", dtpNgayKT.Value);
                cmd.Parameters.AddWithValue("@NoiDung", txtNoiDung.Text);
                cmd.Parameters.AddWithValue("@HinhThuc", txtHinhThuc.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Thêm khen thưởng thành công!");
                LoadData();
            }
        }

        private void FrmKhenThuong_Load(object sender, EventArgs e)
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
