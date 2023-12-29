<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
* [DefaultItemWithCustomAction.aspx](./CS/WebSite/DefaultItemWithCustomAction.aspx) (VB: [DefaultItemWithCustomAction.aspx](./VB/WebSite/DefaultItemWithCustomAction.aspx))
* [DefaultItemWithCustomAction.aspx.cs](./CS/WebSite/DefaultItemWithCustomAction.aspx.cs) (VB: [DefaultItemWithCustomAction.aspx.vb](./VB/WebSite/DefaultItemWithCustomAction.aspx.vb))
<!-- default file list end -->
# How to change default menu items and actions in the popup menu


<p>This demo project shows  and how to add a new menu item and assign a built-in scheduler action to it. <br />
The project is composed of two pages, you can switch between them by using a button at the top left. </p><p>The page named <strong>DefaultItemWithCustomAction</strong> illustrates how to change the default action assigned to a menu item.  It uses two different techniques to modify the existing menu.  <br />
One technique involves a JavaScript function defined in the page markup that replaces the default <i>GoTo</i> command with  a custom handler. The <a href="http://help.devexpress.com/#AspNet/DevExpressWebASPxSchedulerASPxScheduler_PopupMenuShowingtopic"><u>PopupMenuShowing</u></a> event is handled to specify a JavaScript handler.<br />
Another technique implements a <strong>CustomMenuViewCallbackCommand</strong> that is used to perform a callback. The <a href="http://help.devexpress.com/#AspNet/DevExpressWebASPxSchedulerASPxScheduler_BeforeExecuteCallbackCommandtopic"><u>BeforeExecuteCallbackCommand</u></a> event should be handled to process the command.  The <a href="http://help.devexpress.com/#AspNet/DevExpressWebASPxSchedulerASPxScheduler_PopupMenuShowingtopic"><u>PopupMenuShowing</u></a> event is handled to rename a menu item so it can be properly recognized by a custom callback command.<br />
The page named <strong>CustomItemWithDefaultAction</strong> adds two new menu items - <strong>MyOpenAppoinment</strong> that executes a built-in "Open Appointment" command and <strong>Make Important</strong> item that executes the same command as a menu item that labels the appointment as "Important". The name for the menu item is obtained from a corresponding item contained in the  "Label As" submenu.</p><br />


<br/>


