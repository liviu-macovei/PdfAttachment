using System;
using System.IO;
using System.Text;

namespace PdfAttachment.Helpers
{
    public enum HtmlVersionType
    {
        AdminVersion,
        CouncilMemberVersion
    }

    public class HtmlGenerator
    {
        private ApprovalUnit approvalUnit;

        private string HtmlContent = string.Empty;
        private string HTMLPageHeader;
        private string langCode = "dk";
        private ApprovalUnit parentApprovalUnit;
        private HtmlVersionType pdfVersion;

        public HtmlGenerator(ApprovalUnit approvalUnit, HtmlVersionType pdfVersion)
        {
            this.approvalUnit = approvalUnit;
            this.pdfVersion = pdfVersion;
        }

        public HtmlGenerator(ApprovalUnit approvalUnit, Guid? councilMeetingId, HtmlVersionType pdfVersion)
        {
            this.approvalUnit = approvalUnit;
            this.pdfVersion = pdfVersion;
        }

        public string GetHtml()
        {
            GenerateHtml();
            return HtmlContent;
        }

        private void GenerateHtml()
        {
            var sb = new StringBuilder("<html>" + AddHeader(AddCssStyles()) + "<body>");

            sb.Append(@"
                            <h1>Enter the main heading, usually the same as the title.</h1>
                            <p> Be <b> bold </ b> in stating your key points.Put them in a list: </ p>
                            <ul>
                                <li> The first item in your list </ li>
                                <li> The second item; <i> italicize </ i> key words </ li>
                            </ ul>
                            <p> Improve your image by including an image. </ p>
                            <p><img src = 'http://www.mygifs.com/CoverImage.gif' alt = 'A Great HTML Resource'></ p>
                            <p> Add a link to your favorite <a href = 'http://www.dummies.com/'> Web site </ a>.
                            Break up your page with a horizontal rule or two. </ p>
                            <hr>
                            <p> Finally, link to <a href = 'page2.html'> another page </ a> in your own Web site.</ p>
                            <!--And add a copyright notice.-->
                            <p> &#169; Wiley Publishing, 2011</p>
                        </body></html>");
            HtmlContent = sb.ToString();

            if (!Directory.Exists("C:/temp/"))
            {
                Directory.CreateDirectory("C:/temp/");
            }
            File.WriteAllText("C:/temp/tmp.html", HtmlContent);
        }

        private string AddHeader(string styles = "")
        {
            var header = new StringBuilder("<head>");
            header.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            if (!string.IsNullOrWhiteSpace(styles))
            {
                header.Append(styles);
            }
            header.Append("</head>");

            return header.ToString();
        }

        private string AddCssStyles()
        {
            var sb = new StringBuilder("<style>");
            //sb.Append(@"table.list{width:100%; border-collapse:collapse; border-bottom:1px solid #f6f6f6; margin-top:10px; margin-bottom:10px;}");
            //sb.Append(@"table.list a, table.list a:link, table.list a:active, table.list a:visited{text-decoration:underline;}");
            //sb.Append(@"table.list a:hover{color:#525252; text-decoration:none;}");
            //sb.Append(@"table.SmallList td, table.SmallList th{padding-left:2px;padding-right:10px; vertical-align:top; padding-top:2px; padding-bottom:2px;}");			
            //sb.Append(@"table.SmallList tr.listRow2, table.list tr.listTop{background-color:#f6f6f6;}");
            //sb.Append(@"table.SmallList tr.listRow1{background-color:white;}");
            //sb.Append(@"table.SmallList th{ text-align:left;  font-weight:bold;}");
            //sb.Append(@"table.list td, table.list th{padding: 10px;padding-left:5px;padding-right:10px; vertical-align:top; padding-top:12px; padding-bottom:12px;}");
            //sb.Append(@"table.list th{ text-align:left; font-weight:bold;}");
            //sb.Append(@"table.list th a{color:#525252}");
            //sb.Append(@"table.list tr{border-bottom:1px solid #e3e3e3;}");
            //sb.Append(@"table.list tr.listRow2, table.list tr.listTop{background-color:#f6f6f6;}");
            //sb.Append(@"table.list tr.listRow1{background-color:red;}");
            //sb.Append(@"table.list tr td.ColumnDate{width:4%;}");
            //sb.Append(@"table.list tr td.ColumnSep{width:1%;}");
            //sb.Append(@"table.list tr td.listCellWidth{width:95%;}");
            //sb.Append(@".veterinary table.list a, .veterinary table.list a:link, .veterinary table.list a:active, .veterinary table.list a:visited{text-decoration:underline;}");
            //sb.Append(@".veterinary table.list a:hover{color:#525252; text-decoration:none;}");
            //sb.Append(@".veterinary table.list td, .veterinary table.list th{padding-left:5px;padding-right:5px; vertical-align:top; padding-top:4px; padding-bottom:3px;}");
            //sb.Append(@"table tr.listRow2.selected, table tr.listRow1.selected, table tr.listRow.selected { background-color:#0000aa; color: white !important;	}");
            //sb.Append(@"table tr.listRow2.selected a, table tr.listRow1.selected a, table tr.listRow.selected a { color: white !important;	}");
            //sb.Append(@".listCell{ padding-right: 4px;}");
            //sb.Append(@".verticaltext{ white-space: nowrap;}");
            //sb.Append(@"table.update tr, table.Show tr, table.Create tr, table.list tr, table.list tr {    padding-top:3px;    padding-bottom:3px;}");
            //sb.Append(@"table.update td.Lable, table.Create td.Lable, table.update td.Value, table.Create td.Value {vertical-align:top;padding-right:5px;padding:1px}");
            //sb.Append(@".button { padding: 2px; margin: 2px;}");
            //sb.Append(@".list .CreateRow .Value table{margin-top:12px;}");
            //sb.Append(@".list .CreateRow .Value input{margin-left:12px;}");
            //sb.Append(@".Lable{font-weight:bold;width: 200px;}");
            sb.Append(@"h1 {font-family: 'Stag Sans', Arial, Georgia; color: #0A2614; font-size: 14pt;}");
            sb.Append(@"h2 {font-family: 'Stag Sans', Arial, Georgia; color: #0A2614; font-size: 12pt;}");
            sb.Append(
                @"table.detailsList {border-collapse:collapse; font-family: 'Stag Sans', Arial, Georgia; font-size: 12pt;}");
            sb.Append(
                @".detailsList .Lable {vertical-align: top; background-color: #EEF3E2; padding: 10px; border-collapse:collapse; font-weight: bold; font-family: 'Stag Sans', Arial, Georgia; font-size: 12pt}");
            sb.Append(
                @".Header {background-color: #DCE7C5; text-align: center; font-weight: bold; padding: 10px; font-family: 'Stag Sans', Arial, Georgia; font-size: 14pt; }");
            sb.Append(
                @".Value {padding:10px; width: 70%; vertical-align: top; font-family: 'Stag Sans', Arial, Georgia; font-size: 12pt;}");
            sb.Append(
                @".detailsList{width: 100%;border-collapse:collapse; font-family: 'Stag Sans', Arial, Georgia; font-size: 12pt;}");
            sb.Append(@".detailsList > tbody> tr > td {border: 1px solid black; }");
            sb.Append(@".detailsList > tr > td {border: 1px solid black; }");
            //sb.Append(@"input[disabled=disabled], input:disabled {opacity: 0.65; cursor: not-allowed;}");
            sb.Append("</style>");
            return sb.ToString();
        }
    }
}