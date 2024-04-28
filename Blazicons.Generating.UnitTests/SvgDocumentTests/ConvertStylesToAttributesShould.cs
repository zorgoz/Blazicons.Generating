using Blazicons.Generating.Internals;

namespace Blazicons.Generating.UnitTests.SvgDocumentTests;

[TestClass]
public class ConvertStylesToAttributesShould : VerifyBase
{
    [TestMethod]
    public void ConvertGivenChildStyle()
    {
        var input = "<svg viewBox='0 0 24 24'><path style='fill: red; stroke: blue;' d='M6,5H18V7H6M7,11H17V13H7M5.5,17H18.5V19H5.5'></path></svg>";
        var expected = "<svg viewBox='0 0 24 24'><path d='M6,5H18V7H6M7,11H17V13H7M5.5,17H18.5V19H5.5' fill='red' stroke='blue'></path></svg>";

        var svg = new SvgDocument(input);
        svg.ConvertStylesToAttributes();
        Assert.AreEqual(expected, svg.Document.DocumentNode.OuterHtml);
    }

    [TestMethod]
    public void ConvertGivenRootStyle()
    {
        var input = "<svg viewBox='0 0 24 24' style='fill: red; stroke: blue;'><path d='M6,5H18V7H6M7,11H17V13H7M5.5,17H18.5V19H5.5'></path></svg>";
        var expected = "<svg viewBox='0 0 24 24' fill='red' stroke='blue'><path d='M6,5H18V7H6M7,11H17V13H7M5.5,17H18.5V19H5.5'></path></svg>";

        var svg = new SvgDocument(input);
        svg.ConvertStylesToAttributes();
        Assert.AreEqual(expected, svg.Document.DocumentNode.OuterHtml);
    }

    [TestMethod]
    public void DoNothingGivenNoStyleAttributes()
    {
        var input = "<svg viewBox='0 0 24 24'><path d='M6,5H18V7H6M7,11H17V13H7M5.5,17H18.5V19H5.5'></path></svg>";
        var svg = new SvgDocument(input);
        svg.ConvertStylesToAttributes();
        Assert.AreEqual(input, svg.Document.DocumentNode.OuterHtml);
    }
}