<template>
    <div>
        <div class="checkArticle-page">
            <el-form style="position: absolute;top:20%;left:7%">
                <el-column v-for="item in list">
                    <el-form-item>
                        <el-card class="article-list" @click="goArticle">
                            <b style="position: absolute;top:20%;left:5%;font-size: 18px;color:rgb(61, 61, 61)">
                                帖子标题：{{ item.name }}
                            </b>
                            <b style="position: absolute;top:55%;left:5%;font-size: 14px;color:rgb(120, 120, 120)">
                                ID：{{ item.postID }}
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
                <el-avatar :size="70" style="position: absolute;top:8%;left:7%"></el-avatar>
                <b style="position: absolute;top:9%;left:25%;font-size: 20px;color:rgb(61, 61, 61)">
                    作者名：
                </b>
                <b style="position: absolute;top:16%;left:25%;font-size: 16px;color:rgb(120, 120, 120)">
                    作者ID：
                </b>
                <b style="position: absolute;top:28%;left:9%;font-size: 20px;color:rgb(61, 61, 61)">
                    帖子内容：
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
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
import router from "@/router/index.js"

const goArticle = () =>{
    
}

const pass =() =>{
    
}

let list=[{
        postID:25001,
        name:"如何做好宫保鸡丁"
    },
    {
        postID:25002,
        name:"如何做好宫保鸡丁"
    },
    {
        postID:25003,
        name:"如何做好宫保鸡丁"
    },
    {
        postID:25004,
        name:"如何做好宫保鸡丁"
    },
    {
        postID:25005,
        name:"如何做好宫保鸡丁"
    },
    {
        postID:25006,
        name:"如何做好宫保鸡丁"
    },
    {
        postID:25007,
        name:"如何做好宫保鸡丁"
    },
]

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

.article-list{
    background-color:  #ace5d8;
    width: 450px;
    height:100px;
    box-shadow: 0px 4px 4px 0px gray;
    border-radius: 30px;
}
.pass_btn{
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
    width: 600px;
    height:500px;
    box-shadow: 4px 4px 4px 2px gray;
    border-radius: 10px;
}

</style>