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

namespace qlttnv
{
    public partial class FrmQLthongtinnhanvien : Form
    {
        public FrmQLthongtinnhanvien()
        {
            InitializeComponent();
        }

     

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = @"
                    SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.GioiTinh, nv.DiaChi, nv.DienThoai, pb.TenPB 
                    FROM NhanVien nv
                    JOIN PhongBan pb ON nv.MaPB = pb.MaPB";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNhanVien.DataSource = dt;

                // Định dạng ngày tháng trong DataGridView
                dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";

                // Thiết lập tiêu đề các cột
                dgvNhanVien.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvNhanVien.Columns["HoTen"].HeaderText = "Tên Nhân Viên";
                dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
                dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dgvNhanVien.Columns["DienThoai"].HeaderText = "Số Điện Thoại";
                dgvNhanVien.Columns["TenPB"].HeaderText = "Phòng Ban";

                // Tự động co giãn cột
                dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
                {
                    conn.Open(); // Mở kết nối

                    string query = "SELECT * FROM NhanVien WHERE MaNV LIKE @Search";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Search", "%" + txtTimMaNhanVien.Text.Trim() + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count == 0)
                        {
                            // Nếu không tìm thấy giảng viên
                            MessageBox.Show("Mã nhân viên  không tồn tại hoặc không khớp với tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            // Hiển thị kết quả tìm kiếm
                            dgvNhanVien.DataSource = table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm giảng viên: " + ex.Message);
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu click vào tiêu đề, dòng rỗng hoặc dòng "New Row"
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhanVien.Rows.Count - (dgvNhanVien.AllowUserToAddRows ? 1 : 0))
            {
                return;
            }

            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

            // Kiểm tra nếu các ô dữ liệu bị null
            if (row.Cells["MaNV"].Value == null || row.Cells["HoTen"].Value == null)
            {
                return;
            }

            txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
            txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
            dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
            cmbGioiTinh.Text = row.Cells["GioiTinh"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            txtDienThoai.Text = row.Cells["DienThoai"].Value.ToString();

            // Tìm và chọn đúng phòng ban trong ComboBox
            cmbMaPB.SelectedIndex = cmbMaPB.FindStringExact(row.Cells["TenPB"].Value.ToString());
        }
        private void ClearFields()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cmbGioiTinh.SelectedIndex = -1;
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            cmbMaPB.SelectedIndex = -1;
           
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
          MessageBox.Show("Thêm mới thành công.");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "UPDATE NhanVien SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, " +
                               "DiaChi = @DiaChi, DienThoai = @DienThoai, MaPB = @MaPB WHERE MaNV = @MaNV";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", cmbGioiTinh.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);
                    cmd.Parameters.AddWithValue("@MaPB", cmbMaPB.SelectedValue);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật nhân viên thành công!");
                    LoadData();
                    ClearFields();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa nhân viên thành công!");
                    LoadData();
                    ClearFields();
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, DiaChi, DienThoai, MaPB) " +
                               "VALUES (@HoTen, @NgaySinh, @GioiTinh, @DiaChi, @DienThoai, @MaPB)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", cmbGioiTinh.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);
                    cmd.Parameters.AddWithValue("@MaPB", cmbMaPB.SelectedValue);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhân viên thành công!");
                    LoadData();
                    ClearFields();
                }
            }
        }

        private void FrmQLthongtinnhanvien_Load(object sender, EventArgs e)
        {
            // Định dạng DateTimePicker
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            LoadData();
            LoadPhongBan();
        }
        private void LoadPhongBan()
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                string query = "SELECT MaPB, TenPB FROM PhongBan";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbMaPB.DataSource = dt;
                cmbMaPB.DisplayMember = "TenPB";
                cmbMaPB.ValueMember = "MaPB";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
