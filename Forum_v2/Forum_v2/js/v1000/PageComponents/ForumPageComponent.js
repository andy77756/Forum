/*
    討論區首頁
*/
var ForumPageComponent = Vue.extend({
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
                    <a @click="toPost(item.postid)">
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
        `</div>` +
        `</div>`
    ,
    data: function () {
        return {
            posts: [],
            text: '',
            selected: 'keyTopic',
            searchKey: new Map()
        }
    },
    components: {
        'nav-bar': NavBarComponent
    },
    methods: {
        toPost: function(postid) {
            this.searchKey.set('keyTopic', '');
            this.searchKey.set('keyNickname', '');
            this.searchKey.set(this.selected, this.text);
            $.router.set({ route: '/post/'+ postid, queryString: 'searchField=' + this.selected + '&key=' + this.text });
        },
        searchFilter: function() {
            this.searchKey.set('keyTopic', '');
            this.searchKey.set('keyNickname', '');
            this.searchKey.set(this.selected, this.text);
            this.sendSearch(this.searchKey.get('keyTopic'), this.searchKey.get('keyNickname'), 0, 5);
        },
        sendSearch: function(topic, nickname, index, size) {
            var datas = this;
            var postData = {
                topic: topic,
                nickname: nickname,
                index: 0,
                size: 5
            };
            $.ajax({
                type: "POST",
                url: "/ajax/ForumService.aspx/GetPosts?" + 'topic=' + topic + '&nickname=' + nickname + '&index=' + index + '&size=' + size,
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
    created: function() {
        this.searchKey.set('keyTopic', '');
        this.searchKey.set('keyNickname', '');
        this.sendSearch("", "", 0, 10);
    }
});