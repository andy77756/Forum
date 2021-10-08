<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Forum_v2.view._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://kit.fontawesome.com/557a6f88a1.js" crossorigin="anonymous"></script>
    <script src="../js/jquery-2.1.4.js"></script>
    <script src="../js/jquery.router.min.js"></script>
    <script src="../js/vue.js"></script>
    <script src="https://unpkg.com/vue-i18n@8.26.5/dist/vue-i18n.js"></script>
    <script src="../js/v1000/i18n.js"></script>
    <script src="https://unpkg.com/@reactivex/rxjs@5.0.3/dist/global/Rx.js"></script>
    <script src="../js/v1000/ShareComponents.js"></script>
    <script src="../js/v1000/PageCompnents.js"></script>
    <link href="../css/v1000/styles.css" rel="stylesheet" />
    <script src="../js/v1000/RouterConfig.js"></script>

</head>
<body>
    <div id="app">
        <div :is="currentComponent"></div>

        <app-dialog :show.sync="showdialog">
            {{errorMessage}}
        </app-dialog>
    </div>
    

    <script>        
        this.vm = new Vue({
            el: '#app',
            i18n,
            data: function () {
                return {
                    currentComponent: 'forumPageComponent',
                    showdialog: false,
                    errorMessage: ''
                }
            },
            components: {
                'forumPageComponent': ForumPageComponent,
                'postPageComponent': PostPageComponent,
                'createPageComponent': CreatePageComponent,
                'loginPageComponent': LoginPageComponent,
                'registerPageComponent': RegisterPageComponent,
                //'dialog': DialogComponent
            }
        })
        $.router.set('/');
    </script>
</body>
</html>
