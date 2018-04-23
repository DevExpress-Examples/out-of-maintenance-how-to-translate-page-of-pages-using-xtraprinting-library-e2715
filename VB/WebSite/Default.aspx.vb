Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxGridView.Export.Helper
Imports DevExpress.XtraPrinting
Imports System.IO
Imports System.Drawing
Imports DevExpress.XtraPrintingLinks

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub btn_Click(ByVal sender As Object, ByVal e As EventArgs)
		 Dim link As New GridViewLink(exporter)

		 AddHandler link.CreateMarginalHeaderArea, AddressOf link_CreateMarginalHeaderArea

		 link.CreatePS()

		 link.CreateDocument()

		 Using stream As New MemoryStream()
			 link.PrintingSystem.ExportToPdf(stream)
			 Response.Clear()
			 Response.Buffer = False
			 Response.AppendHeader("Content-Type", "application/pdf")
			 Response.AppendHeader("Content-Transfer-Encoding", "binary")
			 Response.AppendHeader("Content-Disposition", "attachment; filename=grid.pdf")
			 Response.BinaryWrite(stream.GetBuffer())
			 Response.End()
		 End Using
	End Sub

	Private Sub link_CreateMarginalHeaderArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
		Dim format As String = "Page(eng) {0} of(eng) {1}"
		e.Graph.Font = e.Graph.DefaultFont
		e.Graph.BackColor = Color.Transparent

		Dim r As New RectangleF(0, 0, 0, e.Graph.Font.Height)

		Dim brick As PageInfoBrick = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, format, Color.Black, r, BorderSide.None)
		brick.Alignment = BrickAlignment.Far
		brick.AutoWidth = True
	End Sub
End Class
