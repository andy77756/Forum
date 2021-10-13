/*
    新增文章頁面
*/

var CreatePageComponent = Vue.extend({
    template:
        `<div>` +
        //導覽列
        `<nav-bar></nav-bar>` +
        // Form表單
        `<div class="editor-page">
                <div class="container page">
                    <div class="row">
                        <form>
                            <fieldset class="form-control">
                                <input v-model="newPost.topic" class="form-control form-control-lg" type="text" :placeholder="$t('create.topic')" />
                            </fieldset>
                            <fieldset class="form-control">
                                <textarea v-model="newPost.content" class="form-control" rows="8" :placeholder="$t('create.content')"></textarea>
                            </fieldset>
                            <div class="form-control-right">
                                <button @click="send" type="button" class="btn btn-primary">
                                    {{$t('create.createBtn')}}
                                </button>
                                <button class="btn btn-alert" @click="cancel">
                                    {{$t('create.cancelBtn')}}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>` +
        `</div>`
    ,
    data: function () {
        return {
            newPost: {
                userId: 0, //發文者ID
                topic: '', //文章標題
                content: '', //文章內容
                token: ''  //JWT
            }
        }
    },
    components: {
        'nav-bar': NavBarComponent
    },
    methods: {
        cancel: function() {
            $.router.set('/forum');
        },
        send: function () {
            //發送新增文章ajax, 成功 or 無權限則導回首頁, 發生錯誤皆顯示dialog
            var local = this;
            $.ajax({
                type: "POST",
                url: "/ajax/ForumService.aspx/AddPost",
                data: JSON.stringify(this.newPost),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = JSON.parse(response.d);
                    if (result.statusCode != 1) {
                        vm.$bus.$emit("errorEvent", { errorMessage: local.$t('error.'+ result.statusCode.toString()), show: true });

                        if (result.statusCode == -8) {
                            $.router.set('/Forum');
                        }
                        else if (result.statusCode == -7) {
                            $.router.set({route: '/login', queryString: 'redirect=Create'})
                        }
                    }
                    else {
                        $.router.set('/Forum');
                    }
                }
            });
        },
    },
    created: function () {
        //若localStorage不為空則取userInfo欄位值反序列化為物件後將userId及token指派給newPost.userId及newPost.token
        if (localStorage.getItem('userInfo') != null) {
            var userInfo = JSON.parse(localStorage.getItem("userInfo").toString() ?? '{}');
            this.newPost.userId = userInfo.userId;
            this.newPost.token = userInfo.token;
        }
    }
});