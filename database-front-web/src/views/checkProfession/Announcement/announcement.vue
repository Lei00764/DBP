<template>
    <el-form style="position: absolute;top:13%;left:28%">
        <el-form-item v-for="(item, index) in list" :key="index">
            <el-card :class='index==currentCard?"article-list-active":"article-list-inactive"' @click.native="goArticle(index)">
                <!-- 下面组件的v-if都不能省略，否则刷新页面会出错 -->
                <userAvatar :userId=item.adminId :width=45 :addLink="list[index]"
                    style="position: absolute;top:15%;left:3%;"></userAvatar>
                <div style="position: absolute;top:60%;left:3%;text-align:center;width:45px;">{{ item.adminName }}</div>
                <div style="position: absolute;top:70%;left:15%;font-size: 12px; color: #9a9a9a;">{{ item.announcementTime }}</div>
                <b v-if="list[index]"
                style="position: absolute;top:10%;left:15%;font-size: 18px;color:rgb(61, 61, 61)">
                        {{ list[index].title }}
                </b>
                <b style="position: absolute;top:40%;left:15%;font-size: 14px;color:rgb(120, 120, 120)">
                    {{ item.summary }}
                </b>

                <el-button class="deleteButton" type="primary" round size="small" @click.stop="deleteAnnouncements(item.announcementID)"
                    style="position:absolute;top:13%;right:3%;">删除</el-button>

                <el-button class="editButton" type="primary" round size="small" @click.stop="editAnnouncements"
                    style="position:absolute;top:13%;right:15%;">编辑</el-button>

                <div class="istop" v-if="item.isTop == 1">
                  <el-button class="cancelTopButton" type="primary" round size="small" @click.stop="cancelTop(item)"
                    style="position:absolute;top:13%;right:27%;">取消置顶</el-button>
                </div>
                <div v-else>
                  <el-button class="topButton" type="primary" round size="small" @click.stop="executeTop(item)"
                  style="position:absolute;top:13%;right:27%;">置顶</el-button>
                </div>
            </el-card>
        </el-form-item>
    </el-form>

    <!-- START 修改公告弹窗 -->
    <el-card class="card" v-show="card_show" v-if="list[currentCard]">
      <el-form @submit.native.prevent="confirmAnnouncement">
        <el-form-item label="标题：">
          <el-input type="textarea" v-model="list[currentCard].title"  />
        </el-form-item>
        <el-form-item label="内容：">
          <el-input  type="textarea" v-model="list[currentCard].announcementContent" />
        </el-form-item>
      </el-form>
        <span class="dialog-footer">
          <el-button @click="goArticle">取消并关闭</el-button>
          <el-button type="primary" class="editButton" @click="confirmAnnouncement">保存</el-button>
          <el-button type="primary" class="deleteButton" @click="card_show = false">删除</el-button>
          <el-button v-if="list[currentCard].isTop == 1" type="primary" class="cancelTopButton" 
              @click="card_show = false">取消置顶</el-button>
          <el-button v-else type="primary" class="topButton" 
              @click="card_show = false">置顶</el-button>
        </span>
    </el-card>
    <!-- END 修改公告弹窗 -->

    <!-- <el-card class="card" v-show="card_show" v-if="list[currentCard]">
        <b style="position:absolute;top:8%;left:5%;">举报原因</b>
        <div class="blank" style="top:6%;left:20%;">
            <div style="position:absolute;top:28%;left:5%">{{ list[currentCard].reason }} </div>
        </div>

        <b style="position:absolute;top:20%;left:5%;">帖子标题</b>
        <div class="blank" style="top:18%;left:20%;">
            <div style="position:absolute;top:28%;left:5%">{{ Info[currentCard].title }} </div>
        </div>

        <b style="position:absolute;top:32%;left:5%;">帖子内容</b>
        <div class="blank" style="top:30%;left:20%;">
            <div style="position:absolute;top:28%;left:5%">{{ Info[currentCard].content }} </div>
        </div>

                <el-button class="pass_btn" style="position: absolute;bottom:5%;left:42%;"
                    @click="pass(list[currentCard].reportId)">
                    <el-icon>
                        <Check />
                    </el-icon>
                </el-button>
                <el-button class="close_btn" style="position: absolute;bottom:5%;left:52%;"
                    @click="decline(list[currentCard].reportId)">
                    <el-icon>
                        <Close />
                    </el-icon>
                </el-button>
            </el-card> -->
</template>

<script setup>
import { ref, reactive, onMounted, watch } from 'vue';
// import { useRouter } from 'vue-router';

import navTop from "@/components/navTop.vue"
import announcementListItem from "@/components/announcementListItem.vue"
import { loadAnnouncement, updateAnnouncement } from "@/api/announcement.js"
import { deleteAnnouncement, topAnnouncement } from "@/api/announcement.js"
//import { forum_searchArticle } from "@/api/article.js"
import router from "@/router/index.js"
import { useStore } from 'vuex' // 引入store
import { postAnnouncement } from "@/api/announcement.js"

const store = useStore();//使用store必须加上

const card_show = ref(false);//用以点击进入申请信息的详情界面
const currentCard = ref(-1);//用来记录当前显示的资料卡片（index）
const list = ref([]); // 定义并初始化 list 变量

const GetList = () => {
    //将获取列表信息的接口封装在函数中
    loadAnnouncement()
        .then(function (result) {
            afterGet(result);
        })
        .catch(function (error) {
            console.log(error);
        });
}

const afterGet = async (request) => {
    list.value = request.data;//申请信息放入list中
}
GetList();//获取列表，给list赋值

const goArticle = (index) => {
    //进入文章详情页面（卡片）
    if (index == currentCard.value) {
        card_show.value = !card_show.value;
        currentCard.value = -1;
    }
    else {
        card_show.value = true;
        currentCard.value = index;
    }
}

//   置顶与取消置顶START
const executeTop = async (announcement) => {
  let result;
  const params = {
    announcementId: announcement.announcementID,
    istop: 1
  };
  result = await topAnnouncement(params);
  if (result.code == 200) {
    window.alert('success');
  }
  else {
    window.alert('error');
  }
  location.reload();
};

const cancelTop = async (announcement) => {
  let result;
  const params = {
    announcementId: announcement.announcementID,
    istop: 0
  };
  result = await topAnnouncement(params);
  if (result.code == 200) {
    window.alert('success');
  }
  else {
    window.alert('error');
  }
  location.reload();
};
//   置顶与取消置顶END

//   删除公告START
const deleteAnnouncements = async (AnnouncementId) => {
  let result;
  const params = {
    announcementId: AnnouncementId
  };
  result = await deleteAnnouncement(params);
  if (result.code == 200) {
    window.alert('success');
  }
  else {
    window.alert('error');
  }
  location.reload();
};
//   删除公告END

//   修改公告START
 const form = ref({
   title: "",
   announcementContent: "",
 });

 const editAnnouncements = () => {
    card_show.value = true;
 }

 const confirmAnnouncement = (announcement) => {
   let params = {
    announcementId: announcement.announcementID,
    title: form.value.title,
    content: form.value.announcementContent,
   }
   //console.log(params);
   updateAnnouncement(params);
      dialogVisible.value = false;
  location.reload();
 };
//   修改公告END

//   公告详细内容START
const detaildialogVisible = ref(false)

const detailAnnouncements = () => {
  detaildialogVisible.value = true;
}
//   公告详细内容END

</script>

<style scoped>
.checkArticle-page {
    background-image: url('@/assets/check_article.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 107vh;
    width: 100vw;
}

.article-list-inactive {
    background-color:#E7F9FD;
    width: 500px;
    height: 100px;
    box-shadow: 0px 4px 4px 0px gray;
    border-radius: 10px;
}

.article-list-active {
    background-color:#CCFFCA;
    width: 500px;
    height: 100px;
    box-shadow: 0px 4px 4px 0px gray;
    border-radius: 10px;
}

.pass_btn {
    width: 35px;
    height: 35px;
    border-radius: 50px;
    background-color: rgb(255, 255, 255);
    color: rgb(45, 45, 45);
    border-color: transparent;
    box-shadow: 0px 4px 4px 0px gray;
}
.close_btn {
    width: 35px;
    height: 35px;
    border-radius: 50px;
    background-color: rgb(32, 32, 32);
    color: rgb(255, 255, 255);
    border-color: transparent;
    box-shadow: 0px 4px 4px 0px gray;
}
.blank {
    position: absolute;
    font-size: 16px;
    background-color:rgb(255, 255, 255);
    width: 350px;
    height: 50px;
    border-radius: 10px;
}
.card {
    position: fixed;
    top: 13%;
    right: 3%;
    background-color: #CCFFCA;
    width: 480px;
    height: 700px;
    box-shadow: 4px 4px 4px 2px gray;
    border-radius: 10px;
}

.icon {
    font-size: 25px;
    position: absolute;
    top: 7%;
    left: 3%
}

.deleteButton {
  background-color: black;
  border-color: transparent;

  border-radius: 10px;
}

.editButton {
  background-color: white;
  color:black;
  border-color: transparent;
  border-radius: 10px;
}

.topButton {
  background-color: #ffd500;
  border-color: transparent;
  border-radius: 10px;
}

.cancelTopButton {
  background-color: #ffaa5f;
  border-color: transparent;
  border-radius: 10px;
}

.dialog-footer{
    position: absolute;
    bottom:5%;
    left:14%;
}
</style>