/*
    註冊event bus, 產生Root Vue實體
*/

Vue.prototype.$bus = new Vue();

this.vm = new Vue({
    el: '#app',
    template:
        `<div>`+
        `<div :is="currentComponent"></div>
         <errorDialog>
         </errorDialog>`+
        `</div>`,
    components: {
        'forumPageComponent': ForumPageComponent,
        'postPageComponent': PostPageComponent,
        'createPageComponent': CreatePageComponent,
        'loginPageComponent': LoginPageComponent,
        'registerPageComponent': RegisterPageComponent,
        'errorDialog': DialogComponent
    },
    i18n,
    vuetify: new Vuetify(),
    data: function () {
        return {
            currentComponent: 'forumPageComponent'
        }
    } 
})
$.router.set('/');