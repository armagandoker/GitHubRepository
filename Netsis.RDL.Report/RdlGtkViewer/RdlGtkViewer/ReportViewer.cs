// 
//  ReportViewer.cs
//  
//  Author:
//       Krzysztof Marecki
// 
//  Copyright (c) 2010 Krzysztof Marecki
//  Copyright (c) 2012 Peter Gill
// 
// This file is part of the NReports project
// This file is part of the My-FyiReporting project 
//	
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;

using Gtk;
using fyiReporting.RDL;

namespace fyiReporting.RdlGtkViewer
{	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ReportViewer : Gtk.Bin
	{
		private Report report;
		private Pages pages;
		private PrintOperation printing;
		
		public NeedPassword DataSourceReferencePassword = null;
		
		public string connstr_param_name = "connection_string";
		public string ConnectionStringParameterName {
			get { return connstr_param_name; }
			set { connstr_param_name = value; }
		}
		
		public string conntype_param_name = "connection_type";
		public string ConnectionTypeParameterName {
			get { return conntype_param_name; }
			set { conntype_param_name = value; }
		}
		
		public ListDictionary Parameters { get; private set; }
		
		bool show_errors;
		public bool ShowErrors {
			get {
				return show_errors;
			}
			set {
				show_errors = value;
				CheckVisibility ();
			}
		}
		
		bool show_params;
		public bool ShowParameters {
			get {
				return show_params;
			}
			set { 
				show_params = value; 
				CheckVisibility ();
			}
		}
		
		public Uri SourceFile { get; private set; }
		
		public ReportViewer ()
		{
			this.Build ();
			Parameters = new ListDictionary ();
			
			this.errorsAction.Toggled += OnErrorsActionToggled;
			DisableActions ();
			ShowErrors = false;
		}
		
		public void LoadReport(Uri filename, string parameters, string connectionString)
		{

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename.AbsolutePath);

            foreach (XmlNode node in xmlDoc.GetElementsByTagName("ConnectString"))
            {
                node.InnerText = connectionString;
            }

            xmlDoc.Save(filename.AbsolutePath);

			LoadReport(filename, parameters);
        }
		
		
		/// <summary>
		/// Loads the report.
		/// </summary>
		/// <param name='filename'>
		/// Filename.
		/// </param>
		public void LoadReport(Uri filename)
		{
			LoadReport(filename, "");
		}
				
		/// <summary>
		/// Loads the report.
		/// </summary>
		/// <param name='filename'>
		/// Filename.
		/// </param>
		/// <param name='parameters'>
		/// Example: parameter1=someValue&parameter2=anotherValue
		/// </param>
		public void LoadReport(Uri sourcefile, string parameters)
		{
			SourceFile = sourcefile;
						
			string source;
			// Any parameters?  e.g.  file1.rdl?orderid=5 
			if (parameters.Trim() != "")
			{
			    this.Parameters = this.GetParmeters(parameters);
			}
			else 
			{
			    this.Parameters = null;
			}
			
			// Obtain the source 
			source = System.IO.File.ReadAllText(sourcefile.AbsolutePath);
			// GetSource is omitted: all it does is read the file.
			// Compile the report 
			report = this.GetReport(source);
			AddParameterControls ();		
			
			RefreshReport ();
		}
		
		void RefreshReport ()
		{
			SetParametersFromControls ();
			report.RunGetData (Parameters);
			pages = report.BuildPages ();
			
			
			foreach (Gtk.Widget w in vboxPages.AllChildren)
			{
				vboxPages.Remove(w);
			}
			
			for (int pageCount = 0; pageCount < pages.Count; pageCount++)
			{
				ReportArea area = new ReportArea();
				area.SetReport (report, pages[pageCount]);
				//area.Scale
				vboxPages.Add(area);
			}
			this.ShowAll();
			
			if (report.ErrorMaxSeverity > 0)
				SetErrorMessages (report.ErrorItems);
			
//			Title = string.Format ("RDL report viewer - {0}", report.Name);
			EnableActions ();
			CheckVisibility ();
		}
		
		
		protected virtual void OnZoomOutActionActivated (object sender, System.EventArgs e)
		{
			foreach (Gtk.Widget w in vboxPages.AllChildren)
			{
				if (w is ReportArea)
				{
					((ReportArea)w).Scale -= 0.1f;
				}
			}
			//reportarea.Scale -= 0.1f;
		}

		protected virtual void OnZoomInActionActivated (object sender, System.EventArgs e)
		{
			foreach (Gtk.Widget w in vboxPages.AllChildren)
			{
				if (w is ReportArea)
				{
					((ReportArea)w).Scale += 0.1f;
				}
			}
			//reportarea.Scale += 0.1f;
		}
		
		// GetParameters creates a list dictionary
      // consisting of a report parameter name and a value.
      private System.Collections.Specialized.ListDictionary GetParmeters(string parms)
      {
            System.Collections.Specialized.ListDictionary ld = new System.Collections.Specialized.ListDictionary();
            if (parms == null)
			{
                  return ld; // dictionary will be empty in this case
			}

            // parms are separated by & 

            char[] breakChars = new char[] {'&'};
            string[] ps = parms.Split(breakChars);

            foreach (string p in ps)
            {
                  int iEq = p.IndexOf("=");
                  if (iEq > 0)
                  {
                        string name = p.Substring(0, iEq);
                        string val = p.Substring(iEq+1);
                        ld.Add(name, val);
                  }
            }
            return ld;
      }
		
		private Report GetReport(string reportSource)
		{
		    // Now parse the file 
		
		    RDLParser rdlp;
		    Report r;
		
			rdlp =  new RDLParser(reportSource);
			// RDLParser takes RDL XML and Parse compiles the report
			
			r = rdlp.Parse();
			if (r.ErrorMaxSeverity > 0) 
			{
			
			    foreach (string emsg in r.ErrorItems)
			    {
			          Console.WriteLine(emsg);
			    }
			
			    int severity = r.ErrorMaxSeverity;
				r.ErrorReset();
			    if (severity > 4)
			    {
			          r = null; // don't return when severe errors
			    }
			}
		
		    return r;
		}
		

		void OnErrorsActionToggled (object sender, EventArgs e)
		{
			ShowErrors = errorsAction.Active;
		}
		
		void CheckVisibility ()
		{
			if (ShowErrors) {
					scrolledwindowErrors.ShowAll ();
				} else {
					scrolledwindowErrors.HideAll ();
				}
			if (ShowParameters) {
				vboxParameters.ShowAll ();
			} else {
				vboxParameters.HideAll ();
			}	
		}
		
		protected override void OnShown ()
		{
			base.OnShown ();
			CheckVisibility ();
		}

		void DisableActions ()
		{
			PdfAction.Sensitive = false;
			refreshAction.Sensitive = false;
			printAction.Sensitive = false;
			ZoomInAction.Sensitive = false;
			ZoomOutAction.Sensitive = false;
		}
		
		void EnableActions ()
		{
			PdfAction.Sensitive = true;
			refreshAction.Sensitive = true;
			printAction.Sensitive = true;
			ZoomInAction.Sensitive = true;
			ZoomOutAction.Sensitive = true;
		}
		
		void AddParameterControls ()
		{
			foreach (Widget child in vboxParameters.Children) {
				vboxParameters.Remove (child);
			}
			foreach (UserReportParameter rp in report.UserReportParameters) {
				HBox hbox = new HBox ();
				Label labelPrompt = new Label ();
				labelPrompt.SetAlignment (0, 0.5f);
				labelPrompt.Text = string.Format ("{0} :", rp.Prompt);
				hbox.PackStart (labelPrompt, true, true, 0);
				Entry entryValue = new Entry ();
				if (Parameters.Contains (rp.Name)) {
					if (Parameters [rp.Name] != null) {
						entryValue.Text = Parameters [rp.Name].ToString ();
					}
				} else {
					if (rp.DefaultValue != null) {
						StringBuilder sb = new StringBuilder ();
						for (int i = 0; i < rp.DefaultValue.Length; i++) {
							if (i > 0)
								sb.Append (", ");
							sb.Append (rp.DefaultValue[i].ToString ());
						}
						entryValue.Text = sb.ToString ();
					}
				}
				hbox.PackStart (entryValue, false, false, 0);
				vboxParameters.PackStart (hbox, false, false, 0);
			}
		}
		
		void SetParametersFromControls ()
		{	
			int i = 0;
			foreach (UserReportParameter rp in report.UserReportParameters) {
				HBox hbox = (HBox) vboxParameters.Children[i];
				Entry entry = (Entry) hbox.Children [1];
				//parameters.Add (rp.Name, entry.Text);
				Parameters [rp.Name] = entry.Text;
				i++;
			}
		}

		void SetErrorMessages (IList errors)
		{
			textviewErrors.Buffer.Clear ();
			
			StringBuilder msgs = new StringBuilder ();
			foreach (var error in errors)
				msgs.AppendLine (error.ToString ());
			
			textviewErrors.Buffer.Text = msgs.ToString ();
		}

		protected virtual void OnPdfActionActivated (object sender, System.EventArgs e)
		{

			// *********************************
			object []param= new object[4];
			param[0] = "Cancel";
			param[1] = Gtk.ResponseType.Cancel;
			param[2] = "Save";
			param[3] = Gtk.ResponseType.Accept;
			
			Gtk.FileChooserDialog fc=
			new Gtk.FileChooserDialog("Save File As",
			                            null,
			                            Gtk.FileChooserAction.Save,
			                            param);
			
			Gtk.FileFilter pdfFilter = new Gtk.FileFilter();
			pdfFilter.Name = "PDF";
			
			Gtk.FileFilter csvFilter = new Gtk.FileFilter();
			csvFilter.Name = "CSV";
			
			Gtk.FileFilter asphtmlFilter = new Gtk.FileFilter();
			asphtmlFilter.Name = "ASP HTML";
			
			Gtk.FileFilter excel2003 = new Gtk.FileFilter();
			excel2003.Name = "Excel 2003";
			
			Gtk.FileFilter htmlFilter = new Gtk.FileFilter();
			htmlFilter.Name = "HTML";
			
			Gtk.FileFilter mhtmlFilter = new Gtk.FileFilter();
			mhtmlFilter.Name = "MHTML";
			
			Gtk.FileFilter rtfFilter = new Gtk.FileFilter();
			rtfFilter.Name = "RTF";
			
			Gtk.FileFilter xmlFilter = new Gtk.FileFilter();
			xmlFilter.Name = "XML";
					
			fc.AddFilter(pdfFilter);
			fc.AddFilter(csvFilter);
			fc.AddFilter(asphtmlFilter);
			fc.AddFilter(excel2003);
			fc.AddFilter(htmlFilter);
			fc.AddFilter(mhtmlFilter);
			fc.AddFilter(xmlFilter);
			
			if (fc.Run() == (int)Gtk.ResponseType.Accept) 
			{
				try
				{
					// Must use the RunGetData before each export or there is no data.
                	report.RunGetData(this.Parameters); 
										
					string filename = fc.Filename;		
					OutputPresentationType exportType = OutputPresentationType.PDF;
					if(fc.Filter.Name == "CSV")
					{
						exportType = OutputPresentationType.CSV;
						if (filename.ToLower().Trim().EndsWith(".csv") == false)
						{
							filename = filename + ".csv";
						}
					}
					else if(fc.Filter.Name == "PDF")
					{
						exportType = OutputPresentationType.PDF;
						if (filename.ToLower().Trim().EndsWith(".pdf") == false)
						{
							filename = filename + ".pdf";
						}
					}
					else if(fc.Filter.Name == "ASP HTML")
					{
						exportType = OutputPresentationType.ASPHTML;
						if (filename.ToLower().Trim().EndsWith(".asphtml") == false)
						{
							filename = filename + ".asphtml";
						}
					}
					else if(fc.Filter.Name == "Excel 2003")
					{
						exportType = OutputPresentationType.Excel;
						if (filename.ToLower().Trim().EndsWith(".xlsx") == false)
						{
							filename = filename + ".xlsx";
						}
					}
					else if(fc.Filter.Name == "HTML")
					{
						exportType = OutputPresentationType.HTML;
						if (filename.ToLower().Trim().EndsWith(".html") == false)
						{
							filename = filename + ".html";
						}
					}
					else if(fc.Filter.Name == "MHTML")
					{
						exportType = OutputPresentationType.MHTML;
						if (filename.ToLower().Trim().EndsWith(".mhtml") == false)
						{
							filename = filename + ".mhtml";
						}
					}
					else if(fc.Filter.Name == "XML")
					{
						exportType = OutputPresentationType.XML;
						if (filename.ToLower().Trim().EndsWith(".xml") == false)
						{
							filename = filename + ".xml";
						}
					}
					
					ExportReport(report, filename, exportType);				
				}
				catch(Exception ex)
				{
					Gtk.MessageDialog m = new Gtk.MessageDialog(null, Gtk.DialogFlags.Modal, Gtk.MessageType.Info,
						Gtk.ButtonsType.Ok, false, 
						"Error Saving Copy of PDF." + System.Environment.NewLine + ex.Message);
						
					m.Run();
					m.Destroy();
				}
			}
			//Don't forget to call Destroy() or the FileChooserDialog window won't get closed.
			fc.Destroy();
			
			
		}
		
	  /// <summary>
	  /// Save the report to the output selected.
	  /// </summary>
	  /// <param name='report'>
	  /// Report.
	  /// </param>
	  /// <param name='FileName'>
	  /// File name.
	  /// </param>
      private void ExportReport(Report report, string FileName, OutputPresentationType exportType)
      {
            OneFileStreamGen sg=null;

            try 
            {
                  sg = new OneFileStreamGen(FileName, true);
                  report.RunRender(sg, exportType);     
            }
            catch(Exception ex)
            {
				Gtk.MessageDialog m = new Gtk.MessageDialog(null, Gtk.DialogFlags.Modal, Gtk.MessageType.Error,
						Gtk.ButtonsType.Ok, false, 
						ex.Message);
						
					m.Run();
					m.Destroy();
            }
            finally 
            {
                  if (sg != null)
                  {
                        sg.CloseMainStream();
                  }
            }
            return;
      }
		
		protected virtual void OnPrintActionActivated (object sender, System.EventArgs e)
		{
			using (PrintContext context = new PrintContext (GdkWindow.Handle)) {
				
				printing = new PrintOperation ();
				printing.Unit = Unit.Points;
				printing.UseFullPage = false;
				
				printing.BeginPrint += HandlePrintBeginPrint;
				printing.DrawPage += HandlePrintDrawPage;
				printing.EndPrint += HandlePrintEndPrint;
				
				printing.Run (PrintOperationAction.PrintDialog, null);
			}
		}

		void HandlePrintBeginPrint (object o, BeginPrintArgs args)
		{
			printing.NPages = 1;
		}

		void HandlePrintDrawPage (object o, DrawPageArgs args)
		{
			Cairo.Context g = args.Context.CairoContext;
		
			RenderCairo render = new RenderCairo (g);
			render.RunPages (pages);	
		}

		void HandlePrintEndPrint (object o, EndPrintArgs args)
		{
			
		}

		protected virtual void OnRefreshActionActivated (object sender, System.EventArgs e)
		{
			RefreshReport ();
		}
		
		int hpanedWidth = 0;
		void SetHPanedPosition ()
		{
			int textviewWidth = scrolledwindowErrors.Allocation.Width +10;
			hpanedReport.Position = hpanedWidth - textviewWidth;
		}
		
		protected virtual void OnHpanedReportSizeAllocated (object o, Gtk.SizeAllocatedArgs args)
		{
			if (args.Allocation.Width != hpanedWidth) {
				hpanedWidth = args.Allocation.Width;
				SetHPanedPosition ();
			}
		}
	}
}

