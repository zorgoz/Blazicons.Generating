using Blazicons.Generating.Internals;

namespace Blazicons.Generating.UnitTests.SvgDocumentTests;

[TestClass]
public class RemoveCommentsShould : VerifyBase
{
    [TestMethod]
    public Task RemoveGivenComment()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><!-- This is a comment--><path fill='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);
        doc.RemoveComments();
        var output = doc.Document.DocumentNode.OuterHtml;
        return Verify(output);
    }

    [TestMethod]
    public Task RemoveAllGivenMultipleComments()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><!-- This is a comment--><!-- This is another comment--><path fill='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'/></svg>";
        var doc = new SvgDocument(svg);
        doc.RemoveComments();
        var output = doc.Document.DocumentNode.OuterHtml;
        return Verify(output);
    }

    [TestMethod]
    public Task DoNothingGivenNoComments()
    {
        var svg = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'><path fill='#000000' d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z'></path></svg>";
        var doc = new SvgDocument(svg);
        doc.RemoveComments();
        var output = doc.Document.DocumentNode.OuterHtml;
        return Verify(output);
    }
}