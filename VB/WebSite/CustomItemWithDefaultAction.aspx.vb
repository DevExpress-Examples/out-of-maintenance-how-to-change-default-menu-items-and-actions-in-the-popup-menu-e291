Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxMenu
Imports DevExpress.XtraScheduler

Partial Public Class CustomItemWithDefaultAction
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		DataHelper.SetupMappings(ASPxScheduler1)
		DataHelper.ProvideRowInsertion(ASPxScheduler1, DataSource1.AppointmentDataSource)
		DataSource1.AttachTo(ASPxScheduler1)
	End Sub

	Protected Sub ASPxScheduler1_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
		Dim menu As ASPxSchedulerPopupMenu = e.Menu
		If menu.Id.Equals(SchedulerMenuItemId.AppointmentMenu) Then
			Dim menuItems As MenuItemCollection = menu.Items
			Dim defaultItemOpentAppointment As MenuItem = menuItems.FindByText("Open")
			MenuHelper.RemoveMenuItem(menu, "OpenAppointment")
			MenuHelper.AddMenuItem(menu, 0, "MyOpenAppoinment", defaultItemOpentAppointment.Name)
			Dim defaultItemLabelAsImportant As MenuItem = menuItems.FindByText("Important")
			MenuHelper.AddMenuItem(menu, 1, "Make Important", defaultItemLabelAsImportant.Name)
		End If
	End Sub
End Class
