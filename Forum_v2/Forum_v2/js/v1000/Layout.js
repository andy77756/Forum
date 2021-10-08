var Test1 = Vue.extend({
    template:`<h1>{{num}}</h1>`,
    data: function() {
            return {
                num: 1
            }
     } 
})

var Test2 = Vue.extend({
    template: `<div><div>{{num}}</div></div>`,
    data: function() {
            return {
                num:2
            }
    },
    created() {
        console.log("test2 Created");
    }
})

var Layout = Vue.extend({
    data: function() {
            return {
                userInfo: {
                    token: '',
                    userId: 0,
                    userName: '',
                    nickname: '',
                    level: 0
                }
            }
    },
    methods: {
        navTo() {
            $.router.set('/test2');
        }
    },
    template:
        `<nav class="header header-primary">
          <div class="container">
            <div class="navbar">
              <div class="navbar-brand">
                <a>Forum</a>
              </div>
              <div class="navbar-nav">
                <ul >
                  <li class="nav-item dropdown">
                    <a class="nav-link">語言</a>
                    <div class="dropdown-content">
                      <a class="nav-link" @click="navTo">中文</a>
                      <a class="nav-link" >英文</a>
                    </div>
                  </li>
                  <li class="nav-item" >
                      <a class="nav-link" >
                        新增貼文
                      </a>
                  </li>
                  <li class="nav-item">
                      <a class="nav-link">登入</a>
                  </li>
                  <li class="nav-item dropdown">
                    <a class="nav-link">abab123</a>
                    <div class="dropdown-content">
                      <a class="nav-link" >level:</a>
                      <a class="nav-link" >登出</a>
                    </div>
                  </li>
              </ul>
              </div>
            </div>
          </div>
        </nav>`,
    created() {
        $.router.init();
       
        $.route('test', (e, params, query) => {
            console.log('123');
            console.log(e);
            console.log(params);
            console.log(query);
        })
    }

})