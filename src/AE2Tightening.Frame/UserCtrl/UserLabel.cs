using System.Drawing;
using System.Windows.Forms;

namespace AE2Tightening.Frame
{
    public class UserLabel : Label
    {
        private Color bkColor = Color.LightGray;
        public override string Text
        {
            get => base.Text;
            set
            {
                if (base.Text == value)
                    return;
                BackColor = Color.Transparent;
                ForeColor = Color.White;
                base.Text = value;
                switch (Text)   
                {
                    case "OK":
                        bkColor = Color.Green;
                        break;
                    case "NG":
                        bkColor = Color.Red;
                        break;
                    default:
                        bkColor = Color.LightGray;
                        return;
                }
            }
        }
        public UserLabel() : base()
        {
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            pevent.Graphics.FillEllipse(new SolidBrush(bkColor), ClientRectangle);
        }
        /*
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using(Bitmap bmp = new Bitmap(Width, Height))
            {
                Graphics g = Graphics.FromImage(bmp);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                Brush brush = new SolidBrush(Color.Transparent);
                switch (Result)
                {
                    case EnumTdStatus.OK:
                        {
                            brush = new SolidBrush(Color.Green);
                            break;
                        }
                    case EnumTdStatus.NG:
                        {
                            brush = new SolidBrush(Color.Red);
                            break;
                        }
                    default:
                        return;
                }
                g.FillEllipse(brush, this.ClientRectangle);
                g.DrawString(Result.ToString(), Font, new SolidBrush(ForeColor), this.ClientRectangle,new StringFormat() { Alignment= StringAlignment.Center,LineAlignment= StringAlignment.Far});
                e.Graphics.DrawImageUnscaled(bmp, 0, 0);
            }
        }
        */
    }
}
