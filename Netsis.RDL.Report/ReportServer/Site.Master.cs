﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReportServer
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (SessionVariables.LoggedIn == true)
            {
                LiteralLogin.Text = string.Format("{0} - <a href=\"Logout.aspx\">Logout</a>", SessionVariables.LoggedEmail);

            }
                else
            {
                LiteralLogin.Text = string.Format("{0} - <a href=\"Login.aspx\">Login</a> / <a href=\"Signup.aspx\">Signup</a>", SessionVariables.LoggedEmail);
            }

        }
    }
}