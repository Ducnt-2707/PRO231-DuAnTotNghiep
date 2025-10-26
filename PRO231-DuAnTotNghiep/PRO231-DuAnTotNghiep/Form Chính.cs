using formQLkhenthuong;
using FrmQLPhongBan;
using PRO231;
using QLTaiKhoanNguoiDung;
using qlttnv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRO231_DuAnTotNghiep
{
    public partial class formmain : Form
    {
        public formmain()
        {
            InitializeComponent();
        }
        private string userRole; // Lưu vai trò người dùng


        public formmain(string role)
        {
            InitializeComponent();
            userRole = role;
            SetupInterface();
        }

        private void SetupInterface()
        {
            if (userRole == "User")
            {
                btnBaoCao.Enabled = true;
                btnKL.Enabled = true;
                btnKT.Enabled = true;
                btnQLHopDong.Enabled = true;
                btnNhanVien.Enabled = false;
                btnPhongBan.Enabled = false;
                btnQLTaiKhoan.Enabled = false;
            }
        }

        private void btnQLHopDong_Click(object sender, EventArgs e)
        {
            if (userRole == "User")
            {
                FrmHopDongLaoDong frm = new FrmHopDongLaoDong("User");
                this.Hide();
                frm.ShowDialog();
                this.Show();
            }
            else
            {
                FrmHopDongLaoDong frm = new FrmHopDongLaoDong("Admin");
                this.Hide();
                frm.ShowDialog();
                this.Show();
            }

        }

        private void btnKT_Click(object sender, EventArgs e)
        {
            if (userRole == "User")
            {
                FrmKhenThuong frm = new FrmKhenThuong("User");
                this.Hide();
                frm.ShowDialog();
                this.Show();
            }
            else
            {
                FrmKhenThuong frm = new FrmKhenThuong("Admin");
                this.Hide();
                frm.ShowDialog();
                this.Show();
            }

        }

        private void btnKL_Click(object sender, EventArgs e)
        {
            if (userRole == "User")
            {
                FrmKyLuat frm = new FrmKyLuat("User");
                this.Hide();
                frm.ShowDialog();
                this.Show();
            }
            else
            {
                FrmKyLuat frm = new FrmKyLuat("Admin");
                this.Hide();
                frm.ShowDialog();
                this.Show();
            }
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            FrmBaoCao frm = new FrmBaoCao();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            FrmQLthongtinnhanvien frm = new FrmQLthongtinnhanvien();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnPhongBan_Click(object sender, EventArgs e)
        {
            FrmPhongBan frm = new FrmPhongBan();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnQLTaiKhoan_Click(object sender, EventArgs e)
        {
            FrmTaiKhoanNguoiDung frm = new FrmTaiKhoanNguoiDung();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận đăng xuất",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide(); // Ẩn form hiện tại
                FrmLogin Login = new FrmLogin(); // Giả sử FormLogin là form đăng nhập
                Login.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmDoiMatKhau frm = new FrmDoiMatKhau(dungchung.TenDangNhap);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}
