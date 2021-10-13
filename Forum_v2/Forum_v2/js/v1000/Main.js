/*
    註冊event bus, 產生Root Vue實體
*/

Vue.prototype.$bus = new Vue();

this.vm = new Vue({
    el: '#app',
    template:
        `<div>`+
        `<div :is="currentComponent"></div>
         <app-dialog>
         </app-dialog>`+
        `</div>`,
    i18n,
    components: {
        'forumPageComponent': ForumPageComponent,
        'postPageComponent': PostPageComponent,
        'createPageComponent': CreatePageComponent,
        'loginPageComponent': LoginPageComponent,
        'registerPageComponent': RegisterPageComponent
    },
    data: function () {
        return {
            currentComponent: 'forumPageComponent'
        }
    }
})
$.router.set('/');