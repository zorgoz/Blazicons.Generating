using Blazicons.Generating.Internals;

namespace Blazicons.Generating.UnitTests.AttributesCollectionTests;

[TestClass]
public class FindOrAddShould
{
    [TestMethod]
    public void AddGivenNewAttributeSet()
    {
        var collection = new AttributesCollection();
        var attributes = new Dictionary<string, string>
        {
            { "fill", "#000" },
            { "viewBox", "0 0 24 24" }
        };

        var index = collection.FindOrAdd(attributes);
        Assert.AreEqual(0, index);
    }

    [TestMethod]
    public void ReturnExistingGivenDifferentOrderAttributeSet()
    {
        var collection = new AttributesCollection();
        var attributes = new Dictionary<string, string>
        {
            { "fill", "#000" },
            { "viewBox", "0 0 24 24" }
        };

        collection.FindOrAdd(attributes);
        var differentOrderAttributes = new Dictionary<string, string>
        {
            { "viewBox", "0 0 24 24" },
            { "fill", "#000" }
        };

        var index = collection.FindOrAdd(differentOrderAttributes);
        Assert.AreEqual(0, index);
    }

    [TestMethod]
    public void ReturnExistingIndexGivenExistingAttributeSet()
    {
        var collection = new AttributesCollection();
        var attributes = new Dictionary<string, string>
        {
            { "fill", "#000" },
            { "viewBox", "0 0 24 24" }
        };

        collection.FindOrAdd(attributes);
        var index = collection.FindOrAdd(attributes);
        Assert.AreEqual(0, index);
    }

    [TestMethod]
    public void ReturnNewIndexGivenDifferentAttributeSet()
    {
        var collection = new AttributesCollection();
        var attributes = new Dictionary<string, string>
        {
            { "fill", "#000" },
            { "viewBox", "0 0 24 24" }
        };

        collection.FindOrAdd(attributes);
        var differentAttributes = new Dictionary<string, string>
        {
            { "fill", "#FFF" },
            { "viewBox", "0 0 24 24" }
        };

        var index = collection.FindOrAdd(differentAttributes);
        Assert.AreEqual(1, index);
    }
}