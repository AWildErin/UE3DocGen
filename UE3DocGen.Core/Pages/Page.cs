using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UE3DocGen.Core.Pages;

/// <summary>
/// UE3 Documentation page.
/// </summary>
internal class Page
{
	public string Title { get; set; } = "NO NAME DEFINED";

	/// <summary>
	/// Brief summary of the page
	/// </summary>
	public string Description { get; set; } = "NO DESCRIPTION DEFINED";

	/// <summary>
	/// Minimum version at which this document is valid from
	/// </summary>
	public int Version { get; set; } = 0;

	/// <summary>
	/// Non vital warnings that should be output for each page generation
	/// </summary>
	public List<string> PageWarnings { get; set; } = new();


	/// <summary>
	/// File path to the file
	/// </summary>
	public string FilePath { get; set; }

	/// <summary>
	/// Raw unparsed text
	/// </summary>
	public string RawText { get; set; } = string.Empty;

	/// <summary>
	/// Parsed HTML text
	/// </summary>
	public string HtmlText { get; set; } = string.Empty;

	/// <summary>
	/// Language for this page
	/// </summary>
	public LocalisationCode LocalisationCode { get; set; }

	/// <summary>
	/// three letter for the language
	/// </summary>
	/// @todo Maybe we should consider moving this to the standard en-US like modern UE4 is. We made it like this because of UE3
	public string LocalisationTag { get; set; } = string.Empty;

	public Page( string filePath )
	{
		FilePath = filePath;

		if ( File.Exists( FilePath ) )
		{
			RawText = File.ReadAllText( filePath );
		}

		// Extract the locale code
		var fileName = Path.GetFileName( FilePath );
		var fileNameBrokeUp = fileName.Split( "." );
		// 3 would mean Name . Locale . udn

		// @todo This is really not robust!! We should do this better
		if ( fileNameBrokeUp.Count() >= 3 && fileNameBrokeUp[2] == "udn" )
		{
			LocalisationTag = fileNameBrokeUp[1];

			switch ( LocalisationTag )
			{
				case "INT": LocalisationCode = LocalisationCode.International; break;
				case "JPN": LocalisationCode = LocalisationCode.Japanese; break;
				case "DEU": LocalisationCode = LocalisationCode.German; break;
				case "FRA": LocalisationCode = LocalisationCode.French; break;
				case "ESN": LocalisationCode = LocalisationCode.SpanishSpain; break;
				case "ESM": LocalisationCode = LocalisationCode.SpanishMexio; break;
				case "ITA": LocalisationCode = LocalisationCode.Italian; break;
				case "KOR": LocalisationCode = LocalisationCode.Korean; break;
				case "CHN": LocalisationCode = LocalisationCode.Chinese; break;
				case "POL": LocalisationCode = LocalisationCode.Polish; break;
				case "RUS": LocalisationCode = LocalisationCode.Russian; break;
				case "CZE": LocalisationCode = LocalisationCode.Czech; break;
				case "HUN": LocalisationCode = LocalisationCode.Hungarian; break;
			}
		}
	}

	public void WritePage( string OutputFile )
	{
		var sb = new StringBuilder();
		sb.AppendLine( "<html>" );
		sb.AppendLine( "<head>" );
		sb.AppendLine( $"<title>{Title}</title>" );
		sb.AppendLine( "</head>" );
		sb.AppendLine( "<body>" );
		sb.AppendLine( $"{HtmlText}" );
		sb.AppendLine( "</body>" );
		sb.AppendLine( "</html>" );

		File.WriteAllText( OutputFile, sb.ToString() );
	}
}
