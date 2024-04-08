namespace Blazicons.Generating.Internals;

internal class AttributesCollection : List<Dictionary<string, string>>
{
    /// <summary>
    /// Gets the matching unique attributes or adds a new set of attributes.
    /// </summary>
    /// <param name="attributes">The attributes to match.</param>
    /// <returns>The index of the matching attributes.</returns>
    public int FindOrAdd(Dictionary<string, string> attributes)
    {
        var match = Find(x => x.Count == attributes.Count && x.Keys.All(k => attributes.ContainsKey(k) && attributes[k] == x[k]));
        if (match is null)
        {
            Add(attributes);
            match = attributes;
        }

        return IndexOf(match);
    }
}