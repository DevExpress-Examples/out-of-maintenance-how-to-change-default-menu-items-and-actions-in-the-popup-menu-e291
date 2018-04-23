using System;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;


public partial class CustomItemWithDefaultAction : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		DataHelper.SetupMappings(ASPxScheduler1);
		DataHelper.ProvideRowInsertion(ASPxScheduler1, DataSource1.AppointmentDataSource);
		DataSource1.AttachTo(ASPxScheduler1);
	}
    #region #PopupMenuShowing_CustomItem
    protected void ASPxScheduler1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
    {
		ASPxSchedulerPopupMenu menu = e.Menu;
        if (menu.MenuId.Equals(SchedulerMenuItemId.AppointmentMenu)) {
            MenuItemCollection menuItems = menu.Items;
            MenuItem defaultItemOpentAppointment = menuItems.FindByText("Open");
            MenuHelper.RemoveMenuItem(menu, "OpenAppointment");
            MenuHelper.AddMenuItem(menu, 0, "MyOpenAppoinment", defaultItemOpentAppointment.Name);
            MenuItem defaultItemLabelAsImportant = menuItems.FindByText("Important");
            MenuHelper.AddMenuItem(menu, 1, "Make Important", defaultItemLabelAsImportant.Name);
        }
    }
    #endregion #PopupMenuShowing_CustomItem
    #region #MenuHelper
    public class MenuHelper
    {
        public static void RemoveMenuItem(ASPxSchedulerPopupMenu menu, string menuItemName)
        {
            MenuItem item = menu.Items.FindByName(menuItemName);
            if (item != null)
                menu.Items.Remove(item);
        }
        public static void AddMenuItem(ASPxSchedulerPopupMenu menu, int index, string caption, string menuItemName)
        {
            MenuItemCollection items = menu.Items;
            MenuItem item = new MenuItem(caption, menuItemName);
            items.Insert(index, item);
        }
    }
    #endregion #MenuHelper
}
