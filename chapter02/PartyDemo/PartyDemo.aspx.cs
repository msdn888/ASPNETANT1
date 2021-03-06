using System;
using System.Collections.Generic;
using System.Text;


namespace SitePoint.Cookbook
{
	public partial class PartyDemo : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Party[] parties = new Party[]
			{
			  new Party(DateTime.Parse("1/23/2006"))
				, new Party(DateTime.Parse("12/25/2005"))
				, new Party(DateTime.Parse("5/25/2004"))
			};
			string result = Join("|", parties
				, delegate(Party item)
			  {
				TimeSpan ts = DateTime.Parse("11/24/2006") - item.PartyDate;
				return ((int)ts.TotalDays).ToString();
			  });
			Response.Write(result);
		}

		public static string Join<T>(string delimiter
                             , IEnumerable<T> items
                             , Converter<T, string> converter)
		{
		  StringBuilder builder = new StringBuilder();
		  foreach(T item in items)
		  {
			builder.Append(converter(item));
			builder.Append(delimiter);
		  }
		  if (builder.Length > 0) 
		  {
			builder.Length = builder.Length - delimiter.Length;
		  }
		  return builder.ToString();
		}
	}

	public class Party
	{
		public Party(DateTime partyDate)
		{
			this.partyDate = partyDate;
		}

		public DateTime PartyDate
		{
			get
			{
				return partyDate;
			}
		}

		DateTime partyDate;
	}

	public delegate TOutput Converter<TIn, TOutput>(TIn input);
}
