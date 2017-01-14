using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PowerBI_TagHelper.TagHelpers
{
    [HtmlTargetElement("power-bi", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PowerBITagHelper : TagHelper
    {
        private static readonly string _powerBIAppUrl = "https://app.powerbi.com/";

        public int Height { get; set; } = 600;

        public int Width { get; set; } = 800;

        [HtmlAttributeName("reportCode")]
        public string ReportCode { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var src = $"{_powerBIAppUrl}view?r={ReportCode}";
            output.TagName = null;
            output.Content.SetHtmlContent($@"<iframe width=""{Width}"" height=""{Height}"" src=""{src}"" frameborder=""0"" allowFullScreen=""true""></iframe>");
        }
    }
}
