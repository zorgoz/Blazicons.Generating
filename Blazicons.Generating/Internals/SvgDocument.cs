using HtmlAgilityPack;

namespace Blazicons.Generating.Internals;

internal class SvgDocument
{
    public SvgDocument(string svg)
    {
        Svg = svg;
        Document = new HtmlDocument();
        Document.LoadHtml(Svg);

        SvgNode = Document.DocumentNode.SelectSingleNode("//svg")
            ?? throw new InvalidOperationException("The provided SVG does not contain a root <svg> element.");
    }

    public string Svg { get; }

    public HtmlDocument Document { get; }

    public HtmlNode SvgNode { get; }

    public Dictionary<string, string> GetAttributes()
    {
        return SvgNode.Attributes.ToDictionary(x => x.Name, x => x.Value);
    }

    /// <summary>
    /// Determines if the SVG and its children contain multiple fill colors.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public bool HasMultipleFillColors()
    {
        var type = CalculateSvgColorType();
        return type == SvgColorType.Multiple;
    }

    public SvgColorType CalculateSvgColorType()
    {
        var nodes = SvgNode.DescendantsAndSelf().ToList();
        var fillColors = nodes.Where(x => x.Attributes.Contains("fill")).Select(x => x.Attributes["fill"].Value).Distinct().ToList();
        fillColors.Remove("none");
        var strokeColors = nodes.Where(x => x.Attributes.Contains("stroke")).Select(x => x.Attributes["stroke"].Value).Distinct().ToList();
        strokeColors.Remove("none");
        var allColors = fillColors.Concat(strokeColors).Distinct().ToList();
        if (allColors.Count > 1)
        {
            return SvgColorType.Multiple;
        }

        if (fillColors.Count == 1 && strokeColors.Count == 1)
        {
            return SvgColorType.SingleStrokeAndFill;
        }

        if (fillColors.Count == 1)
        {
            return SvgColorType.SingleFill;
        }

        if (strokeColors.Count == 1)
        {
            return SvgColorType.SingleStroke;
        }

        return SvgColorType.None;
    }

    public void RemoveColorAttributes()
    {
        var nodes = SvgNode.Descendants().ToList();

        foreach (var node in nodes)
        {
            node.Attributes.Remove("fill");
            node.Attributes.Remove("stroke");
        }
    }

    public void RemoveComments()
    {
        var comments = SvgNode.Descendants().Where(x => x.NodeType == HtmlNodeType.Comment).ToList();
        foreach (var comment in comments)
        {
            comment.Remove();
        }
    }

    public void Scrub()
    {
        RemoveComments();
        CalculateSvgColorType();
        UpdateRootColor();
    }

    public void UpdateRootColor()
    {
        var needsColorClean = false;
        var colorType = CalculateSvgColorType();
        if (colorType == SvgColorType.SingleStroke || colorType == SvgColorType.SingleStrokeAndFill)
        {
            if (SvgNode.Attributes.Contains("stroke"))
            {
                SvgNode.Attributes["blazicon-stroke"].Value = "currentColor";
            }
            else
            {
                SvgNode.Attributes.Add("blazicon-stroke", "currentColor");
            }

            needsColorClean = true;
        }

        if (colorType == SvgColorType.SingleFill || colorType == SvgColorType.SingleStrokeAndFill || colorType == SvgColorType.None)
        {
            if (SvgNode.Attributes.Contains("fill"))
            {
                SvgNode.Attributes["blazicon-fill"].Value = "";
            }
            else
            {
                SvgNode.Attributes.Add("blazicon-fill", "");
            }

            needsColorClean = true;
        }

        if (needsColorClean)
        {
            RemoveColorAttributes();
        }
    }
}