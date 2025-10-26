using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assignment_DuAnMau;

namespace Assignment_DuAnMau
{
    internal class RoundedPanel : Panel
    {
        public int CornerRadius { get; set; } = 2;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Tạo đường viền bo góc
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
            path.AddArc(Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
            path.AddArc(Width - CornerRadius, Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            path.AddArc(0, Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            path.CloseAllFigures();

            // Thiết lập vùng hiển thị
            this.Region = new Region(path);

            this.BackColor = Color.White;
            // Tùy chọn vẽ đường viền nếu cần
            using (Pen pen = new Pen(Color.Black, 4))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }
        }
    }
}
