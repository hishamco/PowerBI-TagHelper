using Microsoft.AspNetCore.Razor.TagHelpers;
using PowerBI_TagHelper.Extensions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PowerBI_TagHelper.TagHelpers
{
    [HtmlTargetElement("power-bi", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PowerBITagHelper : TagHelper
    {
        [HtmlAttributeName("accessKey")]
        public string AccessKey { get; set; }

        public int Height { get; set; } = 600;

        [HtmlAttributeName("reportId")]
        public string ReportId { get; set; }

        public int Width { get; set; } = 800;

        [HtmlAttributeName("workspaceCollectionName")]
        public string WorkspaceCollectionName { get; set; }

        [HtmlAttributeName("workspaceId")]
        public string WorkspaceId { get; set; }

        private string AccessToken
        {
            get
            {
                // For more information refer to this link https://docs.microsoft.com/en-us/azure/power-bi-embedded/power-bi-embedded-iframe
                var token1 = "{" +
              "\"typ\":\"JWT\"," +
              "\"alg\":\"HS256\"" +
              "}";
                var token2 = "{" +
              $"\"wid\":\"{WorkspaceId}\"," +
              $"\"rid\":\"{ReportId}\"," +
              $"\"wcn\":\"{WorkspaceCollectionName}\"," +
              "\"iss\":\"PowerBISDK\"," +
              "\"ver\":\"0.2.0\"," +
              "\"aud\":\"https://analysis.windows.net/powerbi/api\"," +
              "\"nbf\":" + DateTime.UtcNow.ToUnixTimestamp() + "," +
              "\"exp\":" + DateTime.UtcNow.AddHours(1).ToUnixTimestamp() +
              "}";

                var inputValue = $"{UrlEncode(token1)}.{UrlEncode(token2)}";
                var hash = new HMACSHA256(Encoding.UTF8.GetBytes(AccessKey))
                    .ComputeHash(Encoding.UTF8.GetBytes(inputValue));
                var signature = UrlEncode(hash);

                return $"{inputValue}.{signature}";
            }
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            if (WorkspaceCollectionName != null && WorkspaceId != null && AccessKey != null & ReportId != null)
            {
                var content = $@"<div id=""reportContainer"" style=""width: {Width}px; height: {Height}px""></div>

   <script>
    $(function() {{
        var embedConfiguration = {{
            type: 'report',
            accessToken: '{AccessToken}',
            id: '{ReportId}',
            embedUrl: 'https://embedded.powerbi.com/appTokenReportEmbed?reportId={ReportId}'
        }};
            var $reportContainer = $('#reportContainer');
            var report = powerbi.embed($reportContainer.get(0), embedConfiguration);
    }})
</script>";
                output.Content.SetHtmlContent(content);
            }
            else if (ReportId != null)
            {
                const string PowerBIAppUrl = "https://app.powerbi.com/";
                var embeddUrl = $"{PowerBIAppUrl}view?r={ReportId}";
                output.Content.SetHtmlContent($@"<iframe width=""{Width}"" height=""{Height}"" src=""{embeddUrl}"" frameborder=""0"" allowFullScreen=""true""></iframe>");
            }       
        }

        private string UrlEncode(string plainText)
        {
            // Implementation for base64 url according to RFC 4648
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var result = Convert.ToBase64String(plainTextBytes);
            result = result.Replace("/", "_");
            result = result.Replace("+", "-");
            result = result.TrimEnd('=');

            return result;
        }

        private string UrlEncode(byte[] plainTextBytes)
        {
            // Implementation for base64 url according to RFC 4648
            var result = Convert.ToBase64String(plainTextBytes);
            result = result.Replace("/", "_");
            result = result.Replace("+", "-");
            result = result.TrimEnd('=');

            return result;
        }
    }
}
