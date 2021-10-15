/*
    登入頁面
*/

var LoginPageComponent = {
    template:
        `<div class="wrap">` +
        `<div class="container">
            <div class="row">
                <div class="tabs">
                    <input type="radio" class="tabs-radio"  id="tab1" checked>
                    <label for="tab1" class="tabs-label">{{$t('login.login')}}</label>
                    <div class="tabs-content">
                        <form>
                            <fieldset class="form-control">
                                <input v-model="userLogin.userName" @keyup="UserNameValidate" type="text" :placeholder="$t('login.userName')">
                            </fieldset>
                            <fieldset class="form-control" v-show="!validation.userName.valid">
                                <p v-for="item in validation.userName.errorMessage">{{item}}</p>
                            </fieldset>
                            <fieldset class="form-control">
                                <input v-model="userLogin.pwd" @keyup="PwdValidate"  type="password" :placeholder="$t('login.pwd')">
                            </fieldset>
                            <fieldset class="form-control" v-show="!validation.pwd.valid">
                                <p v-for="item in validation.pwd.errorMessage">{{item}}</p>
                            </fieldset>
                            <div class="form-control form-control-right">
                                <button class="btn  btn-primary"
                                    type="button"
                                    :disabled="!(validation.userName.valid&validation.pwd.valid)"
                                    @click="Login">
                                    {{$t('login.login')}}
                                </button>
                                <button class="btn  btn-alert"  @click="NavTo('/forum')">{{$t('login.cancel')}}</button>
                            </div>
                        </form>
                    </div>
                    <input type="radio" class="tabs-radio"  id="tab2" @click="NavTo('/register')">
                    <label for="tab2" class="tabs-label">{{$t('login.register')}}</label>
                </div>
            </div>
         </div>` +
        `</div>`,
    data: function () {
        return {
            userLogin: {
                userName: '',
                pwd: ''
            },
            validation: {
                userName: {
                    valid: false,
                    errorMessages:[]
                },
                pwd: {
                    valid: false,
                    errorMessage:[]
                }
            }
        }
    },
    methods: {
        /* 
           username input keyup事件驗證方法
           長度5~30字元, 第一個字元大小英文,其餘可以大小寫英數字
        */
        UserNameValidate: function (event) {
            this.validation.userName.valid = true;
            this.validation.userName.errorMessage = [];
            if (this.userLogin.userName.length < 5) {
                this.validation.userName.errorMessage.push(this.$t('error.invalidMinLength', {count: 5}));
                this.validation.userName.valid = false;
            }
            if (this.userLogin.userName.length > 30) {
                this.validation.userName.errorMessage.push(this.$t('error.invalidMaxLength', {count: 30}));
                this.validation.userName.valid = false;
            }
            var userNameRegExp = new RegExp('^[a-zA-Z][a-zA-Z0-9_-]{5,30}$');
            if (!userNameRegExp.test(this.userLogin.userName)) {
                this.validation.userName.errorMessage.push(this.$t('error.invalidPattern'));
                this.validation.userName.valid = false;
            }
        },
        /*
           pwd input keyup事件驗證方法
           長度6~20字元, 至少包含一個大寫英文一個小寫英文一個數字
        */
        PwdValidate: function (event) {
            this.validation.pwd.valid = true;
            this.validation.pwd.errorMessage = [];
            if (this.userLogin.pwd.length < 6) {
                this.validation.pwd.errorMessage.push(this.$t('error.invalidMinLength', { count: '6' }));
                this.validation.pwd.valid = false;
            }
            if (this.userLogin.pwd.length > 20) {
                this.validation.pwd.errorMessage.push(this.$t('error.invalidMaxLength', { count: '20' }));
                this.validation.pwd.valid = false;
            }
            var pwdRegExp = new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9-_]{6,30}$');
            if (!pwdRegExp.test(this.userLogin.pwd)) {
                this.validation.pwd.errorMessage.push(this.$t('error.invalidPattern'));
                this.validation.pwd.valid = false;
            }
        },
        Login: function () {          
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
                        vm.$bus.$emit("errorEvent", { errorMessage: local.$t('error.' + result.statusCode), show: true });
                    }
                    else {
                        localStorage.setItem('userInfo', JSON.stringify(result.returnData));
                        $.router.set('/Forum');
                    }
                }
            });
        },
        NavTo: function(route) {
            $.router.set(route);
        }
    }
};