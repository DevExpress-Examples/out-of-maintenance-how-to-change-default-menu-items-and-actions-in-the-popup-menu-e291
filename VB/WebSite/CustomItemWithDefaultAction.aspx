<%@ Page Language="vb" AutoEventWireup="true" CodeFile="CustomItemWithDefaultAction.aspx.vb" Inherits="CustomItemWithDefaultAction" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v13.1.Core" Namespace="DevExpress.XtraScheduler"
	TagPrefix="cc2" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dxm" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v13.1.Core, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler"
	TagPrefix="dxwschs" %>

<%@ Register Src="~/DefaultObjectDataSources.ascx" TagName="DefaultObjectDataSources" TagPrefix="dds" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Untitled Page</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<dds:DefaultObjectDataSources runat="server" ID="DataSource1" />
		<dxm:ASPxMenu ID="ASPxMenu1" runat="server">
			<Items>
				<dxm:MenuItem NavigateUrl="~/DefaultItemWithCustomAction.aspx" Text="Default item with custom action">
				</dxm:MenuItem>
				<dxm:MenuItem NavigateUrl="~/CustomItemWithDefaultAction.aspx" Text="Custom item with default action">
				</dxm:MenuItem>
			</Items>
		</dxm:ASPxMenu>
		<dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server"  GroupType="Resource" Start="2008-07-17" ClientInstanceName="Scheduler1"
		   OnPopupMenuShowing="ASPxScheduler1_PopupMenuShowing">
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
</body>
</html>