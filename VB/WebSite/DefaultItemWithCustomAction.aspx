<%@ Page Language="vb" AutoEventWireup="true" CodeFile="DefaultItemWithCustomAction.aspx.vb" Inherits="DefaultItemWithCustomAction" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v10.2.Core" Namespace="DevExpress.XtraScheduler"
	TagPrefix="cc2" %>

<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dxm" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v10.2.Core, Version=10.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.2, Version=10.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler"
	TagPrefix="dxwschs" %>

<%@ Register Src="~/DefaultObjectDataSources.ascx" TagName="DefaultObjectDataSource" TagPrefix="dds" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
	<title>Untitled Page</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<dds:DefaultObjectDataSource runat="server" ID="DataSource1" />
		<dxm:ASPxMenu ID="ASPxMenu1" runat="server">
			<Items>
				<dxm:MenuItem NavigateUrl="~/DefaultItemWithCustomAction.aspx" Text="Default item with custom action">
			</dxm:MenuItem>
			<dxm:MenuItem NavigateUrl="~/CustomItemWithDefaultAction.aspx" Text="Custom item with default action">
			</dxm:MenuItem>
			</Items>
		</dxm:ASPxMenu>
		<dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" GroupType="Resource" Start="2008-07-17" ClientInstanceName="Scheduler1" 
			OnBeforeExecuteCallbackCommand="ASPxScheduler1_BeforeExecuteCallbackCommand" OnPopupMenuShowing="ASPxScheduler1_PopupMenuShowing">
			<Views>
<DayView><TimeRulers>
<cc1:TimeRuler></cc1:TimeRuler>
</TimeRulers>
</DayView>

<WorkWeekView><TimeRulers>
<cc1:TimeRuler></cc1:TimeRuler>
</TimeRulers>
</WorkWeekView>
</Views>
		</dxwschs:ASPxScheduler>
	</div>
	</form>
	<script type="text/javascript">
		function DefaultViewMenuHandler(scheduler, s, args) {
			if (args.item.GetItemCount() <= 0) {
				if (args.item.name == "GotoToday") {
					if (window.confirm("Are you realy want to leave this day?")) {
						scheduler.RaiseCallback("GOTODAY|" + args.item.name);
					}
				}
				else
					scheduler.RaiseCallback("MNUVIEW|" + args.item.name);
			}
		}
	</script>
</body>
</html>