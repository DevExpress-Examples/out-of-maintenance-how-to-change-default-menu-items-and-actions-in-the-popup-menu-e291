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

public partial class CustomItemWithDefaultAction : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		DataHelper.SetupMappings(ASPxScheduler1);
		DataHelper.ProvideRowInsertion(ASPxScheduler1, DataSource1.AppointmentDataSource);
		DataSource1.AttachTo(ASPxScheduler1);
	}

    protected void ASPxScheduler1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
    {
		ASPxSchedulerPopupMenu menu = e.Menu;
        if (menu.Id.Equals(SchedulerMenuItemId.AppointmentMenu)) {
            MenuItemCollection menuItems = menu.Items;
            MenuItem defaultItemOpentAppointment = menuItems.FindByText("Open");
            MenuHelper.RemoveMenuItem(menu, "OpenAppointment");
            MenuHelper.AddMenuItem(menu, 0, "MyOpenAppoinment", defaultItemOpentAppointment.Name);
            MenuItem defaultItemLabelAsImportant = menuItems.FindByText("Important");
            MenuHelper.AddMenuItem(menu, 1, "Make Important", defaultItemLabelAsImportant.Name);
        }
    }
}
