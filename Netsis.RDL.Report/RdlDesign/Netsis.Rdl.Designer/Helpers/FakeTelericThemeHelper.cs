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

        static Point mouseInitialLocation = new Point(0, 0);

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int BORDER_SIZE = 25;
        public const int BOTTOM_BORDER_SIZE = 4;
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
            frm.Padding = new System.Windows.Forms.Padding(BOTTOM_BORDER_SIZE, BORDER_SIZE, BOTTOM_BORDER_SIZE, BOTTOM_BORDER_SIZE);
            frm.Height += BORDER_SIZE;
            frm.MouseDown += FakeTelericThemeHelper.Form1_MouseDown;

            Label lb = new Label();
            lb.Location = new Point(buttonX, 0);
            lb.Size = new Size(frm.Width - (buttonX * 3), BORDER_SIZE);
            lb.TextAlign = ContentAlignment.MiddleCenter;            
            lb.Text = frm.Text;
            lb.BackColor = Color.Transparent;
            lb.ForeColor = SystemColors.ControlDarkDark;
            lb.Font = new System.Drawing.Font(lb.Font, FontStyle.Bold);
            lb.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            lb.MouseDown += FakeTelericThemeHelper.Form1_MouseDown;            
            frm.Controls.Add(lb);

            Button btn = new Button();
            btn.Location = new Point(frm.Width - buttonX, 0);
            btn.Size = new Size(BORDER_SIZE, BORDER_SIZE);
            btn.Text = "X";
            btn.Font = new System.Drawing.Font(btn.Font, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = SystemColors.ControlDarkDark;
            btn.BackColor = Color.Transparent;
            btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn.Click += btn_Click;
            frm.Controls.Add(btn);           

            Button btnFullScreen = new Button();
            btnFullScreen.Location = new Point(frm.Width - buttonX - btn.Width, 0);
            btnFullScreen.Size = new Size(BORDER_SIZE, BORDER_SIZE);
            btnFullScreen.Text = "□";
            btnFullScreen.Font = new System.Drawing.Font(btn.Font, FontStyle.Bold);            
            btnFullScreen.FlatStyle = FlatStyle.Flat;
            btnFullScreen.FlatAppearance.BorderSize = 0;
            btnFullScreen.ForeColor = SystemColors.ControlDarkDark;
            btnFullScreen.BackColor = Color.Transparent;
            btnFullScreen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFullScreen.Click += btnFullScreen_Click;     
            frm.Controls.Add(btnFullScreen);                               
            
            Panel pnl = new Panel();
            pnl.Location = new Point(frm.Width - 13, frm.Height - 13);
            pnl.Size = new Size(13, 13);
            pnl.BackColor = Color.Blue;            
            pnl.Cursor = Cursors.SizeNWSE;
            pnl.MouseDown += pnl_MouseDown;
            pnl.MouseMove += pnl_MouseMove;
            pnl.MouseLeave += pnl_MouseLeave;
            pnl.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            frm.Controls.Add(pnl);
            
        }       

        static void btnFullScreen_Click(object sender, EventArgs e)
        {
            ChangeFullScreen(sender);
        }

        static void ChangeFullScreen(object sender)
        {
            Form frm = (Form)((Control)sender).Parent;
            if (frm.WindowState == FormWindowState.Maximized)
                frm.WindowState = FormWindowState.Normal;
            else
                frm.WindowState = FormWindowState.Maximized;
        }

        static void pnl_MouseLeave(object sender, EventArgs e)
        {
            mouseInitialLocation = new Point(0, 0);
        }

        static void pnl_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseInitialLocation.X > 0 && mouseInitialLocation.Y > 0)
            {
                Form frm = (Form)(((Control)sender).Parent);
                int x = e.Location.X - mouseInitialLocation.X;
                int y = e.Location.Y - mouseInitialLocation.Y;
                frm.Size = new Size(frm.Size.Width + x, frm.Size.Height + y);
            }
        }       

        static void pnl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseInitialLocation = e.Location;
        }        

        static void btn_Click(object sender, EventArgs e)
        {
            ((Form)((Button)sender).Parent).Close();
        }

        internal static void PaintToolBar(PaintEventArgs e, Form frm)
        {
            Color borderColor = FORM_BORDERCOLOR;
            ControlPaint.DrawBorder(e.Graphics, frm.ClientRectangle,
                                  borderColor, BOTTOM_BORDER_SIZE, ButtonBorderStyle.Solid,
                                  borderColor, BORDER_SIZE, ButtonBorderStyle.Outset,
                                  borderColor, BOTTOM_BORDER_SIZE, ButtonBorderStyle.Solid,
                                  borderColor, BOTTOM_BORDER_SIZE, ButtonBorderStyle.Solid);
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
