<!-- 单个文章缩略图卡片 -->
<template>
  <div>
    <div class="announcement-item">
      <!--  -->
      <div class="announcement-item-inner">
        <div class="announcement-body">
          <!-- 管理员信息 -->
          <div class="admin-info">
            <router-link :to="`/layout`">{{ data.adminName }}</router-link>
            <div class="post-time">
              <span>
                {{ data.announcementTime }}
              </span>
            </div>
          </div>
          <div class="content">
            <router-link :to="`/announcementDetail/${data.announcementID}`">{{ data.announcementContent }}</router-link>
          </div>
        </div>
      </div>
    <!--  -->
    </div>
  </div>
</template>

<script setup>

// 接收父组件的信息
const props = defineProps({
  data: {
    type: Object
  },
});

// console.log(props.data);
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
  font-size: 14px;
  color: #4e5969;
}

.link-info {
  margin-left: 5px;
  color: #494949;
  text-decoration: none;
}

.link-info:hover {
  color: var(--link);
}

.post-time {
  font-size: 13px;
  color: #9a9a9a;
}

.content {
  font-weight: bold;
  text-decoration: none;
  color: #4a4a4a;
  font-size: 16px;
  margin: 10px 0px;
  display: inline-block;
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
</style>