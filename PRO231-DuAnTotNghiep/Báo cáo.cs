using PRO231_DuAnTotNghiep;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PRO231
{
    public partial class FrmBaoCao : Form
    {

        public FrmBaoCao()
        {
            InitializeComponent();
            this.Load += FrmBaoCao_Load;
        }

        // Khi form load, load dữ liệu phòng ban và loại báo cáo.
        private void FrmBaoCao_Load(object sender, EventArgs e)
        {
            LoadPhongBan();
        }
        private void LoadPhongBan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
                {
                    conn.Open();
                    string query = "SELECT MaPB, TenPB FROM PhongBan";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbPhongBan.DataSource = dt;
                    cbPhongBan.DisplayMember = "TenPB";
                    cbPhongBan.ValueMember = "MaPB";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải phòng ban: " + ex.Message);
            }
        }

        private void LoadBaoCao(int maPB, string loaiBaoCao)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dungchung.chuoiKetNoi))
                {
                    conn.Open();
                    string query = "";

                    if (loaiBaoCao == "Khen Thưởng")
                    {
                        query = @"SELECT nv.HoTen AS 'Họ Tên', kt.NgayKT AS 'Ngày Khen Thưởng', 
                                 kt.NoiDung AS 'Nội Dung', kt.HinhThuc AS 'Hình Thức'
                          FROM KhenThuong kt
                          JOIN NhanVien nv ON kt.MaNV = nv.MaNV
                          WHERE nv.MaPB = @MaPB";
                    }
                    else if (loaiBaoCao == "Kỷ Luật")
                    {
                        query = @"SELECT nv.HoTen AS 'Họ Tên', kl.NgayKL AS 'Ngày Kỷ Luật', 
                                 kl.NoiDung AS 'Nội Dung', kl.HinhThuc AS 'Hình Thức'
                          FROM KyLuat kl
                          JOIN NhanVien nv ON kl.MaNV = nv.MaNV
                          WHERE nv.MaPB = @MaPB";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaPB", maPB);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvBaoCao.DataSource = dt;

                    // Định dạng hiển thị cột ngày theo dd/MM/yyyy
                    dgvBaoCao.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";

                    // Đặt tiêu đề cột
                    dgvBaoCao.Columns[0].HeaderText = "Họ Tên";
                    dgvBaoCao.Columns[1].HeaderText = loaiBaoCao == "Khen Thưởng" ? "Ngày Khen Thưởng" : "Ngày Kỷ Luật";
                    dgvBaoCao.Columns[2].HeaderText = "Nội Dung";
                    dgvBaoCao.Columns[3].HeaderText = "Hình Thức";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message);
            }
        }

        // Sự kiện nhấn nút Xem: kiểm tra chọn phòng ban và loại báo cáo, sau đó gọi hàm LoadBaoCao.
        private void btnXem_Click(object sender, EventArgs e)
        {
            if (cbPhongBan.SelectedValue == null || cbLoaiBaoCao.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn phòng ban và loại báo cáo!");
                return;
            }

            int maPB = Convert.ToInt32(cbPhongBan.SelectedValue);
            string loaiBaoCao = cbLoaiBaoCao.SelectedItem.ToString();

            LoadBaoCao(maPB, loaiBaoCao);
        }


        private void FrmBaoCao_Load_1(object sender, EventArgs e)
        {
            LoadPhongBan();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void roundedPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
