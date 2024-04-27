using HtmlAgilityPack;

namespace Blazicons.Generating.Internals;

internal class SvgDocument
{
    public SvgDocument(string svg)
    {
        Svg = svg;
        Document = new HtmlDocument();
        Document.OptionOutputOriginalCase = true;
        Document.GlobalAttributeValueQuote = AttributeValueQuote.SingleQuote;
        Document.LoadHtml(Svg);
        Document.UseAttributeOriginalName("svg");

        SvgNode = Document.DocumentNode.SelectSingleNode("//svg")
            ?? throw new InvalidOperationException("The provided SVG does not contain a root <svg> element.");
    }

    public HtmlDocument Document { get; }

    public string Svg { get; }

    public HtmlNode SvgNode { get; }

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

    public void ConvertStylesToAttributes()
    {
        var nodes = SvgNode.DescendantsAndSelf().ToList();
        foreach (var node in nodes.Where(node => node.Attributes.Contains("style")))
        {
            var styleValue = node.Attributes["style"].Value;
            var parts = styleValue.Split([';'], StringSplitOptions.RemoveEmptyEntries);
            var attributes = new List<HtmlAttribute>();
            foreach (var part in parts)
            {
                var styleEntry = part.Split(':');
                if (styleEntry.Length > 1)
                {
                    attributes.Add(Document.CreateAttribute(styleEntry[0].Trim(), styleEntry[1].Trim()));
                }
            }

            node.Attributes.Remove("style");
            foreach (var attribute in attributes)
            {
                node.Attributes.Add(attribute);
            }
        }
    }

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
        ConvertStylesToAttributes();
        CalculateSvgColorType();
        UpdateColors();
    }

    public void UpdateColors()
    {
        var colorType = CalculateSvgColorType();
        UpdateFillColor(colorType);

        if (colorType == SvgColorType.SingleStroke || colorType == SvgColorType.None)
        {
            if (SvgNode.Attributes.Contains("fill"))
            {
                if (!SvgNode.Attributes["fill"].Value.Equals("none", StringComparison.OrdinalIgnoreCase))
                {
                    SvgNode.Attributes["fill"].Value = "currentColor";
                }
            }
            else
            {
                SvgNode.Attributes.Add("fill", "currentColor");
            }
        }

        UpdateStrokeColor(colorType);
    }

    private void UpdateFillColor(SvgColorType colorType)
    {
        if (colorType == SvgColorType.SingleFill || colorType == SvgColorType.SingleStrokeAndFill)
        {
            var nodes = Document.DocumentNode.DescendantsAndSelf();
            foreach (var node in nodes.Where(node => node.Attributes.Contains("fill") && !node.Attributes["fill"].Value.Equals("none", StringComparison.OrdinalIgnoreCase)))
            {
                node.Attributes["fill"].Value = "currentColor";
            }
        }
    }

    private void UpdateStrokeColor(SvgColorType colorType)
    {
        if (colorType == SvgColorType.SingleStroke || colorType == SvgColorType.SingleStrokeAndFill)
        {
            var nodes = Document.DocumentNode.DescendantsAndSelf();
            foreach (var node in nodes.Where(node => node.Attributes.Contains("stroke") && !node.Attributes["stroke"].Value.Equals("none", StringComparison.OrdinalIgnoreCase)))
            {
                node.Attributes["stroke"].Value = "currentColor";
            }
        }
    }
}