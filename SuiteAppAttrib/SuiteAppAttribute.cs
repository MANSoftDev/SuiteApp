using System;

namespace SuiteAppAttrib
{
	/// <summary>
	/// Custom attribute to be applied to PluginApps
	/// so container will know they should be included
	/// Attribute is applied to classes only
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public class SuiteAppAttribute : Attribute
	{
		private string m_strName;
		private string m_strGroup;

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="strName">App name</param>
		/// <param name="strGroup">Group name</param>
		public SuiteAppAttribute(string strName, string strGroup)
		{
			m_strName = strName;
			m_strGroup = strGroup;
		}

		/// <summary>
		/// Name of application
		/// </summary>
		public string Name
		{
			get{ return m_strName; }
		}

		/// <summary>
		/// Name of group to which the app
		/// should be assigned
		/// </summary>
		public string Group
		{
			get{ return m_strGroup; }
		}
	}
}
