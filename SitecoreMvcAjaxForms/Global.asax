<%@Application Language='C#' Inherits="Sitecore.Web.Application" %>
<%@ Import Namespace="SitecoreMvcAjaxForms" %>

<script runat="server">

    public void Application_Start()
    {
        AreaRegistration.RegisterAllAreas();

        RouteConfig.RegisterRoutes(RouteTable.Routes);
        ModelBinderConfig.RegisterModelBinderProviders(ModelBinderProviders.BinderProviders);
    }

</script>
