/*
  多語系設定
  預設值: 中文
  選項:
    1. 'tw': 中文 
    2. 'en': 英文
*/
var messages = {
    en: {
        "error": {
            "require": "{{itemName}} is reqired ",
            "invalidMinLength": "Length should more than {count}",
            "invalidMaxLength": "Length should less than {count}",
            "invalidPattern": "invalid pattern",
            "1": "Success",
            "0": "UserName Exist",
            "-1": "Invalid UserName",
            "-2": "Invalid Nickname",
            "-3": "Invalid Password",
            "-4": "UserName is not Exist",
            "-5": "Wrong Password",
            "-6": "Please Log In",
            "-7": "Expired, Please Log In Again",
            "-8": "Permission Deny",
            "-9": "Invalid Topic",
            "-10": "Invalid Content",
            "-11": "Post doesn't exist",
            "-12": "Reply content coul't be null",
            "-500": "Unknown Error Occured, Please Contact Admin."
        },
        "nav": {
            "post": "New Post",
            "login": "Log In",
            "logout": "Log Out",
            "langChoice": "language",
            "lang": {
                "zh-tw": "Chinese-Traditional",
                "en": "Englisg"
            }
        },
        "dialog": {
            "header": "System Information",
            "confirm": "Confirm"
        },
        "login": {
            "userName": "UserName",
            "pwd": "Password",
            "nickname": "Nickname",
            "login": "Login",
            "register": "Register",
            "cancel":"Cancel"
        },
        "posts": {
            "topic": "Topic",
            "postUserName": "Post User",
            "replyUserName": "Reply User",
            "replyCount": "Reply Count"
        },
        "create": {
            "topic": "Topic",
            "content": "Content",
            "createBtn": "Create Post",
            "cancelBtn": "Cancel"
        },
        "post": {
            "postAt": "Post At",
            "replyAt": "Reply At",
            "addReply": "Reply",
            "placeHolder": "Please Enter Content..."
        },
        "searchBar": {
            "search": "Search",
            "topic": "Topic",
            "nickname": "Nickname"
        }
    },
    tw: {
        "error": {
            "require": "{{itemName}} 為必填欄位 ",
            "invalidMinLength": "長度應大於{count}",
            "invalidMaxLength": "長度應小於{count}",
            "invalidPattern": "不符合規則",
            "1": "成功",
            "0": "使用者帳號已存在",
            "-1": "使用者帳號不符規範",
            "-2": "暱稱不符規範",
            "-3": "密碼不符規範",
            "-4": "使用者帳號不存在",
            "-5": "密碼錯誤",
            "-6": "請登入",
            "-7": "時效過期, 請重新登入",
            "-8": "權限不足",
            "-9": "標題不符規範",
            "-10": "文章內容不符規範",
            "-11": "文章不存在",
            "-12": "回覆內容不能為空",
            "-500": "發生錯誤，請聯繫管理員"
        },
        "nav": {
            "post": "新增貼文",
            "login": "登入",
            "logout": "登出",
            "langChoice": "語言",
            "lang": {
                "zh-tw": "繁體中文",
                "en": "英文"
            }
        },
        "dialog": {
            "header": "系統提示",
            "confirm": "確定"
        },
        "login": {
            "userName": "帳號",
            "pwd": "密碼",
            "nickname": "暱稱",
            "login": "登入",
            "register": "註冊",
            "cancel": "取消"
        },
        "posts": {
            "topic": "標題",
            "postUserName": "發文",
            "replyUserName": "回覆",
            "replyCount": "回覆數量"
        },
        "create": {
            "topic": "標題",
            "content": "內容",
            "createBtn": "發表文章",
            "cancelBtn": "取消"
        },
        "post": {
            "postAt": "發表於",
            "replyAt": "回覆於",
            "addReply": "回覆",
            "placeHolder": "請輸入回覆內容..."

        },
        "searchBar": {
            "search": "搜尋",
            "topic": "標題",
            "nickname": "暱稱"
        }
    }
}

var i18n = new VueI18n({
    locale: 'tw',
    messages
});