using System;
using System.Reflection;
using System.Collections;

namespace SuiteAppContainer
{
	/// <summary>
	/// Summary description for AppLoader.
	/// </summary>
	public class AppLoader
	{
		public AppLoader()
		{
		}

		/// <summary>
		/// Searches application path for dll's that have the
		/// SuiteAppAttribute
		/// </summary>
		public static Hashtable FindApps()
		{
			// Create hashtable to fill in
			Hashtable hashAssemblies = new Hashtable();

			// Get the current application path
			string strPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
			
			// Iterate through all dll's in this path
			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(strPath);
			foreach( System.IO.FileInfo file in di.GetFiles("*.dll") )
			{
				// Load the assembly so we can query for info about it.
				System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFile(file.FullName);
				
				// Iterate through each module in this assembly
				foreach( System.Reflection.Module mod in asm.GetModules() )
				{
					// Iterate through the types in this module
					foreach( Type t in mod.GetTypes() )
					{		
						// Check for the custom attribute and get the group and name
						object[] attributes = t.GetCustomAttributes(typeof(SuiteAppAttrib.SuiteAppAttribute), true);
						if( attributes.Length == 1 )
						{
							// Get the app name and group from the attribute
							string strName = ((SuiteAppAttrib.SuiteAppAttribute)attributes[0]).Name;
							string strGroup = ((SuiteAppAttrib.SuiteAppAttribute)attributes[0]).Group;
						
							// Create a new app instance and add it to the list
							SuiteApp app = new SuiteApp(t.Name, file.FullName, strName, strGroup);
							
							// Make sure the names isn't already being used
							if( hashAssemblies.ContainsKey(strName) )
								throw new Exception("Name already in use.");

							hashAssemblies.Add(t.Name, app);
						}
					}
				}
			}

			return hashAssemblies;
		}
	}

	/// <summary>
	/// Helper class to maintain details about the application
	/// </summary>
	public sealed class SuiteApp
	{
		private readonly string m_strGroup;
		private readonly string m_strName;
		private readonly string m_strAppName;
		private readonly string m_strPath;
		private System.Windows.Forms.Form m_oWindow = null;

		public SuiteApp(string strName, string strPath, string strAppName, string strGroup)
		{
			m_strName = strName;
			m_strPath = strPath;
			m_strAppName = strAppName;
			m_strGroup = strGroup;
		}

		#region Properties
		
		public string Name
		{
			get{ return m_strName; }
		}

		public string AppName
		{
			get{ return m_strAppName; }
		}

		public string Path
		{
			get{ return m_strPath; }
		}

		public string Group
		{
			get{ return m_strGroup; }
		}

		public System.Windows.Forms.Form Form
		{
			get{ return m_oWindow; }
			set{ m_oWindow = value; }
		}

		#endregion
	}
}
