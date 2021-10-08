var ForumPageComponent = Vue.extend({
    data: function () {
        return {
            posts: [],
            text: '',
            selected: 'keyTopic',
            searchKey: new Map()
        }
    },
    template:
        `<div>` +
        `<nav-bar></nav-bar>` +
        `<div class="container">` +
        //search
        `<div class="search-container">
            <ul>
              <li>
                <input
                  type="text"
                  v-bind:placeholder="$t('searchBar.search')"
                  v-model = "text"
                >
              </li>
              <li>
                <div class="select">
                  <select v-model="selected">
                    <option value="keyTopic">{{$t('searchBar.topic')}}</option>
                    <option value="keyNickname">{{$t('searchBar.nickname')}}</option>
                  </select>
                  <div class="select__arrow">
                  </div>
                </div>
              </li>
              <li>
                <div class="select">
                <button class="searchButton" @click= "searchFilter">{{$t('searchBar.search')}}</button>
                </div>
              </li>
            </ul>
        </div>`+
        //table      
        `<div class="row">
            <table class="table table-primary table-bordered">
              <thead>
                <tr>
                  <td class="firstField">
                    <p>
                      {{$t('posts.topic')}}
                    </p>
                  </td>
                  <td class="notFirstField">
                    <p>
                      {{$t('posts.postUserName')}}
                    </p>
                  </td>
                  <td class="notFirstField">
                    <p>
                      {{$t('posts.replyCount')}}
                    </p>
                  </td>
                  <td class="notFirstField">
                    <p>
                      {{$t('posts.replyUserName')}}
                    </p>
                  </td>
                </tr>
              </thead>             
             <template v-for="item in posts">
              <tbody >
                <tr>
                  <td class="firstField">
                    <a @click="toPost(item.postid)"  routerLinkActive="router-link-active">
                      <p>
                        {{item.topic}}
                      </p>
                    </a>
                  </td>
                  <td>
                    <p class="userName">{{item.postUserName}}</p>
                    <p class="userName datetime">{{item.postAt}}</p>
                  </td>
                  <td>
                    <p>{{item.replyCount}}</p>
                  </td>
                  <td>
                    <p class="userName">{{item.replyUserName}}</p>
                    <p class="userName datetime">{{item.replyAt}}</p>
                  </td>
                </tr>
              </tbody>
            </template>
            </table>
          </div>`+
        `</div>`+
        `</div>`
    ,
    components: {
        'nav-bar': NavBarComponent
    },
    methods: {
        toPost(postid) {
            $.router.set(`/post/${postid}`);
        },
        searchFilter() {
            this.searchKey.set('keyTopic', '');
            this.searchKey.set('keyNickname', '');
            this.searchKey.set(this.selected, this.text);
            this.sendSearch(this.searchKey.get('keyTopic'), this.searchKey.get('keyNickname'), 0, 5);
        },
        sendSearch(topic, nickname, index, size) {
            var datas = this;
            var postData = {
                topic: topic,
                nickname: nickname,
                index: 0,
                size: 5
            };
            $.ajax({
                type: "POST",
                url: "/ajax/ForumService.aspx/GetPosts?"+ `topic=${topic}&nickname=${nickname}&index=${index}&size=${size}`,
                data: JSON.stringify(postData),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = JSON.parse(response.d);
                    datas.posts = result.returnData.posts;
                }
            });
        }
    },
    created() {
        this.searchKey.set('keyTopic', '');
        this.searchKey.set('keyNickname', '');
        this.sendSearch("", "", 0, 10);
    }
});

var PostPageComponent = Vue.extend({
    data: function () {
        return {
            postId: 0,
            post: {
                postid: 0,
                topic: '',
                content: '',
                postUserName: '',
                postAt: '',
                replyUserName: '',
                replyAt: '',
                replyCount: 0
            },
            replies: [
            ],
            reply: {
                userId: 0,
                postId: 0,
                content: '',
                token: ''
            },
            queryParam: {
                key: '',
                searchField: '',
                page: 0,
                size: 10
            }
        }
    },
    template:
        `<div>` +
        `<nav-bar></nav-bar>` +
        `<div class="container">` +
        //display content
        `<template v-if="post.topic != ''">
            <div class="post-header">
              <h1 class="h6">{{post.topic}}</h1>
            </div>
            <div class="post-content">
              <div class="userInfo">
                <div class="post-userInfo">
                  <div>
                    <p>{{post.postUserName}}</p>
                  </div>
                  <div>
                    <p>{{$t('post.postAt')}}</p>
                    <p>{{post.postAt}}</p>
                  </div>
                </div>
              </div>
              <div class="post-body">
                  {{post.content}}
              </div>
            </div>
            <template v-for="reply in replies">
            <div class="reply-content">
              <div class="reply-body">
                  {{reply.content}}
              </div>
              <div class="userInfo">
                <div class="reply-userInfo">
                  <div>
                    <p>{{reply.nickname}}</p>
                  </div>
                  <div>
                    <p>{{$t('post.replyAt')}}</p>
                    <p>{{reply.createAt}}</p>
                  </div>
                </div>
              </div>
            </div>
            </template>
        </template>`+
        //reply content
        `<div class="reply-edit">
            <form>
                <fieldset class="form-group">
                    <textarea class="form-control" name="content" v-model="reply.content" :placeholder="$t('post.placeHolder')">
                  </textarea>
                </fieldset>
                <button type="button" class="btn btn-primary" @click="addReply">{{$t('post.addReply')}}</button>
            </form>
        </div>`+
        `</div>`+
        `</div>`
    ,
    components: {
        'nav-bar': NavBarComponent
    },
    methods: {
        addReply() {
            var local = this;
            $.ajax({
                type: "POST",
                url: "/ajax/ForumService.aspx/AddReply",
                data: JSON.stringify(this.reply),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = JSON.parse(response.d);
                    if (result.statusCode != 1) {
                        vm.errorMessage = local.$t(`error.${result.statusCode}`);
                        vm.showdialog = true;
                    }
                }
            });
        },
        sendSearch(postid, index, size) {
            var datas = this;
            var postData = {
                id: postid,
                index: index,
                size: size
            };
            $.ajax({
                type: "POST",
                url: "/ajax/ForumService.aspx/GetPostById?" + `id=${postid}&index=${index}&size=${size}`,
                data: JSON.stringify(postData),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = JSON.parse(response.d);
                    datas.post = result.returnData.post;
                    datas.replies = result.returnData.replies;
                }
            });
        }
    },
    created() {
        if (localStorage.getItem('userInfo') != null) {
            const userInfo = JSON.parse(localStorage.getItem("userInfo").toString() ?? '{}');
            this.reply.userID = userInfo.userId;
            this.reply.token = userInfo.token;
        }
        this.postId = +(window.location.pathname.split('/')[2]);
        this.reply.postId = +(window.location.pathname.split('/')[2]);
        this.sendSearch(this.postId, 0, 5);
    }
});

var CreatePageComponent = Vue.extend({
    data: function () {
        return {
            newPost: {
                topic: '',
                content:''
            }
        }
    },
    template:
        `<div>` +
            `<nav-bar></nav-bar>` +
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
    components: {
        'nav-bar': NavBarComponent
    },
    methods: {
        send() {
            console.log('send');
        },
        cancel() {
            $.router.set('/forum');
        }
    }
});

var LoginPageComponent = Vue.extend({
    data: function () {
        return {
            userLogin: {
                userName: '',
                pwd: ''
            }           
        }
    },
    template:
        `<div class="wrap">` +
        `<div class="container">
            <div class="row">
                <form>
                    <fieldset class="form-control">
                        <input v-model="userLogin.userName"  type="text" :placeholder="$t('login.userName')">
                    </fieldset>
                    <fieldset class="form-control">
                        <input v-model="userLogin.pwd"  type="password" :placeholder="$t('login.pwd')">
                    </fieldset>
                    <div class="form-control form-control-right">
                        <button class="btn  btn-primary"
                            type="button"
                            @click="login">
                            {{$t('login.login')}}
                        </button>
                    </div>
                    <div class="form-control form-control-right">
                        <a class="a-link" @click="navTo"> register </a>
                    </div>
                </form>
            </div>
         </div>` +
        `</div>`,
    methods: {
        login() {
            var local = this;
            $.ajax({
                type: "POST",
                url: "/ajax/AuthorizeService.aspx/Login",
                data: JSON.stringify(this.userLogin),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = JSON.parse(response.d);
                    if (result.statusCode != 1) {
                        console.log(vm);
                        vm.errorMessage = local.$t(`error.${result.statusCode}`);
                        vm.showdialog = true;
                    }
                    else {
                        localStorage.setItem('userInfo', JSON.stringify(result.returnData));
                        $.router.set('/Forum');
                    }
                }
            });
        },
        navTo() {
            $.router.set('/register');
        }
    }
});

var RegisterPageComponent = Vue.extend({
    data: function () {
        return {
            userRegister: {
                userName: '',
                nickname: '',
                pwd: '',
            }
        }
    },
    template:
        `<div class="wrap">` +
            `<div class="container">
                <div class="row">
                    <form>
                        <fieldset class="form-control">
                            <input v-model="userRegister.userName" name="loginUserName" type="text" :placeholder="$t('login.userName')">
                        </fieldset>
                        <fieldset class="form-control">
                            <input
                              v-model="userRegister.nickname" name="nickname" type="text" :placeholder="$t('login.nickname')" >
                          </fieldset>
                        <fieldset class="form-control">
                            <input v-model="userRegister.pwd" name="loginPwd" type="password" :placeholder="$t('login.pwd')">
                        </fieldset>
                        <div class="form-control form-control-right">
                            <button class="btn  btn-primary"
                                type="button"
                                @click="register">
                                {{$t('login.register')}}
                            </button>
                        </div>
                        <div class="form-control form-control-right">
                            <a class="a-link" @click="navTo"> login </a>
                        </div>
                    </form>
                </div>
            </div>` +
        `</div>`,
    methods: {
        register() {
            var local = this;
            $.ajax({
                type: "POST",
                url: "/ajax/AuthorizeService.aspx/Register",
                data: JSON.stringify(this.userRegister),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = JSON.parse(response.d);
                    if (result.statusCode != 1) {
                        console.log(vm);
                        vm.errorMessage = local.$t(`error.${result.statusCode}`);
                        vm.showdialog = true;
                    }
                    else {
                        localStorage.setItem('userInfo', JSON.stringify(result.returnData));
                        $.router.set('/Forum');
                    }
                }
            });
        },
        navTo() {
            $.router.set('/login');
        }
    }
});