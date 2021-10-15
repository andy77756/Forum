<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Forum_v2.view._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/@mdi/font@6.x/css/materialdesignicons.min.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/vuetify@2.x/dist/vuetify.min.css" rel="stylesheet">
    <link href="<%= ResolveUrl("../css/"+CurrentVersion+"/styles.css") %>" rel="stylesheet" />
</head>
<body>
    <div id="app"></div>     

    <script src="https://kit.fontawesome.com/557a6f88a1.js" crossorigin="anonymous"></script>   
    <script src="../js/jquery-2.1.4.js"></script>
    <script src="../js/jquery.router.min.js"></script>
    <script src="../js/vue.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/vuetify@2.x/dist/vuetify.js"></script>
    <script src="https://unpkg.com/vue-i18n@8.26.5/dist/vue-i18n.js"></script>
    <script src="<%= ResolveUrl("../js/"+CurrentVersion+"/i18n.js") %>"></script>
    <script src="<%= ResolveUrl("../js/"+CurrentVersion+"/ShareComponents.js") %>"></script>
    <script src="<%= ResolveUrl("../js/"+CurrentVersion+"/PageComponents/CreatePageComponent.js") %>"></script>
    <script src="<%= ResolveUrl("../js/"+CurrentVersion+"/PageComponents/ForumPageComponent.js") %>"></script>
    <script src="<%= ResolveUrl("../js/"+CurrentVersion+"/PageComponents/LoginPageComponent.js") %>"></script>
    <script src="<%= ResolveUrl("../js/"+CurrentVersion+"/PageComponents/PostPageComponent.js") %>"></script>
    <script src="<%= ResolveUrl("../js/"+CurrentVersion+"/PageComponents/RegisterPageComponent.js") %>"></script>  
    <script src="<%= ResolveUrl("../js/"+CurrentVersion+"/RouterConfig.js") %>"></script>
    <script src="<%= ResolveUrl("../js/"+CurrentVersion+"/Main.js") %>"></script>

</body>
</html>
