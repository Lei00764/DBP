<template>
    <div>
        <!-- 背景布局 -->
        <div class="background1">
            <div class="background2">
            </div>
        </div>   
        <!-- 侧边栏展示 -->
        <div class="PersonSide">
                <el-form-item>
                        <el-button class="PersonSide_img" type="primary">
                        </el-button>
                </el-form-item>
            <div class="PersonSide_text">
                <div class="user_name">
                        昵称                      
                    <span> {{ nickname }} </span>
                </div>
                <div class="user_text">
                    个人描述                    
                </div>  
            </div>
                <div class="Line">
            </div>
            <div class="information">
                <div class="fans">
                    Fans:{{ props.fans_num }}       
                </div>
                <div class="following">
                    Following:{{ props.following_num }}       
                </div>
                <div class="content">
                    Content:{{ props.content_num }}      
                </div>
                <div class="rectangle_point">  
                    <div class="point">
                    积分：      
                    </div>     
                </div>
            </div>            
        </div>
        <!-- 顶部的很好的 -->
        <div class="topp">
            <el-form>
                <el-form-item>
                    <el-input   
                        v-model="formData.keyword" 
                        clearable
                        placehoder="请输入内容" 
                        >
                    </el-input>
                </el-form-item>
                    <el-button class=button11 round @click="home"
                    style="color:#000000;background-color:transparent;margin-top: 2px;">
                        <el-icon :size="20"><House />
                        </el-icon>
                    </el-button>
                    <el-button class=button12 round color= transparent @click="stars"
                    style="color:#000000;background-color:transparent;margin-top: 2px;">
                        <el-icon :size="20"><Star />
                        </el-icon>
                    </el-button>
                    <el-button class=button13 round color= transparent @click="user"
                    style="color:#000000;background-color:transparent;margin-top: 2px;">
                        <el-icon :size="20"><User />
                        </el-icon>
                    </el-button>
                    
                <div class="button2">
                    &emsp;Login
                </div>
                <div class="button3">
                    &emsp;swicth account
                </div>
            </el-form>
        </div>      

    </div>
</template>
  
<script setup="props">
import { ref, reactive,toRefs,onMounted } from 'vue';
import { userLogin } from '@/api/user';
import Message from "@/utils/Message.js"
import { House,Star,User} from '@element-plus/icons-vue'
import { ElPagination } from 'element-plus' 
import { useRouter } from 'vue-router'
const router = useRouter()
const formData = reactive({
    keyword: '',
});
const props = defineProps({
    title: {
        type:String, // 可以设置传来值的类型
        default:""
    },
    fans_num:{
        type:Int16Array,
        default:0
    },
    following_num:{
        type:Int16Array,
        default:0
    },
    content_num:{
        type:Int16Array,
        default:0
    }
})
const home = () => {
    router.push({ path: 'homeUser'})
};
const stars=()=>{
    
};
const user=()=>{
    router.push({ path: '/userHomePage/user'})
};
</script>
  
<style scoped>
/* 初始化 */
*{
   margin: 0px;
   padding: 0px;
   box-sizing: border-box;
   font-family: sans-serif;
}
.background1 {
    position: absolute;
    width: 1230px;
    height: 540px;
    left: 0;
    top: 0;
    bottom: 340px;
    background: linear-gradient(180.00deg, rgba(224, 248, 242, 0.9),rgba(224, 248, 242, 0) 100%);
}

.background2 {
/* Rectangle 4 */
position: absolute;
left: 0;
width: 260px;
height: 800px;
/* White */
background: rgb(255, 255, 255);
/* Elevation / 06 */
box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.1);
}
/* 个人侧边栏 */
.PersonSide {
    position: absolute;
    width: 260px;
    height: 800px;
}
/* 头像 */
.PersonSide_img {
  width: 60px;
  height: 60px;
  background:url('@/assets/head.jpg');
  background-size: cover;
  background-position: center;
  background-color: transparent;
  margin-right: 3%;
  margin-left: 5%;
  border-radius: 20px;
}
.PersonSide_img.hover{
    opacity: 0.2;
}
/* 昵称 */
.PersonSide_text {
  /* Elaine-GIFT */
    position: absolute;
    width: 252px;
    height: 47px;
    left: 100px;
    right: 84px;
    top: 10px;
    color: rgb(0, 0, 0);
    font-family: Cabin;
    font-size: 20px;
    text-align: left;
}
/* 信息 */
.user_text {
  /* Elaine-GIFT */
    color: rgb(98, 98, 98);
    font-size: 8px;
}
.Line {
  /* 直线 1 */
    position: absolute;
    width: 230px;
    left:10px;
    height: 0;
    top: 100px;
    border: 1px solid rgb(148, 141, 141);
    border-radius: 20px;
    transform: rotate(-0.58deg);
}
/* 信息 */
.information {
  /* Elaine-GIFT */
    top: 160px;
    left:40px;
    position: absolute;
    color: rgb(0, 0, 0);
    font-family: Georgia, serif;
    font-size: 18px;
    text-align: left;
}
.content {
    position: absolute;
    top: 80px;
}
.following {
    position: absolute;
    top: 160px;
}
/* 积分 */
.rectangle_point{
position: absolute;
width: 220px;
height: 111px;
left:-30px;
top: 280px;
background: rgba(224, 248, 242, 0.9);
border-radius:
27px;
}
/* 积分 */
.point{
/* 积分：769 */
position: absolute;
left: 30px;
top: 45px;
color: rgb(8, 102, 75);
font-family: Noto Sans SC;
font-size: 20px;
letter-spacing: 0px;
display: flex;
}
/* 用来设计输入框 */
.topp{
    position: absolute;
    left: 300px;
    top: 45px;
    width: 800px; 
}
.el-input{
    position: absolute;
    width: 400px;    /*调整整个组件的宽度*/
    height:30px;
}
:deep().el-input__wrapper{
        background:rgb(8,102,75);
        border:0;
        border-radius:10px;
}
:deep().el-input__inner{
    font-size: 14px;
    font-family: PingFangSC-Regular, PingFang SC;
    color: #ffffff;
    
}
/* el-button */
.button11{
    position: absolute;
    left: 410px;
    top: -18px;
    
}
.button12{
    position: absolute;
    left: 480px;
    top: -18px;
    
}
.button13{
    position: absolute;
    left: 440px;
    top: -18px;
    
}
/* login */
.button2 {
    border-radius: 8px;
    font-size: 12px;
    background-color: #ffffff;
    border: 2px solid rgb(46, 47, 53);
    position: absolute;
    width: 60px;
    height: 27px;
    left: 620px;
    top: -14px;
    /* 盒子阴影的样式 */
    box-shadow: 2px 2px 0px rgb(46, 47, 53);
}

    /* 按钮被点击时将阴影切换 */
.button2:active {
    box-shadow: 3px 3px 3px inset rgb(99, 99, 99),
        -6px 6px 8px inset rgba(255, 255, 255, 0.6);

}
/* switch account */
.button3 {
    border-radius: 8px;
    font-size: 12px;
    color:#ffffff;
    background: rgb(8, 102, 75);
    border: 2px solid rgb(46, 47, 53);
    position: absolute;
    width: 110px;
    height: 27px;
    left: 700px;
    top: -14px;
    /* 盒子阴影的样式 */
    box-shadow: 2px 2px 0px rgb(46, 47, 53);
}

    /* 按钮被点击时将阴影切换 */
.button3:active {
    box-shadow: 3px 3px 0px inset rgb(1, 18, 0),
        -3px 3px 0px inset rgba(0, 0, 0, 0.1);

} 

</style>
  