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
Imports DevExpress.Web.ASPxScheduler.Internal
Imports System.Data.OleDb

Partial Public Class DefaultItemWithCustomAction
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		DataHelper.SetupMappings(ASPxScheduler1)
		DataHelper.ProvideRowInsertion(ASPxScheduler1, DataSource1.AppointmentDataSource)
		DataSource1.AttachTo(ASPxScheduler1)
	End Sub
	Protected Sub ASPxScheduler1_PreparePopupMenu(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.PreparePopupMenuEventArgs)
		Dim menu As ASPxSchedulerPopupMenu = e.Menu
		If menu.Id.Equals(SchedulerMenuItemId.DefaultMenu) Then
			menu.ClientSideEvents.ItemClick = String.Format("function(s, e) {{ DefaultViewMenuHandler({0}, s, e); }}", ASPxScheduler1.ClientInstanceName)
			ChangeMenuItemName(menu, "NewAppointment", "MyNewAppointment")
		End If
	End Sub
	Private Sub ChangeMenuItemName(ByVal menu As ASPxSchedulerPopupMenu, ByVal oldName As String, ByVal newName As String)
		Dim menuItems As MenuItemCollection = menu.Items
		Dim defaultItem As MenuItem = menuItems.FindByName("NewAppointment")
		defaultItem.Name = "MyNewAppointment"
	End Sub
	Protected Sub ASPxScheduler1_BeforeExecuteCallbackCommand(ByVal sender As Object, ByVal e As SchedulerCallbackCommandEventArgs)
		If e.CommandId = "MNUVIEW" Then
			e.Command = New CustomMenuViewCallbackCommand(ASPxScheduler1)
		End If
	End Sub
End Class
#Region "CustomMenuViewCallbackCommand"
Public Class CustomMenuViewCallbackCommand
	Inherits MenuViewCallbackCommand
	Private menuItemName As String

	Public Sub New(ByVal control As ASPxScheduler)
		MyBase.New(control)
	End Sub

	Public ReadOnly Property MenuItemId() As String
		Get
			Return menuItemName
		End Get
	End Property

	Protected Overrides Sub ParseParameters(ByVal parameters As String)
		Me.menuItemName = parameters
		MyBase.ParseParameters(parameters)
	End Sub
	Protected Overrides Sub ExecuteCore()
		ExecuteUserMenuCommand(MenuItemId)
		MyBase.ExecuteCore()
	End Sub
	Protected Friend Overridable Sub ExecuteUserMenuCommand(ByVal MenuItemName As String)
		If MenuItemName = "MyNewAppointment" Then
			CreateAppointment("New Appointment", AppointmentStatusType.Free, 1)
		End If
	End Sub
	Protected Sub CreateAppointment(ByVal subject As String, ByVal statusType As AppointmentStatusType, ByVal labelId As Integer)
		Dim apt As Appointment = Control.Storage.CreateAppointment(AppointmentType.Normal)
		apt.Subject = subject
		apt.Start = Control.SelectedInterval.Start
		apt.End = Control.SelectedInterval.End
		apt.ResourceId = Control.SelectedResource.Id
		apt.StatusId = CInt(Fix(statusType))
		apt.LabelId = labelId
		Control.Storage.Appointments.Add(apt)
	End Sub
End Class
#End Region
#Region "MenuHelper"
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
#End Region