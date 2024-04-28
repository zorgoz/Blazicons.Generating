using Blazicons.Generating.Internals;

namespace Blazicons.Generating.UnitTests.SvgDocumentTests;

[TestClass]
public class HasMultipleFillColorsShould
{
    [TestMethod]
    public void ReturnFalseGivenDifferentStrokeAndFillColors()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path stroke='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/><path fill='#FFFFFF' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        var result = doc.HasMultipleFillColors();

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ReturnFalseGivenMatchingFillAndStrokeColors()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='#FFFFFF' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/><path stroke='#FFFFFF' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        var result = doc.HasMultipleFillColors();

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ReturnFalseGivenMultipleMatchingFillColors()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='#FFFFFF' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/><path fill='#FFFFFF' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        var result = doc.HasMultipleFillColors();

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ReturnFalseGivenNoFillColor()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        var result = doc.HasMultipleFillColors();

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ReturnFalseGivenSingleFillColor()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        var result = doc.HasMultipleFillColors();

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ReturnFalseGivenSingleStrokeColor()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path stroke='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        var result = doc.HasMultipleFillColors();

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ReturnTrueGivenMultipleDifferentFillColors()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/><path fill='#FFFFFF' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        var result = doc.HasMultipleFillColors();

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ReturnTrueGivenMultipleDifferentStrokeColors()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path stroke='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/><path stroke='#FFFFFF' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);

        var result = doc.HasMultipleFillColors();

        Assert.IsTrue(result);
    }
}