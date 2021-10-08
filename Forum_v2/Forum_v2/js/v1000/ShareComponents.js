var NavBarComponent = Vue.component('nav-bar', {
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
    methods: {
        navTo(route) {
            if (route == 'create') {
                if (this.userInfo.level < 2) {
                    vm.errorMessage = this.$t('error.-8');
                    vm.showdialog = true;
                }
                else {
                    $.router.set(`/${route}`);
                }
            }
            else {
                $.router.set(`/${route}`);
            }
        },
        changeLang(lang) {
            this.$i18n.locale = lang;
        },
        logout() {
            this.userInfo = {
                token: '',
                userId: 0,
                username: '',
                nickname: '',
                level: 0
            }
        }
    },
    created() {
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
                  <slot>error</slot>
                </p>
              </div>
              <div class="jw-modal-footer">
                <button class="btn btn-primary" @click="close">{{$t('dialog.confirm')}}</button>
              </div>
            </div>
          </div>
          <div class="jw-modal-background"></div>`+
        `</div>`,
    props: {
        show: {
            type: Boolean,
            required: true,
            twoWay: true
        }
    },
    methods: {
        close() {
            vm.showdialog = false;
        }
    }
})