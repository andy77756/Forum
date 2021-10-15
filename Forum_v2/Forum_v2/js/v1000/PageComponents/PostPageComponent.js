/*
    回覆文章頁面
*/

var PostPageComponent = {
    template:
        `<div>` +
        `<navBar></navBar>` +
        `<div class="container">` +
        `<button class='btn btn-primary' @click="Back">返回</button>` +
        //pagination
        `<pagination
            :length="metaData.length"
            :currentIndex="metaData.currentIndex"
            :pageSize="metaData.pageSize"
            :pageLength="metaData.pageLength"
            @indexChange="DoSearch"
         >
         </pagination>`+
        //文章內容及回覆列表
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
        //回覆表單
        `<div class="reply-edit">
            <form>
                <fieldset class="form-group">
                    <textarea class="form-control" name="content" :disabled="level >= 1 ? false : true" v-model="reply.content" :placeholder="$t('post.placeHolder')">
                  </textarea>
                </fieldset>
                <button type="button" class="btn btn-primary" :disabled="!reply.content" @click="AddReply">{{$t('post.addReply')}}</button>
            </form>
        </div>`+
        `</div>` +
        `</div>`
    ,
    components: {
        'navBar': NavBarComponent,
        'pagination': Pagination
    },
    data: function () {
        return {
            queryString: '',
            postId: 0,
            level: 0,
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
            queryParams: {
                key: '',
                searchField: '',
                pageIndex: 0,
                pageSize: 10
            },
            metaData: {
                length: 0,
                currentIndex: 0,
                pageSize: 10,
                pageLength: 1
            }
        }
    }, 
    methods: {
        DoSearch: function(event) {
            this.SendSearch(this.postId, event.currentIndex, event.pageSize);
        },
        AddReply: function() {
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
                        vm.$bus.$emit("errorEvent", { errorMessage: local.$t('error.' + result.statusCode), show: true });
                    } else {
                        local.replies.push(result.returnData);
                        local.reply.content = '';
                    }
                }
            });
        },
        SendSearch: function(postid, index, size) {
            var datas = this;
            var postData = {
                id: postid,
                index: index,
                size: size
            };
            $.ajax({
                type: "POST",
                url: "/ajax/ForumService.aspx/GetPostById?id=" + postid + '&index=' + index + '&size=' + size,
                data: JSON.stringify(postData),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = JSON.parse(response.d);
                    datas.post = result.returnData.post;
                    datas.replies = result.returnData.replies;
                    datas.metaData.length = result.returnData.metaData.length;
                    datas.metaData.currentIndex = result.returnData.metaData.currentIndex;
                    datas.metaData.pageSize = result.returnData.metaData.pageSize;
                    datas.metaData.pageLength = Math.floor(datas.metaData.length / datas.metaData.pageSize) + 1;
                }
            });
        },
        Back: function() {
            $.router.set('/Forum'+ this.queryString);
        }
    },
    created: function() {
        if (localStorage.getItem('userInfo') != null) {
            var userInfo = JSON.parse(!!localStorage.getItem("userInfo").toString() ? localStorage.getItem("userInfo").toString() : '{}');
            this.reply.userId = userInfo.userId;
            this.reply.token = userInfo.token;
            this.level = userInfo.level;
        }
        this.postId = +(window.location.pathname.split('/')[2]);
        this.reply.postId = +(window.location.pathname.split('/')[2]);
        this.queryString = window.location.search;
        this.SendSearch(this.postId, this.metaData.currentIndex, this.metaData.pageSize);
    }
};