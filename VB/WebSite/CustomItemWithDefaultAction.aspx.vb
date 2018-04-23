Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Web.ASPxMenu
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler


Partial Public Class CustomItemWithDefaultAction
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		DataHelper.SetupMappings(ASPxScheduler1)
		DataHelper.ProvideRowInsertion(ASPxScheduler1, DataSource1.AppointmentDataSource)
		DataSource1.AttachTo(ASPxScheduler1)
	End Sub
	#Region "#PopupMenuShowing_CustomItem"
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
	#End Region ' #PopupMenuShowing_CustomItem
	#Region "#MenuHelper"
	Public Class MenuHelper
		Public Shared Sub RemoveMenuItem(ByVal menu As ASPxSchedulerPopupMenu, ByVal menuItemName As String)
			Dim item As MenuItem = menu.Items.FindByName(menuItemName)
			If item IsNot Nothing Then
				menu.Items.Remove(item)
			End If
		End Sub
		Public Shared Sub AddMenuItem(ByVal menu As ASPxSchedulerPopupMenu, ByVal index As Integer, ByVal caption As String, ByVal menuItemName As String)
			Dim items As MenuItemCollection = menu.Items
			Dim item As New MenuItem(caption, menuItemName)
			items.Insert(index, item)
		End Sub
	End Class
	#End Region ' #MenuHelper
End Class
