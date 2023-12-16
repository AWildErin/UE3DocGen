using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace UE3DocGen.Core.Pages;

internal class PageParser
{
	internal class PageYaml
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int? Version { get; set; }
	}

	private MarkdownPipeline Pipeline;

	public PageParser()
	{
		Pipeline = new MarkdownPipelineBuilder()
			//.UseAdvancedExtensions()
			.UseYamlFrontMatter()
			.Build();
	}

	public string ToHtml( string pageText )
	{
		return Markdown.ToHtml( pageText, Pipeline );
	}

	/// <summary>
	/// Parses a doc page into our page class
	/// </summary>
	/// <param name="pageText">Text of the .udn page</param>
	/// <returns>Class representation of the document page</returns>
	public Page ParsePage( string filePath )
	{
		Debug.Assert( File.Exists( filePath ) );

		Page page = new( filePath );
		var md = Markdown.Parse( page.RawText, Pipeline );

		// Get the first YAML block
		var frontMatter = md.Descendants<YamlFrontMatterBlock>().FirstOrDefault();

		if ( frontMatter != null )
		{
			var yamlDeserializer = new DeserializerBuilder()
				//.WithNamingConvention( CamelCaseNamingConvention.Instance )
				.Build();

			var yaml = yamlDeserializer.Deserialize<PageYaml>(
				frontMatter
				.Lines // StringLineGroup[]
				.Lines // StringLine[]
				.OrderByDescending( x => x.Line )
				.Select( x => $"{x}\n" )
				.ToList()
				.Select( x => x.Replace( "---", string.Empty ) )
				.Where( x => !string.IsNullOrWhiteSpace( x ) )
				.Aggregate( ( s, agg ) => agg + s )
			);

			if ( yaml.Title != null )
			{
				page.Title = yaml.Title;
			}

			if ( yaml.Description != null )
			{
				page.Description = yaml.Description;
			}

			if ( yaml.Version != null )
			{
				page.Version = yaml.Version.Value;
			}

		}
		else
		{
			page.PageWarnings.Add( "Page does not have a YAML front matter block!" );
		}

		page.HtmlText = md.ToHtml( Pipeline );

		return page;
	}
}
