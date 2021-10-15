/*
    註冊頁面
*/

var RegisterPageComponent = {
    template:
        `<div class="wrap">` +
        `<div class="container">
                <div class="row">
                    <div class="tabs">
                        <input type="radio" class="tabs-radio"  id="tab1" @click="NavTo('/login')">
                        <label for="tab1" class="tabs-label">{{$t('login.login')}}</label>

                        <input type="radio" class="tabs-radio"  id="tab2"  checked>
                        <label for="tab2" class="tabs-label">{{$t('login.register')}}</label>
                        <div class="tabs-content">
                            <form>
                                <fieldset class="form-control">
                                    <input v-model="userRegister.userName" @keyup="UserNameValidate" name="loginUserName" type="text" :placeholder="$t('login.userName')">
                                </fieldset>
                                <fieldset class="form-control" v-show="!validation.userName.valid">
                                    <p v-for="item in validation.userName.errorMessage">{{item}}</p>
                                </fieldset>
                                <fieldset class="form-control">
                                    <input
                                      v-model="userRegister.nickname" @keyup="NicknameValidate" name="nickname" type="text" :placeholder="$t('login.nickname')" >
                                </fieldset>
                                <fieldset class="form-control" v-show="!validation.nickname.valid">
                                    <p v-for="item in validation.nickname.errorMessage">{{item}}</p>
                                </fieldset>
                                <fieldset class="form-control">
                                    <input v-model="userRegister.pwd" @keyup="PwdValidate" name="loginPwd" type="password" :placeholder="$t('login.pwd')">
                                </fieldset>
                                <fieldset class="form-control" v-show="!validation.pwd.valid">
                                    <p v-for="item in validation.pwd.errorMessage">{{item}}</p>
                                </fieldset>
                                <div class="form-control form-control-right">
                                    <button class="btn  btn-primary"
                                        type="button"
                                        :disabled="!(validation.userName.valid&validation.nickname.valid&validation.pwd.valid)"
                                        @click="Register">
                                        {{$t('login.register')}}
                                    </button>
                                    <button class="btn  btn-alert" @click="NavTo('/forum')">{{$t('login.cancel')}}</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>` +
        `</div>`,
    data: function () {
        return {
            userRegister: {
                userName: '',
                nickname: '',
                pwd: '',
            },
            validation: {
                userName: {
                    valid: false,
                    errorMessages: []
                },
                nickname: {
                    valid: false,
                    errorMessages: []
                },
                pwd: {
                    valid: false,
                    errorMessage: []
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
            if (this.userRegister.userName.length < 5) {
                this.validation.userName.errorMessage.push(this.$t('error.invalidMinLength', { count: 5 }));
                this.validation.userName.valid = false;
            }
            if (this.userRegister.userName.length > 30) {
                this.validation.userName.errorMessage.push(this.$t('error.invalidMaxLength', { count: 30 }));
                this.validation.userName.valid = false;
            }
            var userNameRegExp = new RegExp('^[a-zA-Z][a-zA-Z0-9_-]{5,30}$');
            if (!userNameRegExp.test(this.userRegister.userName)) {
                this.validation.userName.errorMessage.push(this.$t('error.invalidPattern'));
                this.validation.userName.valid = false;
            }
        },
        /*
           nickname input keyup事件驗證方法
           長度1~10字元, 第一個字元大小英文,其餘可以大小寫英數字
        */
        NicknameValidate: function (event) {
            this.validation.nickname.valid = true;
            this.validation.nickname.errorMessage = [];
            if (this.userRegister.nickname.length < 1) {
                this.validation.nickname.errorMessage.push(this.$t('error.invalidMinLength', { count: 1 }));
                this.validation.nickname.valid = false;
            }
            if (this.userRegister.nickname.length > 10) {
                this.validation.nickname.errorMessage.push(this.$t('error.invalidMaxLength', { count: 10 }));
                this.validation.nickname.valid = false;
            }
            var nicknameRegExp = new RegExp('^[a-zA-Z][a-zA-Z0-9_-]{0,9}$');
            if (!nicknameRegExp.test(this.userRegister.nickname)) {
                this.validation.nickname.errorMessage.push(this.$t('error.invalidPattern'));
                this.validation.nickname.valid = false;
            }
        },
        /*
           pwd input keyup事件驗證方法
           長度6~20字元, 至少包含一個大寫英文一個小寫英文一個數字
        */
        PwdValidate: function (event) {
            this.validation.pwd.valid = true;
            this.validation.pwd.errorMessage = [];
            if (this.userRegister.pwd.length < 6) {
                this.validation.pwd.errorMessage.push(this.$t('error.invalidMinLength', { count: '6' }));
                this.validation.pwd.valid = false;
            }
            if (this.userRegister.pwd.length > 20) {
                this.validation.pwd.errorMessage.push(this.$t('error.invalidMaxLength', { count: '20' }));
                this.validation.pwd.valid = false;
            }
            var pwdRegExp = new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9-_]{6,30}$');
            if (!pwdRegExp.test(this.userRegister.pwd)) {
                this.validation.pwd.errorMessage.push(this.$t('error.invalidPattern'));
                this.validation.pwd.valid = false;
            }
        },
        Register: function() {
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
                        vm.$bus.$emit("errorEvent", { errorMessage: local.$t('error.' + result.statusCode), show: true });
                        vm.showdialog = true;
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