using fyiReporting.RdlEngine;
using Netsis.Framework.AJM.Contracts;
using Netsis.Framework.Service;
using Netsis.Framework.Service.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netsis.Rdl.AJMTest
{
    public partial class Form1 : Form
    {

        private INAjmService proxy = null;

        public Form1()
        {
            InitializeComponent();
            proxy = NWcfClientUtils.GetChannel<INAjmService>(NWcfServiceUtils.GetEndpointAddress());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = proxy.AddTask(CreateTaskParams("Immediate"), NAjmTaskRecurrence.Immediate().ExSettings(false));
            if (result.Status != NAjmTaskStatus.OK)
                MessageBox.Show(result.StatusText);
        }

        private static NAjmTaskInfo CreateTaskParams(string caller)
        {
            Dictionary<String, Object> pParam = new Dictionary<String, Object>();


            Dictionary<String, Object> pArray = new Dictionary<String, Object>();
            pArray.Add("ReportID", 1051);
            pArray.Add("RenderType", 1);
            pArray.Add("Parameters", pParam);           
            
            //
            var task = new NAjmTaskInfo()
            {
                AsmQualifiedTypeName = typeof(NReportRenderer).AssemblyQualifiedName,
                RequiresDBConnection = false,
                UserID = 1,
                ExecutionParams = pArray,
            };
            return task;
        }

    }
}
