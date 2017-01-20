using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PowerBI_TagHelper.TagHelpers
{
    [HtmlTargetElement("power-bi", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PowerBITagHelper : TagHelper
    {
        public int Height { get; set; } = 600;

        [HtmlAttributeName("reportId")]
        public string ReportId { get; set; }

        public int Width { get; set; } = 800;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            const string PowerBIAppUrl = "https://app.powerbi.com/";
            var embeddUrl = $"{PowerBIAppUrl}view?r={ReportId}";
            output.Content.SetHtmlContent($@"<iframe width=""{Width}"" height=""{Height}"" src=""{embeddUrl}"" frameborder=""0"" allowFullScreen=""true""></iframe>");
        }
    }
}
