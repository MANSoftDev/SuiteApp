using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DemoApp1
{
	/// <summary>
	/// Summary description for App1Form.
	/// </summary>
	[SuiteAppAttrib.SuiteAppAttribute("Demo1", "Demo Group1")]
	public class App1Form : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mnuFilePrint;
		private System.Windows.Forms.MenuItem mnuFileSave;
		private System.Windows.Forms.MenuItem menuItem2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public App1Form()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuFileSave = new System.Windows.Forms.MenuItem();
			this.mnuFilePrint = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuFileSave,
																					  this.mnuFilePrint,
																					  this.menuItem2});
			this.menuItem1.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.menuItem1.Text = "File";
			// 
			// mnuFileSave
			// 
			this.mnuFileSave.Index = 0;
			this.mnuFileSave.Text = "Save";
			this.mnuFileSave.Click += new System.EventHandler(this.OnSave);
			// 
			// mnuFilePrint
			// 
			this.mnuFilePrint.Index = 1;
			this.mnuFilePrint.Text = "Print";
			this.mnuFilePrint.Click += new System.EventHandler(this.OnPrint);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.MergeOrder = 99;
			this.menuItem2.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.menuItem2.Text = "E&xit Child";
			this.menuItem2.Click += new System.EventHandler(this.OnExit);
			// 
			// App1Form
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Menu = this.mainMenu1;
			this.Name = "App1Form";
			this.Text = "App1Form";

		}
		#endregion

		private void OnPrint(object sender, System.EventArgs e)
		{
			MessageBox.Show(this, "Print");
		}

		private void OnSave(object sender, System.EventArgs e)
		{
			MessageBox.Show(this, "Save");
		}

		private void OnExit(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
