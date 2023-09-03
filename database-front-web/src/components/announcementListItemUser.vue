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
  
            </div>
            <div class="announcementTitle" @click="detailAnnouncements">{{ data.title }}</div>
            <div class="announcementContent">{{ data.summary }}</div>
          </div>
        </div>
      </div>
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
import { ref, reactive, toRefs, onMounted,defineEmits} from 'vue';
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
  

  const detaildialogVisible = ref(false)

  const detailAnnouncements = () => {
    detaildialogVisible.value = true;
  }
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
}

.admin-info-detail{
  display: flex;
  align-items: center;
  font-size: 18px;
  color: #868686;
}
  </style>