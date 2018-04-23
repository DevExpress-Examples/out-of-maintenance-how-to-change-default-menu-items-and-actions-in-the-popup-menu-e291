using System;
using System.Web.UI;
#region #usings
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
#endregion #usings

public partial class DefaultItemWithCustomAction : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		DataHelper.SetupMappings(ASPxScheduler1);
		DataHelper.ProvideRowInsertion(ASPxScheduler1, DataSource1.AppointmentDataSource);
		DataSource1.AttachTo(ASPxScheduler1);
	}
    #region #BeforeExecuteCallbackCommand_DefaultItem
    protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, SchedulerCallbackCommandEventArgs e) {
		if(e.CommandId == "MNUVIEW")
			e.Command = new CustomMenuViewCallbackCommand(ASPxScheduler1);
	}
    #endregion #BeforeExecuteCallbackCommand_DefaultItem
    #region #PopupMenuShowing_DefaultItem
    protected void ASPxScheduler1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
    {
        ASPxSchedulerPopupMenu menu = e.Menu;
        if (menu.MenuId.Equals(SchedulerMenuItemId.DefaultMenu)) {
            menu.ClientSideEvents.ItemClick = String.Format("function(s, e) {{ DefaultViewMenuHandler({0}, s, e); }}", ASPxScheduler1.ClientInstanceName);
            MenuItemCollection menuItems = menu.Items;
            MenuItem defaultItem = menuItems.FindByName("NewAppointment");
            defaultItem.Name = "MyNewAppointment";
            defaultItem.Text = "Instant Appointment";
        }
    }
    #endregion #PopupMenuShowing_DefaultItem
}
#region #CustomMenuViewCallbackCommand_DefaultItem
public class CustomMenuViewCallbackCommand : DevExpress.Web.ASPxScheduler.Internal.MenuViewCallbackCommand {
	string menuItemName;

	public CustomMenuViewCallbackCommand(ASPxScheduler control)
		: base(control) {
	}

	public string MenuItemName { get { return menuItemName; } }

	protected override void ParseParameters(string parameters) {
		this.menuItemName = parameters;
		base.ParseParameters(parameters);
	}
	protected override void ExecuteCore() {
		ExecuteUserMenuCommand(MenuItemName);
		base.ExecuteCore();
	}
	protected internal virtual void ExecuteUserMenuCommand(string MenuItemName) {
		if(MenuItemName == "MyNewAppointment")
			CreateAppointment("New Appointment", AppointmentStatusType.Free, 1);
	}
	protected void CreateAppointment(string subject, AppointmentStatusType statusType, int labelId) {
		Appointment apt = Control.Storage.CreateAppointment(AppointmentType.Normal);
		apt.Subject = subject;
		apt.Start = Control.SelectedInterval.Start;
		apt.End = Control.SelectedInterval.End;
		apt.ResourceId = Control.SelectedResource.Id;
		apt.StatusKey = (int)statusType;
		apt.LabelKey = labelId;
		Control.Storage.Appointments.Add(apt);
	}
}
#endregion #CustomMenuViewCallbackCommand_DefaultItem