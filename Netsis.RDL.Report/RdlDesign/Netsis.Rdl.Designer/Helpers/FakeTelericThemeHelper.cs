using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers
{

    internal static class FakeTelericThemeHelper
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int BORDER_SIZE = 25;
        public static Color FORM_BACKCOLOR = Color.FromArgb(191, 219, 255);
        public static Color FORM_BORDERCOLOR = Color.FromArgb(100, 170, 255);

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        internal static void ApplyToolBar(Form frm)
        {
            int buttonX = 35;
            frm.BackColor = FORM_BACKCOLOR;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Padding = new System.Windows.Forms.Padding(2, BORDER_SIZE, 2, 2);
            frm.Height += BORDER_SIZE;
            Label lb = new Label();
            lb.Location = new Point(buttonX, 0);
            lb.Size = new Size(frm.Width - (buttonX * 2), BORDER_SIZE);
            lb.TextAlign = ContentAlignment.MiddleCenter;
            frm.Controls.Add(lb);
            lb.Text = frm.Text;
            lb.BackColor = Color.Transparent;
            lb.ForeColor = SystemColors.ControlDarkDark;
            lb.Font = new System.Drawing.Font(lb.Font, FontStyle.Bold);

            Button btn = new Button();
            btn.Location = new Point(frm.Width - buttonX, 0);
            btn.Size = new Size(BORDER_SIZE, BORDER_SIZE);
            btn.Text = "X";
            btn.Font = new System.Drawing.Font(btn.Font, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = SystemColors.ControlDarkDark;
            btn.BackColor = Color.Transparent;            
            frm.Controls.Add(btn);

            frm.MouseDown += FakeTelericThemeHelper.Form1_MouseDown;
            lb.MouseDown += FakeTelericThemeHelper.Form1_MouseDown;
            btn.Click += btn_Click;
        }

        static void btn_Click(object sender, EventArgs e)
        {
            ((Form)((Button)sender).Parent).Close();
        }

        internal static void PaintToolBar(PaintEventArgs e, Form frm)
        {
            Color borderColor = FORM_BORDERCOLOR;
            ControlPaint.DrawBorder(e.Graphics, frm.ClientRectangle,
                                  borderColor, 2, ButtonBorderStyle.Solid,
                                  borderColor, BORDER_SIZE, ButtonBorderStyle.Outset,
                                  borderColor, 2, ButtonBorderStyle.Solid,
                                  borderColor, 2, ButtonBorderStyle.Solid);
        }

        private static void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                IntPtr handle = ((Control)sender).Handle;
                if (sender is Label)
                    handle = ((Control)sender).Parent.Handle;
                SendMessage(handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        internal static void ChangeTabColor(DrawItemEventArgs e, TabControl tb)
        {
            tb.TabPages[e.Index].BorderStyle = BorderStyle.None;            

            Font TabFont = e.Font;
            Brush BackBrush = new SolidBrush(FORM_BACKCOLOR); //Set background color
            Brush ForeBrush = new SolidBrush(Color.Black);//Set foreground color
            if (e.Index == tb.SelectedIndex)
            {
                TabFont = new Font(e.Font, FontStyle.Regular);
            }
            else
            {
                TabFont = e.Font;
            }
            string TabName = tb.TabPages[e.Index].Text;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.FillRectangle(BackBrush, e.Bounds);
            Rectangle r = e.Bounds;
            r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height - 3);
            e.Graphics.DrawString(TabName, TabFont, ForeBrush, r, sf);
            //Dispose objects
            sf.Dispose();
            if (e.Index == tb.SelectedIndex)
            {
                TabFont.Dispose();
                BackBrush.Dispose();
            }
            else
            {
                BackBrush.Dispose();
                ForeBrush.Dispose();
            }
        }

    }
}
