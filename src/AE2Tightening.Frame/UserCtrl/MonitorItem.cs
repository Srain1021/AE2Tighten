using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlTypes;

namespace AE2Tightening.Frame
{
    public partial class MonitorItem : UserControl
    {
        private string txt = "Monitor";
        private bool status = false;
        private Color okColor = Color.Green;
        private Color ngColor = Color.Red;
        [Browsable(true)]
        [Category("外观"), Description("True Color")]
        public Color TrueColor
        {
            get => okColor;
            set{
                okColor = value;
                Invalidate();
            }
        }
        [Browsable(true)]
        [Category("外观"), Description("False Color")]
        public Color FalseColor
        {
            get => ngColor;
            set
            {
                ngColor = value;
                Invalidate();
            }
        }
        [Browsable(true)]
        [Category("外观"),Description("文本值")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => txt;
            set
            {
                txt = value;
                Invalidate();
            }
        }

        [Browsable(true),Bindable(true)]
        [Category("外观"),Description("状态值")]
        public bool Status
        {
            get => status;
            set
            {
                status = value;
                Invalidate();
            }
        }
        public MonitorItem()
        {
            InitializeComponent();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            using (Graphics g = this.CreateGraphics())
            {
                SizeF size = g.MeasureString(Text, Font);
                int h = (int)size.Height + 10;
                int w = (int)size.Width + h;
                this.MinimumSize = new Size(w, h);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = this.CreateGraphics())
            {
                Rectangle rect = this.ClientRectangle;
                if (!string.IsNullOrEmpty(Text))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    using (var brush = new SolidBrush(ForeColor))
                        g.DrawString(Text, Font, brush, rect,new StringFormat { 
                            LineAlignment = StringAlignment.Center
                        });
                    SizeF size = g.MeasureString(Text, Font);
                    Color lightColor = Status ? TrueColor : FalseColor;
                    using (var brush = new SolidBrush(lightColor))
                    {
                        float h = size.Height + 10;
                        float w = h;
                        float x = Width - h - 2;
                        float y = (Height - h) / 2;
                        //Text = $"{x}:{y}:{w}:{h}";
                        g.FillEllipse(brush, x, y, w, h);
                    }
                }
            }
        }
    }
}
