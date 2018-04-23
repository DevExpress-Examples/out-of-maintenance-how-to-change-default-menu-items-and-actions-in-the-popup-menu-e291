
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxMenu;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using System.Data.OleDb;

public partial class DefaultItemWithCustomAction : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		DataHelper.SetupMappings(ASPxScheduler1);
		DataHelper.ProvideRowInsertion(ASPxScheduler1, DataSource1.AppointmentDataSource);
		DataSource1.AttachTo(ASPxScheduler1);
	}
	void ChangeMenuItemName(ASPxSchedulerPopupMenu menu, string oldName, string newName) {
		MenuItemCollection menuItems = menu.Items;
		MenuItem defaultItem = menuItems.FindByName("NewAppointment");
		defaultItem.Name = "MyNewAppointment";
	}
	protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, SchedulerCallbackCommandEventArgs e) {
		if(e.CommandId == "MNUVIEW")
			e.Command = new CustomMenuViewCallbackCommand(ASPxScheduler1);
	}
    protected void ASPxScheduler1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
    {
        ASPxSchedulerPopupMenu menu = e.Menu;
        if (menu.Id.Equals(SchedulerMenuItemId.DefaultMenu)) {
            menu.ClientSideEvents.ItemClick = String.Format("function(s, e) {{ DefaultViewMenuHandler({0}, s, e); }}", ASPxScheduler1.ClientInstanceName);
            ChangeMenuItemName(menu, "NewAppointment", "MyNewAppointment");
        }
    }
}
#region CustomMenuViewCallbackCommand
public class CustomMenuViewCallbackCommand : MenuViewCallbackCommand {
	string menuItemName;

	public CustomMenuViewCallbackCommand(ASPxScheduler control)
		: base(control) {
	}

	public string MenuItemId { get { return menuItemName; } }

	protected override void ParseParameters(string parameters) {
		this.menuItemName = parameters;
		base.ParseParameters(parameters);
	}
	protected override void ExecuteCore() {
		ExecuteUserMenuCommand(MenuItemId);
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
		apt.StatusId = (int)statusType;
		apt.LabelId = labelId;
		Control.Storage.Appointments.Add(apt);
	}
}
#endregion
#region MenuHelper
public class MenuHelper {
	public static void RemoveMenuItem(ASPxSchedulerPopupMenu menu, string menuItemName) {
		MenuItem item = menu.Items.FindByName(menuItemName);
		if(item != null)
			menu.Items.Remove(item);
	}
	public static void AddMenuItem(ASPxSchedulerPopupMenu menu, int index, string caption, string menuItemName) {
		MenuItemCollection items = menu.Items;
		MenuItem item = new MenuItem(caption, menuItemName);
		items.Insert(index, item);
	}
}
#endregion