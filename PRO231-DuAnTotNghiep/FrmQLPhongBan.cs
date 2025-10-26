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

namespace FrmQLPhongBan
{
    public partial class FrmPhongBan : Form
    {
        public FrmPhongBan()
        {
            InitializeComponent();
        }

        private void FrmQLPhongBan_Load(object sender, EventArgs e)
        {
            LoadPhongBan();

        }
        private void LoadPhongBan()
        {
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM PhongBan";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPhongBan.DataSource = dt;

                    // Thiết lập tiêu đề cột bằng tiếng Việt
                    dgvPhongBan.Columns["MaPB"].HeaderText = "Mã Phòng Ban";
                    dgvPhongBan.Columns["TenPB"].HeaderText = "Tên Phòng Ban";
                    dgvPhongBan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtMaPB.Clear();
            txtTenPB.Clear();
            MessageBox.Show("Thêm mới thành công.");
            LoadPhongBan();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPB.Text))
            {
                MessageBox.Show("Bạn chưa chọn phòng ban để sửa!");
                return;
            }

            if (string.IsNullOrEmpty(txtTenPB.Text))
            {
                MessageBox.Show("Bạn chưa nhập Tên phòng ban mới!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE PhongBan SET TenPB = @TenPB WHERE MaPB = @MaPB";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenPB", txtTenPB.Text);
                        cmd.Parameters.AddWithValue("@MaPB", txtMaPB.Text);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Cập nhật phòng ban thành công!");
                    LoadPhongBan();
                    txtMaPB.Clear();
                    txtTenPB.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPB.Text))
            {
                MessageBox.Show("Bạn chưa chọn phòng ban để xóa!");
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa phòng ban này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No)
                return;

            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM PhongBan WHERE MaPB = @MaPB";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPB", txtMaPB.Text);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Xóa phòng ban thành công!");
                    LoadPhongBan();
                    txtMaPB.Clear();
                    txtTenPB.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenPB.Text))
            {
                MessageBox.Show("Bạn chưa nhập Tên phòng ban!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO PhongBan (TenPB) VALUES (@TenPB)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenPB", txtTenPB.Text);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Thêm phòng ban thành công!");
                    LoadPhongBan();
                    txtMaPB.Clear();
                    txtTenPB.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm phòng ban: " + ex.Message);
                }
            }
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

        private void dgvPhongBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // đảm bảo không phải header
            {
                DataGridViewRow row = dgvPhongBan.Rows[e.RowIndex];
                txtMaPB.Text = row.Cells["MaPB"].Value.ToString();
                txtTenPB.Text = row.Cells["TenPB"].Value.ToString();
            }
        }
    }
}
