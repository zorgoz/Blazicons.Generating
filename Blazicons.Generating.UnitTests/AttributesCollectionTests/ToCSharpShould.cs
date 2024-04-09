using Blazicons.Generating.Internals;

namespace Blazicons.Generating.UnitTests.AttributesCollectionTests;

[TestClass]
public class ToCSharpShould: VerifyBase
{
    [TestMethod]
    public void ReturnEmptyGivenEmptyCollection()
    {
        var collection = new AttributesCollection();
        var output = collection.ToCSharp();
        Assert.AreEqual(string.Empty, output);
    }

    [TestMethod]
    public Task ReturnCodeGivenOneEntry()
    {
        var collection = new AttributesCollection();
        collection.FindOrAdd(new Dictionary<string, string>
        {
            { "fill", "#000" },
            { "viewBox", "0 0 24 24" }
        });

        var output = collection.ToCSharp();
        return Verify(output);
    }

    [TestMethod]
    public Task ReturnCodeGivenMultipleEntries()
    {
        var collection = new AttributesCollection();
        collection.FindOrAdd(new Dictionary<string, string>
        {
            { "fill", "#000" },
            { "viewBox", "0 0 24 24" }
        });

        collection.FindOrAdd(new Dictionary<string, string>
        {
            { "fill", "#FFF" },
            { "viewBox", "0 0 24 24" }
        });

        var output = collection.ToCSharp();
        return Verify(output);
    }
}