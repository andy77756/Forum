$.route((e, params, query) => {
    switch (e.route.split('/')[1]) {
        case 'forum':
            this.vm.currentComponent = 'forumPageComponent'           
            break;
        case 'post':
            this.vm.currentComponent = 'postPageComponent'
            break;
        case 'create':
            this.vm.currentComponent = 'createPageComponent'
            break;
        case 'login':
            this.vm.currentComponent = 'loginPageComponent'
            break;
        case 'register':
            this.vm.currentComponent = 'registerPageComponent'
            break;
        default:
            $.router.set('/forum');
            break;
    }
})