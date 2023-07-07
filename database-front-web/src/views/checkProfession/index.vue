<template>
    <div>
        <div class="profession-page">
            <el-form style="position: absolute;top:20%;left:7%">
                <el-column v-for="item in list">
                    <el-form-item>
                        <!-- 每条申请对应一条卡片 -->
                        <el-card class="profession-list" @click="intoCard">
                            <el-avatar :size="70" :src="avatarURL" style="position: absolute;top:15%;left:5%"></el-avatar>
                            <b style="position: absolute;top:20%;left:25%;font-size: 18px;color:rgb(61, 61, 61)">
                                用户名：{{ item.name }}
                            </b>
                            <b style="position: absolute;top:55%;left:25%;font-size: 14px;color:rgb(120, 120, 120)">
                                ID：{{ item.UserID }}
                            </b>
                            <el-button class="pass_btn" style="position:absolute;top:35%;left:78%;" @click="pass">
                                <el-icon>
                                    <Check />
                                </el-icon>
                            </el-button>
                            <el-button class="close_btn" style="position: absolute;top:35%;left:85%;">
                                <el-icon>
                                    <Close />
                                </el-icon>
                            </el-button>
                        </el-card>
                    </el-form-item>
                </el-column>
            </el-form>
            <card class="card">
                <!-- 资料信息的详细显示 -->
                <el-avatar :size="90" :src="avatarURL" style="position: absolute;top:8%;left:7%"></el-avatar>
                <b style="position: absolute;top:10%;left:25%;font-size: 20px;color:rgb(61, 61, 61)">
                    用户名：
                </b>
                <b style="position: absolute;top:16%;left:25%;font-size: 16px;color:rgb(120, 120, 120)">
                    ID：
                </b>
                <b style="position: absolute;top:30%;left:10%;font-size: 20px;color:rgb(61, 61, 61)">
                    邮箱：
                </b>
                <b style="position: absolute;top:40%;left:10%;font-size: 20px;color:rgb(61, 61, 61)">
                    手机号：
                </b>
                <b style="position: absolute;top:50%;left:10%;font-size: 20px;color:rgb(61, 61, 61)">
                    上传资料：
                </b>
                <el-button class="pass_btn" style="position: absolute;bottom:5%;left:45%;" @click="pass">
                    <el-icon>
                        <Check />
                    </el-icon>
                </el-button>
                <el-button class="close_btn" style="position: absolute;bottom:5%;left:55%;">
                    <el-icon>
                        <Close />
                    </el-icon>
                </el-button>
            </card>
            <el-icon size="30px" style="position:absolute;top: 8%;left:3%" @click="backToHome">
                <Back />
            </el-icon>
        </div>
    </div>
</template>
  
<script setup>
import { ref, reactive } from 'vue';
import router from "@/router/index.js"
const intoCard = () =>{
    //进入卡片
}
const pass = () =>{
    //审核通过
}
const backToHome = () =>{
    router.push({ name: 'homeAdmin' })
}
let list=[{
        UserID:2151133,
        name:"Q"
    },
    {
        UserID:2151134,
        name:"K"
    },
    {
        UserID:2151135,
        name:"K"
    },
    {
        UserID:2151136,
        name:"K"
    },
    {
        UserID:2151137,
        name:"K"
    },
    {
        UserID:2151136,
        name:"K"
    },
    {
        UserID:2151136,
        name:"K"
    },
]
let avatarURL = " ";
</script>
  
<style scoped>
.profession-page {
    background-image: url('@/assets/profession-background.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 98vh;
    width: 99vw;
}
.profession-list{
    background-color:  #ace5d8;
    width: 500px;
    height:100px;
    box-shadow: 0px 4px 4px 0px gray;
    border-radius: 30px;
}
.pass_btn{
    /* 设置按钮风格 */
    width: 35px;
    height: 35px;
    border-radius: 50px;
    background-color: rgb(104, 230, 121);
    color: rgb(255, 255, 255);
    border-color: transparent;
    box-shadow: 0px 4px 4px 0px gray;
}
.close_btn{
    width: 35px;
    height: 35px;
    border-radius: 50px;
    background-color: rgb(230, 104, 104);
    color: rgb(255, 255, 255);
    border-color: transparent;
    box-shadow: 0px 4px 4px 0px gray;
}
.card{
    position:fixed;
    top:13%;
    left:48%;
    background-color:  #ffffff;
    width: 700px;
    height:650px;
    box-shadow: 4px 4px 4px 2px gray;
    border-radius: 10px;
}
</style>
  