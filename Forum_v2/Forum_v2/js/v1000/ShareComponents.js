/*
    share元件

    nav-bar: 上方導覽列
    app-dialog: 錯誤訊息彈跳視窗
*/

var NavBarComponent = Vue.component('nav-bar', {
    template:
        `<div class="header header-primary">
          <div class="container">
            <div class="navbar">
              <div class="navbar-brand">
                <a @click="navTo('forum')">Forum</a>
              </div>
              <div class="navbar-nav">
                <ul >
                  <li class="nav-item dropdown">
                    <a class="nav-link">{{$t('nav.langChoice')}}</a>
                    <div class="dropdown-content">
                      <a class="nav-link" @click="changeLang('tw')">{{$t('nav.lang.zh-tw')}}</a>
                      <a class="nav-link" @click="changeLang('en')">{{$t('nav.lang.en')}}</a>
                    </div>
                  </li>
                  <li class="nav-item" >
                      <a class="nav-link" @click="navTo('create')">
                        {{$t('nav.post')}}
                      </a>
                  </li>               
                <template v-if="!!userInfo.userId">
                  <li class="nav-item dropdown">
                    <a class="nav-link">{{userInfo.nickname}}</a>
                    <div class="dropdown-content">
                      <a class="nav-link" >level:{{userInfo.level}}</a>
                      <a class="nav-link" @click="logout">{{$t('nav.logout')}}</a>
                    </div>
                  </li>
                </template>
                <template v-else>
                  <li class="nav-item">
                      <a class="nav-link" @click="navTo('login')">{{$t('nav.login')}}</a>
                  </li>
                </template>
              </ul>
              </div>
            </div>
          </div>
        </div>`,
    data: function () {
        return {
            userInfo: {
                token: '',
                userId: 0,
                username: '',
                nickname: '',
                level: 0
            }
        }
    },
    methods: {
        navTo: function (route) {
            // route = create: 判斷level權限, 權限不足show dialog; 
            if (route == 'create') {
                if (this.userInfo.level < 2) {
                    this.$bus.$emit('errorEvent', { errorMessage: this.$t('error.-8'), show: true });
                }
                else {
                    $.router.set('/' + route);
                }
            }
            else {
                $.router.set('/' + route);
            }
        },
        changeLang: function(lang) {
            this.$i18n.locale = lang;
        },
        logout: function () {
            // 清空localStorage, 將userInfo設為預設值
            localStorage.clear();
            this.userInfo = {
                token: '',
                userId: 0,
                username: '',
                nickname: '',
                level: 0
            }
        }
    },
    created: function () {
        //若localStorage不為空則取userInfo欄位值反序列化為物件後指派給data:userInfo
        if (localStorage.getItem('userInfo') != null) {
            this.userInfo  = JSON.parse(localStorage.getItem("userInfo").toString() ?? '{}');           
        }
    }
})

var DialogComponent = Vue.component('app-dialog', {
    template:
        `<div class="wrap" v-show="show">`+
        `<div class="jw-modal">
            <div class="jw-modal-body">
              <div class="jw-modal-header">
                <h1 class="h6 text-center">{{$t('dialog.header')}}</h1>
                <i class="fas fa-window-close" @click="close"></i>
              </div>
              <div class="jw-modal-content">
                <p class="h6 text-center">
                  {{errorMessage}}
                </p>
              </div>
              <div class="jw-modal-footer">
                <button class="btn btn-primary" @click="close">{{$t('dialog.confirm')}}</button>
              </div>
            </div>
          </div>
          <div class="jw-modal-background"></div>`+
        `</div>`,
    data: function () {
        return {
            errorMessage: '',
            show: false
        }
    },
    methods: {
        close: function() {
            this.show = false;
        }
    },
    created: function () {
        //訂閱event bus的errorEvent事件, 觸發事件後修改errorMessage及show
        var global = this;
        this.$bus.$on("errorEvent", function(event){
            global.errorMessage = event.errorMessage;
            global.show = event.show;
        })
    }
})