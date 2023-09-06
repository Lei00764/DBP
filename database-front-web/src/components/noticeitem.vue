<!-- 单个文章缩略图卡片 -->
<template>
    <div>
      <div class="menuItem button-item" @click="detailNotices">
        <el-button @click="handle">
          <div class="notice-item">
            <div class="notice-item-inner">
              <div class="notice-body">
                <div class="admin-info">
                  <userAvatar :userId=data.adminId :width=30 :addLink="false"></userAvatar>
  
                  <div class="user-info">{{ data.adminName }}</div>
  
                  <el-divider direction="vertical"></el-divider>
  
                  <div class="summary">{{ data.summary }}</div>
                </div>
                <!--<router-link :to="`/announcementDetail/${data.announcementID}`" class="announcementTitle">{{ data.title }}</router-link>
                <div class="announcementContent">{{ data.announcementContent }}</div>-->
              </div>
            </div>
          </div>
        </el-button>
      </div>
      <!-- START 消息详细界面弹窗 -->
          <el-dialog v-model="detaildialogVisible" :before-close="handleClose">
            <el-form>
                <!--管理员信息-->
                <div class="admin-info-detail">
                    <userAvatar :userId=data.adminId :width=45 :addLink="false"></userAvatar> <!--管理员头像-->
                    <div class="admin-name-detail">{{ data.adminName }}</div> <!--管理员昵称-->
                    <el-divider direction="vertical"></el-divider> <!--分割线-->
                    <div class="post-time-detail">{{ data.noticeTime }}</div> <!--发布时间-->
                </div>
                <!--公告详细信息-->
                <div class="noticeTitleDetail">{{ "管理员信息" }}</div>
                <div class="noticeContentDetail">{{ data.noticeContent }}</div>
            </el-form>

        <template #footer>
            <span class="dialog-footer">
                <el-button @click="detaildialogVisible = false">Close</el-button>
            </span>
        </template>
        </el-dialog>
        <!-- END 消息详细界面弹窗 -->
    </div>
  </template>
  
  <script setup>
  
  import { useStore } from 'vuex' // 引入store
  import { ref, reactive, toRefs, onMounted,defineEmits} from 'vue';
  const store = useStore(); // 使用store必须加上
  // 接收父组件的信息
  const props = defineProps({
    data: {
      type: Object
    },
  });



//   消息详细内容START
const detaildialogVisible = ref(false)

const detailNotices = () => {
    detaildialogVisible.value = true;
}
//   消息详细内容END
  
</script>
  
<style>

.notice-item-inner {
  border-bottom: 1px solid #ddd;
  background-color: #ffffff;
  padding: 0px 20px 0px 10px;
  display: flex;
  z-index: 3; /* 将通知项的层级设置为较高的值 */
}

.notice-body {
  flex: 1;
}

.admin-info {
  display: flex;
  align-items: center;
  font-size: 14px;
  color: black;
  z-index: 3; /* 将通知项的层级设置为较高的值 */
}


.link-info {
  margin-left: 5px;
  color: #494949;
  text-decoration: none;
}

.link-info:hover {
  color: var(--link);
}

.notice-info {
  margin-top: 10px;
  display: flex;
  align-items: center;
  font-size: 13px;
}

.notice-item:hover {
  background: #fffdfb;
}

.admin-info-detail{
  display: flex;
  align-items: center;
  font-size: 16px;
  color: #868686;
}

.noticeContentDetail{
  font-size: 18px;
  white-space: pre-wrap;
}

.noticeTitleDetail {
  font-weight: bold;
  text-decoration: none;
  color: #4a4a4a;
  font-size: 26px;
  margin: 10px 0px;
  display: flex;
  justify-content: center
}
</style>