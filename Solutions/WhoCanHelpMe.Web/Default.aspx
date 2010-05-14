<%@ Page Language="C#" %>
<!-- Please do not delete this file.  It is used to ensure that ASP.NET MVC is activated by IIS when a user makes a "/" request to the server. -->

<script runat="server">
	protected override void OnInit(EventArgs args)
	{
		var httpContext = HttpContext.Current;
		HttpContextBase contextWrapper = new HttpContextWrapper(httpContext);
		var routeData = GetRouteData(contextWrapper);
		var httpHandler = routeData.RouteHandler.GetHttpHandler(new RequestContext(contextWrapper, routeData));
		httpHandler.ProcessRequest(httpContext);
	}

	private static RouteData GetRouteData(HttpContextBase context)
	{
	    return RouteTable.Routes
                         .Select(route => route.GetRouteData(context))
                         .FirstOrDefault(routeData => routeData != null);
	}

</script>