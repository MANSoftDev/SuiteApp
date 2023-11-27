using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

namespace SuiteAppContainer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private Hashtable m_hashApps;
		private OutlookBar.OutlookBar m_OutlookBar;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mnuAboutAbout;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_OutlookBar = new OutlookBar.OutlookBar();
			m_OutlookBar.Location = new Point(0, 0);
			m_OutlookBar.Size = new Size(150, this.ClientSize.Height);
			m_OutlookBar.BorderStyle = BorderStyle.FixedSingle;
			Controls.Add(m_OutlookBar);
			m_OutlookBar.Initialize();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.mnuFileExit = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mnuAboutAbout = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuFileExit});
			this.menuItem1.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.menuItem1.Text = "&File";
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Index = 0;
			this.mnuFileExit.MergeOrder = 99;
			this.mnuFileExit.MergeType = System.Windows.Forms.MenuMerge.Replace;
			this.mnuFileExit.Text = "E&xit";
			this.mnuFileExit.Click += new System.EventHandler(this.OnExit);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuAboutAbout});
			this.menuItem2.Text = "About";
			// 
			// mnuAboutAbout
			// 
			this.mnuAboutAbout.Index = 0;
			this.mnuAboutAbout.Text = "About Suite";
			this.mnuAboutAbout.Click += new System.EventHandler(this.OnAbout);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "MainForm";
			this.Text = "Suite Container";
			this.Load += new System.EventHandler(this.OnLoad);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.Run(new MainForm());
		}

		private void OnLoad(object sender, System.EventArgs e)
		{
			m_hashApps = AppLoader.FindApps();

			// For sorting the groups
			Hashtable hashGroups = new Hashtable(m_hashApps.Count);

			// Iterate through any apps found and add them
			// to the Outlook Bar
			IDictionaryEnumerator Apps = m_hashApps.GetEnumerator();
			while( Apps.MoveNext() )
			{
				SuiteApp app = (SuiteApp)Apps.Value;

				// If the group hasn't been added yet
				if( !hashGroups.Contains(app.Group) )
				{
					OutlookBar.IconPanel panel = new OutlookBar.IconPanel();
					m_OutlookBar.AddBand( app.Group, panel );
					panel.AddIcon(app.AppName, Image.FromFile("img1.ico"), new EventHandler(OnSelectApp), app.Name );
					hashGroups.Add(app.Group, panel);
				}
				else
				{
					// Group exists so just add the app to it
					OutlookBar.IconPanel panel = (OutlookBar.IconPanel)hashGroups[app.Group];
					panel.AddIcon(app.AppName, Image.FromFile("img1.ico"), new EventHandler(OnSelectApp), app.Name );
				}
			}

			// Start with the first band
			m_OutlookBar.SelectBand(0);			
		}

		public void OnSelectApp(object sender, EventArgs e)
		{
			OutlookBar.PanelIcon panel = ((Control)sender).Tag as OutlookBar.PanelIcon;
			
			// Get the item clicked
			string strItem = panel.AppName;

			// Make sure the app is in the list
			if( m_hashApps.ContainsKey(strItem) )
			{
				// If the windows hasn't already been created do it now
				if( ((SuiteApp)m_hashApps[strItem]).Form == null )
				{
					// Load the assembly
					SuiteApp app = (SuiteApp)m_hashApps[strItem];
					Assembly asm = Assembly.LoadFile(app.Path);
					Type[] types = asm.GetTypes();
					
					// Create the application instance
					Form frm = (Form)Activator.CreateInstance(types[0]);

					// Set the parameters and show
					frm.MdiParent = this;
					frm.Show();
					// Set the form closing event so we can handle it
					frm.Closing += new CancelEventHandler(ChildFormClosing);

					// Save the form for later use
					((SuiteApp)m_hashApps[strItem]).Form = frm;

					// We're done for now
					return;
				}
				else
				{
					// Form exists so we just need to activate it
					((SuiteApp)m_hashApps[strItem]).Form.Activate();
				}
			}
			else
				throw new Exception("Application not found");
		}

		/// <summary>
		/// Handler for child form closing event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ChildFormClosing(object sender, CancelEventArgs e)
		{
			string strName = ((Form)sender).Text;

			// If the app is in the list then null it
			if( m_hashApps.ContainsKey(strName) )
				((SuiteApp)m_hashApps[strName]).Form = null;
		}

		private void OnExit(object sender, System.EventArgs e)
		{
			Close();
		}

		private void OnAbout(object sender, System.EventArgs e)
		{
			MessageBox.Show(this, "A Sweet Suite", "About Suite");
		}
	}
}
