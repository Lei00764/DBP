<template>
    <div>
        <div class="header">
                <navTopAdmin></navTopAdmin>
            </div>
        <div class="profession-page">
            <div class="homeAdmin-card">
                <!-- 管理员信息卡片 -->
                <el-form :inline=true class="homeAdmin-avatar-ID">
                    <el-form-item>
                        <!-- 头像 -->
                        <el-avatar :size="80" :src="store.state.Info.avatar"></el-avatar>
                    </el-form-item>
                    <el-form-item>
                        <el-form>
                            <!-- 用户名与ID -->
                            <h2>{{ store.state.Info.name }}</h2>
                            <div>ID &nbsp;&nbsp;{{ store.state.Info.id }}</div>
                        </el-form>
                    </el-form-item>
                    <div class="line"></div>
                    <!-- 这是一条分割线 -->
                </el-form>
                <el-form :inline=true >
                    <!-- 菜单选择器 -->
                    <div class="choose">&nbsp;</div>
                    <el-form-item style="width:100px;position:absolute;top:25%;left:20%;" @click="intoProfession">
                        <b style="color:black;pointer-events: none">审核专业认证</b>
                    </el-form-item>
                </el-form>
                <el-form :inline=true style="position:absolute;top:35%;left:20%;" @click="intoCheck">
                    <el-form-item style="width:100px;">
                        <b style="color:black;pointer-events: none">审核帖子</b>
                    </el-form-item>
                </el-form>
                <el-form :inline=true style="position:absolute;top:45%;left:20%;"  @click="Announcement">
                    <el-form-item style="width:100px;">
                        <b style="color:black;pointer-events: none">公告</b>
                    </el-form-item>
                </el-form>
                <el-form :inline=true style="position:absolute;top:55%;left:20%;" @click="intoComment">
                    <el-form-item style="width:100px;pointer-events: none">
                        <b style="color:black;">审核留言</b>
                    </el-form-item>
                </el-form>
            </div>
        </div>
            <div v-if="choice==1">
                <profession></profession>
            </div>
            <div v-else-if="choice==2">
                <checkarticle></checkarticle>
            </div>
            <div v-else-if="choice==4">
                <checkcomment></checkcomment>
            </div>
            <div v-else-if="choice==3">
                <announcement></announcement>
            </div>
        
    </div>
</template>

<script setup>
import { ref,reactive } from 'vue';
import router from "@/router/index.js"
import navTopAdmin from "@/components/navTopAdmin.vue"
import profession from "@/views/checkProfession/Profession/profession.vue"
import checkarticle from "@/views/checkProfession/Article/checkarticle.vue"
import checkcomment from "@/views/checkProfession/Comment/checkcomment.vue"
import announcement from "@/views/checkProfession/Announcement/announcement.vue"
import { useStore } from 'vuex'//引入store
import { useRoute } from 'vue-router'

const store = useStore();//使用store必须加上
const route = useRoute();
const choice = ref(route.params.choice);
const distance = ref((23 + (choice.value*1-1)*10)+"%");

const intoProfession = () => {
    distance.value = '23%';
    choice.value = 1;
    router.push({ name: 'work', params:{choice:'1'} })
}

const intoCheck = () => {
    distance.value = '33%';
    choice.value = 2;
    router.push({ name: 'work', params:{choice:'2'} })
}

const Announcement = () => {
    distance.value = '43%';
    choice.value = 3;
    router.push({ name: 'work', params:{choice:'3'} })
}

const intoComment = () => {
    distance.value = '53%';
    choice.value = 4;
    router.push({ name: 'work', params:{choice:'4'} })
}


</script>

<style scoped>
.profession-page {
    background-image: url('@/assets/Admin-background-blank.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 98vh;
    width: 99vw;
    position:fixed;
}
.homeAdmin-avatar-ID {
    /* 头像、昵称和ID在卡片上的排布 */
    position: absolute;
    top: 3.5%;
    left: 10%;
}
.header {
    position: absolute;
    width: 100%;
}
.line {
    /* 横线 */
    float: right;
    width: 300px;
    height: 2px;
    background: #888888;
    position: absolute;
}
.homeAdmin-card {
    /* 管理员资料卡片的设置 */
    position: fixed;
    top: 11%;
    left: 0.6%;
    width: 342px;
    height: 700px;
    background-color: #E7FAFE;
}
.choose{
    background-color: #ffffff;
    width:370px;
    position:absolute;
    top: v-bind(distance) ;
    height:60px;
}
</style>
