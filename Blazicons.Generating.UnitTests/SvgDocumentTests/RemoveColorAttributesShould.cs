using Blazicons.Generating.Internals;

namespace Blazicons.Generating.UnitTests.SvgDocumentTests;

[TestClass]
public class RemoveColorAttributesShould : VerifyBase
{
    [TestMethod]
    public Task DoNothingGivenNoColorAttributes()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'></path></svg>";
        var doc = new SvgDocument(svg);

        doc.RemoveColorAttributes();
        return Verify(doc.Document.DocumentNode.OuterHtml);
    }

    [TestMethod]
    public void RemoveAllGivenFill()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        doc.RemoveColorAttributes();
        var output = doc.Document.DocumentNode.OuterHtml;
        Assert.IsFalse(output.Contains("fill"));
    }

    [TestMethod]
    public void RemoveAllGivenMultipleFills()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/><path fill='#FFFFFF' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        doc.RemoveColorAttributes();
        var output = doc.Document.DocumentNode.OuterHtml;
        Assert.IsFalse(output.Contains("fill"));
    }

    [TestMethod]
    public void RemoveAllGivenStroke()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path stroke='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        doc.RemoveColorAttributes();
        var output = doc.Document.DocumentNode.OuterHtml;
        Assert.IsFalse(output.Contains("stroke"));
    }

    [TestMethod]
    public void RemoveAllGivenMultipleStrokes()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path stroke='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/><path stroke='#FFFFFF' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        doc.RemoveColorAttributes();
        var output = doc.Document.DocumentNode.OuterHtml;
        Assert.IsFalse(output.Contains("stroke"));
    }

    [TestMethod]
    public void RemoveAllGivenFillAndStroke()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/><path stroke='#FFFFFF' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        doc.RemoveColorAttributes();
        var output = doc.Document.DocumentNode.OuterHtml;
        Assert.IsFalse(output.Contains("fill"));
        Assert.IsFalse(output.Contains("stroke"));
    }
}