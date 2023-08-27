<template>
    <div>
        <div class="addArticle">
            <!-- 顶部导航栏 -->
            <div class="addArticle-head">
                <el-form :inline=true>
                    <el-icon class="homepageIcon" @click="doHome">
                        <HomeFilled />
                    </el-icon>

                    <el-icon class="userpageIcon" @click="doUser">
                        <UserFilled />
                    </el-icon>

                    <el-form-item>
                        <el-button type="primary" class="logoff-button" @click="doLogoff">
                            <span>退出登录</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
            <!-- 按钮 -->
            <div class="addArticle-button">
                <!-- 返回按钮 -->
                <el-form-item>
                    
                    <el-button type="primary" class="back-button" @click="doReturn">
                        <span class="iconfont icon-Back"></span>
                        <span>返回</span>
                    </el-button>
                </el-form-item>
                <!-- 发布按钮 -->
                <el-form-item>
                 
                    <el-button type="primary" class="publish-button" @click="edit" >
                        <span class="iconfont icon-publish"></span>
                        <span>发布</span>
                    </el-button>
                </el-form-item>
            </div>

            <div class="page_head">
            <span class="welcome-word">在这里更改你的文章</span>
            </div>
            <!-- 展示公告板，添加内容 -->

            <div class="editArticle-form">
            <span class="editArticle-title-word">标题</span>
                <input class="editArticle-title" v-model="formData.title" placeholder="Please input title" disabled>
                <span class="editArticle-tag-word">标签</span>
                <input class="editArticle-tag" v-model="formData.tag" placeholder="Please input title" disabled>
                <span class="editArticle-content-word">内容</span>
                <textarea class="editArticle-content" v-model="formData.content" placeholder="Type what you want"></textarea>
            </div>
        </div>
    </div>
</template>

<script setup>
import router from "@/router/index.js"
import { ref, reactive, toRefs, onMounted } from 'vue';
import { useRoute } from 'vue-router'
import { postArticle,GetArticleDetailsAsync,editArticle } from "@/api/article.js"
import { useStore } from 'vuex' // 引入store
onMounted(() => {
    getArticleDetail(route.query.articleId);
});

const store = useStore(); // 使用store必须加上
const route = useRoute();
const articleInfo = ref([]);
const getArticleDetail = async () => {
    const params = {
        article_id: route.query.articleId,
    }
    let result = await GetArticleDetailsAsync(params);
    if (!result) {
        return;
    }
    
    articleInfo.value = result.data;
    formData.title=articleInfo.value[0].title;
    formData.tag=articleInfo.value[0].tag;
    formData.content=articleInfo.value[0].content;
}
const formData = reactive({
    title:"",
    tag:"",
    content:"",
   
});
const edit = async() => {
    let result;
    const params = {
        post_id: route.query.articleId,
        title:formData.title,
        content:formData.content,
    };
    console.log(params);
    result = await editArticle(params);
    if(result.code==200){
      window.alert('success');
    }
    else{
      window.alert('error');
    }
};
const doHome = () => {
    //返回主页
    router.push({ name: 'homeUser' });
}
const doUser = () => {
    //返回用户主页
    router.push({ path: 'userHomePage' });
};
const doReturn = () => {
    //返回论坛界面
    router.push({ path: 'userHomePage' })
};
const doLogoff = () => {
    //退出登录
    router.push({ name: 'login' })
};
</script>

<style scoped>
    .welcome-word
    {
    position: absolute;
    left: 320px;
    top: 60px;
    font-size: 40px;
    color: #0b0f0e;
    }
.addArticle {
    background-image: url('@/assets/addArticle_bkg.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 100vh;
    width: 100vw;
}

.homepageIcon {
    /* 返回主页图标相关设置 */
    position: absolute;
    left: 1400px;
    top: 77px;
    font-size: 33px;
    color: rgb(55, 192, 255)
}

.homepageIcon:hover {
    opacity: 0.8;
}

.userpageIcon {
    /* 返回用户主页图标相关设置 */
    position: absolute;
    left: 1450px;
    top: 77px;
    font-size: 33px;
    color: rgb(55, 192, 255)
}

.userpageIcon:hover {
    opacity: 0.8;
}

.logoff-button {
    /* 退出登录按钮相关设置 */
    position: absolute;
    top: 66px;
    left: 1510px;
    height: 33px;
    width: 80px;
    background: #08664B;
    border-color: black;
    border-radius: 14px;
    box-shadow: 0px 4px 4px 0px gray;
}

.logoff-button:hover {
    opacity: 0.8;
}

.addArticle-button {
    position: sticky;
    top: 2100px;
    left: 90%;
    width: 30%;
}


/* 两个按钮样式修改稀碎 */
/* 返回按钮 */
.back-button {
    position: absolute;
    top: 25px;
    left: -400px;
    background: #08664B;
}


/* 发布按钮 */
.publish-button {
 
    position: sticky;
    top: 70px;
    left: 240px;
    height: 80%;
    background: #08664B;
   
}


/* 发布文章界面 */
.editArticle-form {
    position: absolute;
    top: 150px;
    left: 300px;
    height: 750px;
    width: 1150px;
    border-radius: 12px;
    
}

.editArticle-title {
    position: absolute;
    top: 20px;
    left: 160px;
    height: 45px;
    width: 300px;
    border-radius: 12px;
    background-color: white;
    border-width: 3px;
    border-style:dashed;
    border-color:#08664B  ; 
    
}

.editArticle-title-word {
    position: absolute;
    top: 22px;
    left: 50px;
    font-size: 28px;
    color: rgb(63, 133, 80);
    
}
input::-webkit-input-placeholder {
    /* placeholder颜色 */
    color: #adb2af;
    /* placeholder字体大小 */
    font-size: 18px;
}


.editArticle-tag {
    position: absolute;
    top: 120px;
    left: 160px;
    height: 50px;
    width: 510px;
    border-radius: 12px;
    background-color: white;
    border-width: 3px;
    border-style:dashed;
    border-color:#08664B  ; 
    font-size: 20px;
}

.editArticle-tag-word {
    position: absolute;
    top: 130px;
    left: 50px;
    font-size: 28px;
    color: rgb(63, 133, 80);
}


/* 下拉选项框样式(有点多) --start */


/* // 取消el-select的边框 */
	:deep(.el-input){
		width:510px;
        height:50px;
		--el-input-focus-border:#fff;
		--el-input-transparent-border: 0 0 0 0px;
		--el-input-border-color:#fff;
		--el-input-hover-border:0px !important;
		--el-input-hover-border-color:#fff;
		--el-input-focus-border-color:#fff;
		--el-input-clear-hover-color:#fff;
		box-shadow: 0 0 0 0px !important;
		--el-input-border:0px;
	}
	:deep(.el-select .el-input__wrapper.is-focus){
		box-shadow: 0 0 0 0px !important;
	}
	:deep(.el-select .el-input.is-focus .el-input__wrapper){
		box-shadow: 0 0 0 0px !important;
	}
	:deep(.el-select){
		--el-select-border-color-hover:#fff;
	}

    /* //修改单个的选项的样式 */


.el-select {
	width: 510px;
    height: 50px;
    font-size: 20px;
}
.input-with-select .el-input-group__prepend {
	background: rgba(10, 30, 55, 0.7);
	box-shadow: 0px 4px 4px rgba(49, 49, 49, 0.5);
}
.el-input-group {
	line-height: normal;
	display: inline-table;
	width: 100%;
	border-collapse: separate;
	border-spacing: 0;
	height: 50px;
	.el-input-group__append,
	.el-input-group__prepend {
		background-color: rgba(10, 30, 55, 0.7);
	}
}
.el-input__inner{
	background: rgba(10, 30, 55, 0.7);
    width: 510px;
    height: 50px;
    border:none;
}
/* // 修改下拉菜单背景颜色 */
.el-scrollbar {
	background: #1d2f46;
	border-radius: 6px;
}
/* //下拉背景 */
.el-card.is-hover-shadow:focus,
.el-card.is-hover-shadow:hover,
.el-color-picker__panel,
.el-menu--popup,
.el-message-box,
.el-picker-panel .el-time-panel,
.el-picker__popper.el-popper[role='tooltip'],
.el-popover.el-popper,
.el-select-v2__popper.el-popper[role='tooltip'],
.el-select__popper.el-popper[role='tooltip'],
.el-table-filter {
	opacity: 0.8;
	border-radius: 6px;
}
.el-popper__arrow {
	display: none;
}
.el-select-dropdown__item {
	color: #3b8e2d;
}
/* //修改输入框背景颜色 */
.el-input-group > .el-input__inner {
	box-shadow: 0px 4px 4px rgba(49, 49, 49, 0.5);
	background: rgba(10, 30, 55, 0.7);
	/* // width: 260px;
	// height: 46px; */
	border-left: 0;
	border-right: 0;
	font-size: 20px;
}
.el-input-group--prepend .el-input__inner,
.el-input-group__append {
	background: rgba(10, 30, 55, 0.7);
	border-top-left-radius: 0;
	border-bottom-left-radius: 0;
}
.el-input-group__append {
	border-left: 0;
}
.el-select-dropdown__wrap,
.el-scrollbar__wrap,
.el-scrollbar__wrap--hidden-default {
	background: rgba(10, 30, 55, 0.7);
}
/* //修改输入框字体 */
.el-input-group--append .el-input__inner,
.el-input-group__prepend {
	font-size: 20px;
	color: #3b8e2d;
}

/* //修改下拉框的字体 */
.el-select-dropdown__list {
	padding: 5px;
	text-align: center;
    /* //修改单个的选项的样式 */
	.el-select-dropdown__item {
		padding: 0 0.2rem 0 0.2rem;
		color: #3b8e2d;
		font-size: 26px;
	}
	.el-select-dropdown__item.selected {
		color: #3b8e2d;
	}
    /* //item选项的hover样式 */
	.el-select-dropdown__item.hover,
	.el-select-dropdown__item:hover {
		background-color: #67b784;
	}
}
/* // 修改下拉箭头左侧字体大小颜色 */
.el-input-group__append button.el-button,
.el-input-group__append div.el-select .el-input__inner,
.el-input-group__append div.el-select:hover .el-input__inner,
.el-input-group__prepend button.el-button,
.el-input-group__prepend div.el-select .el-input__inner,
.el-input-group__prepend div.el-select:hover .el-input__inner {
	color: #3b8e2d !important;
	font-size: 20px;
}
/* // 修改鼠标选中输入框时输入框的颜色 */
input.el-input__inner:focus {
	border-color: #8cdfa9;
}

/* 下拉选项框样式 --end */


.editArticle-content {
    position: absolute;
    top: 240px;
    left: 160px;
    height: 380px;
    width: 1000px;
    border-radius: 12px;
    background-color: white;
    border-width: 3px;
    border-style:dashed;
    border-color:#08664B       

}

.editArticle-content-word {
    position: absolute;
    top: 240px;
    left: 50px;
    font-size: 28px;
    color: rgb(63, 133, 80);
}


textarea::-webkit-input-placeholder {
  /* WebKit browsers */
  color: #adb2af;
  font-size: 30px;
}


</style>