Imports System
Imports System.Web.UI
#Region "#usings"
Imports DevExpress.Web
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler
#End Region ' #usings

Partial Public Class DefaultItemWithCustomAction
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        DataHelper.SetupMappings(ASPxScheduler1)
        DataHelper.ProvideRowInsertion(ASPxScheduler1, DataSource1.AppointmentDataSource)
        DataSource1.AttachTo(ASPxScheduler1)
    End Sub
    #Region "#BeforeExecuteCallbackCommand_DefaultItem"
    Protected Sub ASPxScheduler1_BeforeExecuteCallbackCommand(ByVal sender As Object, ByVal e As SchedulerCallbackCommandEventArgs)
        If e.CommandId = "MNUVIEW" Then
            e.Command = New CustomMenuViewCallbackCommand(ASPxScheduler1)
        End If
    End Sub
    #End Region ' #BeforeExecuteCallbackCommand_DefaultItem
    #Region "#PopupMenuShowing_DefaultItem"
    Protected Sub ASPxScheduler1_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
        Dim menu As ASPxSchedulerPopupMenu = e.Menu
        If menu.MenuId.Equals(SchedulerMenuItemId.DefaultMenu) Then
            menu.ClientSideEvents.ItemClick = String.Format("function(s, e) {{ DefaultViewMenuHandler({0}, s, e); }}", ASPxScheduler1.ClientInstanceName)
            Dim menuItems As MenuItemCollection = menu.Items
            Dim defaultItem As MenuItem = menuItems.FindByName("NewAppointment")
            defaultItem.Name = "MyNewAppointment"
            defaultItem.Text = "Instant Appointment"
        End If
    End Sub
    #End Region ' #PopupMenuShowing_DefaultItem
End Class
#Region "#CustomMenuViewCallbackCommand_DefaultItem"
Public Class CustomMenuViewCallbackCommand
    Inherits DevExpress.Web.ASPxScheduler.Internal.MenuViewCallbackCommand


    Private menuItemName_Renamed As String

    Public Sub New(ByVal control As ASPxScheduler)
        MyBase.New(control)
    End Sub

    Public ReadOnly Property MenuItemName() As String
        Get
            Return menuItemName_Renamed
        End Get
    End Property

    Protected Overrides Sub ParseParameters(ByVal parameters As String)
        Me.menuItemName_Renamed = parameters
        MyBase.ParseParameters(parameters)
    End Sub
    Protected Overrides Sub ExecuteCore()
        ExecuteUserMenuCommand(MenuItemName)
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
        apt.StatusKey = CInt((statusType))
        apt.LabelKey = labelId
        Control.Storage.Appointments.Add(apt)
    End Sub
End Class
#End Region ' #CustomMenuViewCallbackCommand_DefaultItem