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
    public partial class FrmHopDongLaoDong : Form
    {
        public FrmHopDongLaoDong()
        {
            InitializeComponent();
        }
        private string userRole; // Lưu vai trò người dùng
        public FrmHopDongLaoDong(string role)
        {
            InitializeComponent();
            userRole =  role;
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
     
        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "SELECT * FROM HopDongLaoDong";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvHopDong.DataSource = dt;

                // Thiết lập tên cột bằng tiếng Việt
                dgvHopDong.Columns["MaHD"].HeaderText = "Mã Hợp Đồng";
                dgvHopDong.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvHopDong.Columns["LoaiHD"].HeaderText = "Loại Hợp Đồng";
                dgvHopDong.Columns["NgayKy"].HeaderText = "Ngày Ký";
                dgvHopDong.Columns["NgayHetHan"].HeaderText = "Ngày Hết Hạn";
                dgvHopDong.Columns["NoiDung"].HeaderText = "Nội Dung";


                // Tự động co giãn cột
                dgvHopDong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
      


        private void btnTimKiem(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                List<string> conditions = new List<string>(); // Danh sách điều kiện tìm kiếm
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                string query = "SELECT MaNV, HoTen, NgaySinh, GioiTinh, DiaChi, DienThoai FROM NhanVien";

                if (!string.IsNullOrEmpty(txtTimKiemTen.Text))
                {
                    conditions.Add("HoTen LIKE @HoTen");
                    cmd.Parameters.AddWithValue("@HoTen", "%" + txtTimKiemTen.Text.Trim() + "%");
                }

                if (conditions.Count > 0)
                {
                    query += " WHERE " + string.Join(" OR ", conditions); // Ghép điều kiện với OR
                }

                cmd.CommandText = query;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conn.Open();
                adapter.Fill(dt);
                conn.Close();

                dgvHopDong.DataSource = dt;
                dgvHopDong.Refresh();

                // Thiết lập tên cột bằng tiếng Việt
                dgvHopDong.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvHopDong.Columns["HoTen"].HeaderText = "Họ Tên";
                dgvHopDong.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                dgvHopDong.Columns["GioiTinh"].HeaderText = "Giới Tính";
                dgvHopDong.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dgvHopDong.Columns["DienThoai"].HeaderText = "Số Điện Thoại";

                // Thiết lập chế độ tự động điều chỉnh kích thước cột
                dgvHopDong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void LoadHopDong()
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "SELECT * FROM HopDongLaoDong";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvHopDong.DataSource = dt;

                // Thiết lập tên cột bằng tiếng Việt
                dgvHopDong.Columns["MaHD"].HeaderText = "Mã Hợp Đồng";
                dgvHopDong.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvHopDong.Columns["LoaiHD"].HeaderText = "Loại Hợp Đồng";
                dgvHopDong.Columns["NgayKy"].HeaderText = "Ngày Ký";
                dgvHopDong.Columns["NgayHetHan"].HeaderText = "Ngày Hết Hạn";
                dgvHopDong.Columns["NoiDung"].HeaderText = "Nội Dung";


                // Tự động co giãn cột
                dgvHopDong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Xóa nội dung nhập liệu
            txtMaHD.Clear();
            txtMaNV.Clear();
            txtLoaiHD.Clear();
            txtNoiDung.Clear();

            // Đặt lại DateTimePicker về trạng thái mặc định
            dtpNgayKy.Value = DateTime.Now;
            dtpNgayHetHan.Value = DateTime.Now;
            MessageBox.Show("Thêm mới thành công.");
            // Làm mới danh sách hợp đồng trên DataGridView
            LoadHopDong();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "INSERT INTO HopDongLaoDong (MaNV, LoaiHD, NgayKy, NgayHetHan, NoiDung) VALUES (@MaNV, @LoaiHD, @NgayKy, @NgayHetHan, @NoiDung)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", string.IsNullOrEmpty(txtMaNV.Text) ? (object)DBNull.Value : txtMaNV.Text);
                cmd.Parameters.AddWithValue("@LoaiHD", txtLoaiHD.Text);
                cmd.Parameters.AddWithValue("@NgayKy", dtpNgayKy.Value);
                cmd.Parameters.AddWithValue("@NgayHetHan", (object)dtpNgayHetHan.Value ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NoiDung", txtNoiDung.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LoadHopDong();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng chọn hợp đồng cần xóa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "DELETE FROM HopDongLaoDong WHERE MaHD=@MaHD";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", txtMaHD.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LoadHopDong();
            }
        }

        private void dgvHopDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHopDong.Rows.Count - 1)
                return;

            DataGridViewRow row = dgvHopDong.Rows[e.RowIndex];
            txtMaHD.Text = row.Cells["MaHD"].Value?.ToString();
            txtMaNV.Text = row.Cells["MaNV"].Value?.ToString();
            txtLoaiHD.Text = row.Cells["LoaiHD"].Value?.ToString();
            dtpNgayKy.Value = Convert.ToDateTime(row.Cells["NgayKy"].Value);
            dtpNgayHetHan.Value = row.Cells["NgayHetHan"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["NgayHetHan"].Value) : DateTime.Now;
            txtNoiDung.Text = row.Cells["NoiDung"].Value?.ToString();
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

        private void txtTimKiemTen_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
