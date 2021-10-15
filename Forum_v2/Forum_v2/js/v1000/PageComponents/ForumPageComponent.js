/*
    討論區首頁
*/
var ForumPageComponent = {
    template:
        `<div>` +
        `<navBar></navBar>` +
        `<div class="container">` +
        //search
        `<div class="search-container">
            <ul>
              <li>
                <input
                  type="text"
                  :placeholder="$t('searchBar.search')"
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
                <button class="searchButton" @click= "SearchFilter">{{$t('searchBar.search')}}</button>
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
                    <a @click="ToPost(item.postid)">
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
        //pagination
        `<pagination
            :length="metaData.length"
            :currentIndex="metaData.currentIndex"
            :pageSize="metaData.pageSize"
            :pageLength="metaData.pageLength"
            @indexChange="DoSearch"
         >
         </pagination>`+
        `</div>` +
        `</div>`
    ,
    components: {
        'navBar': NavBarComponent,
        'pagination': Pagination
    },
    data: function () {
        return {
            posts: [],
            text: '',
            selected: 'keyTopic',
            searchKey: new Map(),
            metaData: {
                length: 0,
                currentIndex: 0,
                pageSize: 10,
                pageLength: 1
            }
        }
    },
    methods: {
        DoSearch: function (event) {
            this.SendSearch(this.searchKey.get('keyTopic'), this.searchKey.get('keyNickname'), event.currentIndex, event.pageSize);
        },
        ToPost: function(postid) {
            this.searchKey.set('keyTopic', '');
            this.searchKey.set('keyNickname', '');
            this.searchKey.set(this.selected, this.text);
            $.router.set({ route: '/post/'+ postid, queryString: 'searchField=' + this.selected + '&key=' + this.text });
        },
        SearchFilter: function() {
            this.searchKey.set('keyTopic', '');
            this.searchKey.set('keyNickname', '');
            this.searchKey.set(this.selected, this.text);
            this.SendSearch(this.searchKey.get('keyTopic'), this.searchKey.get('keyNickname'), this.metaData.currentIndex, this.metaData.pageSize);
        },
        SendSearch: function(topic, nickname, index, size) {
            var datas = this;
            var postData = {
                topic: topic,
                nickname: nickname,
                index: index,
                size: size
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
                    datas.metaData.length = result.returnData.metaData.length;
                    datas.metaData.currentIndex = result.returnData.metaData.currentIndex;
                    datas.metaData.pageSize = result.returnData.metaData.pageSize;
                    datas.metaData.pageLength = Math.floor(datas.metaData.length / datas.metaData.pageSize) + 1;
                }
            });
        }
    },
    created: function() {
        this.searchKey.set('keyTopic', '');
        this.searchKey.set('keyNickname', '');
        this.SendSearch("", "", 0, 10);
    }
};