using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView.Export.Helper;
using DevExpress.XtraPrinting;
using System.IO;
using System.Drawing;
using DevExpress.XtraPrintingLinks;

public partial class _Default : System.Web.UI.Page
{
    protected void btn_Click(object sender, EventArgs e)
    {
        CompositeLinkBase link = new CompositeLinkBase(new PrintingSystemBase());
        PrintableComponentLinkBase pcLink1 = new PrintableComponentLinkBase();
        pcLink1.Component = exporter;
        link.Links.Add(pcLink1);

        link.CreateMarginalHeaderArea += new CreateAreaEventHandler(link_CreateMarginalHeaderArea);
        using (MemoryStream stream = new MemoryStream())
        {
            link.ExportToPdf(stream);
            Response.Clear();
            Response.Buffer = false;
            Response.AppendHeader("Content-Type", "application/pdf");
            Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Response.AppendHeader("Content-Disposition", "attachment; filename=grid.pdf");
            Response.BinaryWrite(stream.GetBuffer());
            Response.End();
        }
    }

    void link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
    {
        string format = "Page(eng) {0} of(eng) {1}";
        e.Graph.Font = e.Graph.DefaultFont;
        e.Graph.BackColor = Color.Transparent;

        RectangleF r = new RectangleF(0, 0, 0, e.Graph.Font.Height);

        PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, format,
            Color.Black, r, BorderSide.None);
        brick.Alignment = BrickAlignment.Far;
        brick.AutoWidth = true;
    }
}
