using System.Diagnostics;

namespace UE3DocGen.Tests.Pages;

public class PageParsingTests
{
	private string TestPageIntFile = "S:\\unreal\\UE3DocGen\\TestResources\\Pages\\TestPage.INT.udn";
	private string TestPageJpnFile = "S:\\unreal\\UE3DocGen\\TestResources\\Pages\\TestPage.JPN.udn";
	private string EmptyPageIntFile = "S:\\unreal\\UE3DocGen\\TestResources\\Pages\\EmptyPage.JPN.udn";

	[OneTimeSetUp]
	public void Setup()
	{
		// Get the test resource folder here instead of hardcoded paths
	}

	[Test]
	public void TestPageInt()
	{
		PageParser parser = new PageParser();
		var page = parser.ParsePage( TestPageIntFile );

		Assert.That( page.Title, Is.EqualTo( "Test Page" ) );
		Assert.That( page.Description, Is.EqualTo( "A brief summary for the page" ) );
		Assert.That( page.Version, Is.EqualTo( 100 ) );
		Assert.That( page.LocalisationCode, Is.EqualTo( LocalisationCode.International ) );

	}

	[Test]
	public void TestPageJpn()
	{
		PageParser parser = new PageParser();
		var page = parser.ParsePage( TestPageJpnFile );

		Assert.That( page.Title, Is.EqualTo( "Test Page" ) );
		Assert.That( page.Description, Is.EqualTo( "A brief summary for the page" ) );
		Assert.That( page.Version, Is.EqualTo( 100 ) );
		Assert.That( page.LocalisationCode, Is.EqualTo( LocalisationCode.Japanese ) );
	}


	[Test]
	public void EmptyPageInt()
	{
		PageParser parser = new PageParser();
		var page = parser.ParsePage( EmptyPageIntFile );

		Assert.That( page.Title, Is.EqualTo( "NO NAME DEFINED" ) );
		Assert.That( page.Description, Is.EqualTo( "NO DESCRIPTION DEFINED" ) );
		Assert.That( page.Version, Is.EqualTo( 0 ) );
		Assert.That( page.LocalisationCode, Is.EqualTo( LocalisationCode.International ) );
	}
}
