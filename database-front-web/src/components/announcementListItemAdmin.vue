<!-- 单个文章缩略图卡片 -->
<template>
  <div>
    <div class="announcement-item">
      <div class="announcement-item-inner">
        <div class="announcement-body">
          <!-- 管理员信息 -->
          <div class="admin-info">
            <userAvatar :userId=data.adminId :width=45 :addLink="false"></userAvatar>

            <div class="admin-name">{{ data.adminName }}</div>

            <el-divider direction="vertical"></el-divider>

            <div class="post-time">{{ data.announcementTime }}</div>

            <el-divider direction="vertical"></el-divider>

            <div class="tag">{{ "公告  " }}</div>

            <el-divider direction="vertical"></el-divider>

            <el-button type="primary" round class deleteButton size="small" @click="deleteAnnouncements">删除</el-button>

            <el-button type="primary" round class editButton size="small" @click="editAnnouncements">编辑</el-button>

            <el-divider direction="vertical"></el-divider>

            <div class="istop" v-if="data.isTop == 1">
              <el-button type="primary" round class editButton size="small" @click="cancelTop">取消置顶</el-button>
            </div>
            <div v-else>
              <el-button type="primary" round class editButton size="small" @click="executeTop">置顶</el-button>
            </div>
          </div>
          <!--公告标题与详细内容简介-->
          <div class="announcementTitle" @click="detailAnnouncements">{{ data.title }}</div>
          <!-- <router-link :to="`/announcementDetail/${data.announcementID}`" class="announcementTitle">{{ data.title }}</router-link> -->
          <div class="announcementContent">{{ data.summary }}</div>
        </div>
      </div>
    </div>
    <!-- START 修改公告弹窗 -->
    <el-dialog v-model="dialogVisible" title="对此公告进行修改" width="50%" :before-close="handleClose">
      <el-form @submit.native.prevent="confirmAnnouncement">
        <el-form-item label="标题：">
            <el-input type="textarea" v-model="form.title" />
        </el-form-item>
        <el-form-item label="内容：">
            <el-input type="textarea" v-model="form.announcementContent" />
        </el-form-item>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogVisible = false">Cancel</el-button>
          <el-button type="primary" @click="confirmAnnouncement">Confirm</el-button>
        </span>
      </template>
    </el-dialog>
    <!-- END 修改公告弹窗 -->

    <!-- START 公告详细界面弹窗 -->
    <el-dialog v-model="detaildialogVisible" :before-close="handleClose">
      <el-form>
        <!--管理员信息-->
        <div class="admin-info-detail">
          <userAvatar :userId=data.adminId :width=45 :addLink="false"></userAvatar> <!--管理员头像-->
          <div class="admin-name-detail">{{ data.adminName }}</div> <!--管理员昵称-->
          <el-divider direction="vertical"></el-divider> <!--分割线-->
          <div class="post-time-detail">{{ data.announcementTime }}</div> <!--发布时间-->
        </div>
      <!--公告详细信息-->
        <div class="announcementTitleDetail">{{ data.title }}</div>
        <div class="announcementContentDetail">{{ data.announcementContent }}</div>
      </el-form>

      <template #footer>
        <span class="dialog-footer">
          <el-button @click="detaildialogVisible = false">Close</el-button>
        </span>
      </template>
    </el-dialog>
    <!-- END 公告详细界面弹窗 -->


  </div>
</template>

<script setup>

import { deleteAnnouncement } from "@/api/announcement.js"
import { updateAnnouncement } from "@/api/announcement.js"
import { topAnnouncement } from "@/api/announcement.js"
import { ref, reactive, toRefs, onMounted,defineEmits} from 'vue';
import { useRouter } from 'vue-router'
import { useStore } from 'vuex' // 引入store
const store = useStore(); // 使用store必须加上
const emit = defineEmits(['child-click'])
// 接收父组件的信息
const props = defineProps({
  data: {
    type: Object
  },
  index: {
    type: Int16Array,
  },
});

//   置顶与取消置顶START
const executeTop = async() => {
  let result;
  const params = {
    announcementId: props.data.announcementID,
    istop: 1
  };
  result = await topAnnouncement(params);
  if(result.code==200){
    window.alert('success');
    emit('child-click',1)
  }
  else{
    window.alert('error');
  }
  location.reload();
};

const cancelTop = async() => {
  let result;
  const params = {
    announcementId: props.data.announcementID,
    istop: 0
  };
  result = await topAnnouncement(params);
  if(result.code==200){
    window.alert('success');
    emit('child-click',1)
  }
  else{
    window.alert('error');
  }
  location.reload();
};
//   置顶与取消置顶END


//   删除公告START
const deleteAnnouncements = async(AnnouncementId) => {
  let result;
  const params = {
    announcementId: props.data.announcementID
  };
  result = await deleteAnnouncement(params);
  if(result.code==200){
    window.alert('success');
    emit('child-click',1)
  }
  else{
    window.alert('error');
  }
  location.reload();
};
//   删除公告END


//   修改公告START
const dialogVisible = ref(false)

const form = ref({
    title: props.data.title,
    announcementContent: props.data.announcementContent,
});

const editAnnouncements = () => {
    dialogVisible.value = true;
}

const confirmAnnouncement = () => {
    let params = {
        announcementId: props.data.announcementID,
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

<style>
.announcement-item {
  padding: 5px 15px 0 15px;
}

.announcement-item-inner {
  border-bottom: 1px solid #ddd;
  padding: 10px 0px;
  display: flex;
}

.announcement-body {
  flex: 1;
}

.admin-info {
  display: flex;
  align-items: center;
  font-size: 18px;
  color: #4e5969;
}

.post-time {
  font-size: 18px;
  color: #9a9a9a;
}

.announcementTitle {
  font-weight: bold;
  text-decoration: none;
  color: #4a4a4a;
  font-size: 21px;
  margin: 10px 0px;
  display: inline-block;
}

.announcementTitle:hover{
  color:rgb(121, 182, 248)
}

.announcementContent{
  font-size: 18px;
  color: #858585;
}

.link-info {
  margin-left: 5px;
  color: #494949;
  text-decoration: none;
}

.link-info:hover {
  color: var(--link);
}

.announcement-info {
  margin-top: 10px;
  display: flex;
  align-items: center;
  font-size: 13px;

}

.announcement-item:hover {
  background: #fffbfb;
}


.announcementTitleDetail {
  font-weight: bold;
  text-decoration: none;
  color: #4a4a4a;
  font-size: 30px;
  margin: 10px 0px;
  display: flex;
  justify-content: center
}

.announcementContentDetail{
  color: #6e6e6e;
  font-size: 20px;
  white-space: pre-wrap;
}

.admin-info-detail{
  display: flex;
  align-items: center;
  font-size: 18px;
  color: #868686;
}

</style>