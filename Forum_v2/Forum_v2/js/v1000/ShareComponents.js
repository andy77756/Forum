/*
    share元件

    nav-bar: 上方導覽列
    app-dialog: 錯誤訊息彈跳視窗
    Pagination: 分頁元件
*/

var NavBarComponent = {
    template:
        `<div class="header header-primary">
          <div class="container">
            <div class="navbar">
              <div class="navbar-brand">
                <a @click="NavTo('forum')">Forum</a>
              </div>
              <div class="navbar-nav">
                <ul >
                  <li class="nav-item dropdown">
                    <a class="nav-link">{{$t('nav.langChoice')}}</a>
                    <div class="dropdown-content">
                      <a class="nav-link" @click="ChangeLang('tw')">{{$t('nav.lang.zh-tw')}}</a>
                      <a class="nav-link" @click="ChangeLang('en')">{{$t('nav.lang.en')}}</a>
                    </div>
                  </li>
                  <li class="nav-item" >
                      <a class="nav-link" @click="NavTo('create')">
                        {{$t('nav.post')}}
                      </a>
                  </li>               
                <template v-if="!!userInfo.userId">
                  <li class="nav-item dropdown">
                    <a class="nav-link">{{userInfo.nickname}}</a>
                    <div class="dropdown-content">
                      <a class="nav-link" >level:{{userInfo.level}}</a>
                      <a class="nav-link" @click="Logout">{{$t('nav.logout')}}</a>
                    </div>
                  </li>
                </template>
                <template v-else>
                  <li class="nav-item">
                      <a class="nav-link" @click="NavTo('login')">{{$t('nav.login')}}</a>
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
        NavTo: function (route) {
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
        ChangeLang: function (lang) {
            this.$i18n.locale = lang;
        },
        Logout: function () {
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
            this.userInfo = JSON.parse(localStorage.getItem("userInfo").toString() ?? '{}');
        }
    }
};

var DialogComponent = {
    template:
        `<div class="wrap" v-show="show">` +
        `<div class="jw-modal">
            <div class="jw-modal-body">
              <div class="jw-modal-header">
                <h1 class="h6 text-center">{{$t('dialog.header')}}</h1>
                <i class="fas fa-window-close" @click="Close"></i>
              </div>
              <div class="jw-modal-content">
                <p class="h6 text-center">
                  {{errorMessage}}
                </p>
              </div>
              <div class="jw-modal-footer">
                <button class="btn btn-primary" @click="Close">{{$t('dialog.confirm')}}</button>
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
        Close: function () {
            this.show = false;
        }
    },
    created: function () {
        //訂閱event bus的errorEvent事件, 觸發事件後修改errorMessage及show
        var global = this;
        this.$bus.$on("errorEvent", function (event) {
            global.errorMessage = event.errorMessage;
            global.show = event.show;
        })
    }
};

var Pagination = {
    template:
        `<div class="pagination">
            <p>Items per page:</p>
            <select v-model="selectedItem">
              <option v-for="item in options" :value="item">{{item}}</option>
            </select>
            <p> {{firstIndex}}-{{lastIndex}} of {{length}}</p>
            <v-pagination
              v-model="page"             
              :total-visible="10"
              :length="pageLength"
              @input="ChangeIndex"              
            ></v-pagination>
          </div>
        `,
    data: function () {
        return {
            selectedItem: 10, //select每頁數量
            page: 1, //當下選中頁數
            options: [5, 10, 25]  //每頁數量選項
        }
    },
    props: {
        //頁數
        pageLength: {
            default: 10,
            required: true
        },
        //資料數量
        length: {
            type: Number,
            required: true
        },
        //目前頁數
        currentIndex: {
            type: Number,
            required: true
        },
        //每頁數量
        pageSize: {
            type: Number,
            required: true
        },
    },
    computed: {
        //目前頁數第一筆資料index
        firstIndex: function () {
            return this.currentIndex * this.pageSize + 1;
        },
        //目前頁數最後一筆資料index
        lastIndex: function () {
            var result = (this.currentIndex + 1) * this.pageSize;
            return result > this.length ? this.length : result;
        }
    },
    watch: {
        //更改每頁數量發送更新資料事件
        selectedItem: function (val) {
            this.$emit('indexChange', { currentIndex: this.currentIndex, pageSize: val });
        }
    },
    methods: {
        //換頁發送更新資料事件
        ChangeIndex: function (event) {
            this.$emit('indexChange', { currentIndex: event - 1, pageSize: this.selectedItem });
        },
    }
};
