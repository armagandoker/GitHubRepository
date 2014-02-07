using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();	
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnZoomInActionActivated (object sender, System.EventArgs e)
	{
		throw new System.NotImplementedException ();
	}

	protected void OnZoomOutActionActivated (object sender, System.EventArgs e)
	{
		throw new System.NotImplementedException ();
	}

	protected void OnPrintActionActivated (object sender, System.EventArgs e)
	{
		throw new System.NotImplementedException ();
	}

	protected void OnPdfActionActivated (object sender, System.EventArgs e)
	{
		throw new System.NotImplementedException ();
	}

	protected void OnRefreshActionActivated (object sender, System.EventArgs e)
	{
		throw new System.NotImplementedException ();
	}

	protected void OnFileOpen_Activated (object sender, System.EventArgs e)
	{
			object []param= new object[4];
			param[0] = "Cancel";
			param[1] = Gtk.ResponseType.Cancel;
			param[2] = "Open";
			param[3] = Gtk.ResponseType.Accept;
			
			Gtk.FileChooserDialog fc=
			new Gtk.FileChooserDialog("Open File",
			                            null,
			                            Gtk.FileChooserAction.Open,
			                            param);
				
		
		if (fc.Run() == (int)Gtk.ResponseType.Accept) 
		{
			try
			{
												
				string filename = fc.Filename;		
			
				if (System.IO.File.Exists(filename))
				{
				
					string parameters = this.GetParameters(new Uri(filename));
				
					this.reportviewer1.LoadReport(new Uri(filename), parameters);
				}
			
			}
			catch(Exception ex)
			{
				Gtk.MessageDialog m = new Gtk.MessageDialog(null, Gtk.DialogFlags.Modal, Gtk.MessageType.Info,
					Gtk.ButtonsType.Ok, false, 
					"Error Opening File." + System.Environment.NewLine + ex.Message);
					
				m.Run();
				m.Destroy();
			}
		}
		//Don't forget to call Destroy() or the FileChooserDialog window won't get closed.
		fc.Destroy();
	
			
	}
	
	private string GetParameters(Uri sourcefile)
	{
		string parameters = "";
		string sourceRdl = System.IO.File.ReadAllText(sourcefile.AbsolutePath);
		fyiReporting.RDL.RDLParser parser = new fyiReporting.RDL.RDLParser(sourceRdl);
		parser.Parse();
		if (parser.Report.UserReportParameters.Count > 0)
		{
					
			int count=0;
			foreach (fyiReporting.RDL.UserReportParameter rp in parser.Report.UserReportParameters) 
			{
			 	parameters += "&" + rp.Name + "=";
			}
			
			fyiReporting.RdlGtkViewer.ParameterPrompt prompt = new fyiReporting.RdlGtkViewer.ParameterPrompt();
			prompt.Parameters = parameters;
			if (prompt.Run() == (int)Gtk.ResponseType.Ok) 
			{
				parameters = prompt.Parameters;
			}
			prompt.Destroy();
			
		}	
		
		return parameters;
	}
	
}
