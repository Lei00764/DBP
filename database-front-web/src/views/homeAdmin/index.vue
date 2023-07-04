<template slot="color">
    <div>
        <div class="homeAdmin-page">
            <div class="homeAdmin-position">
                <!-- 确定放置位置 -->
                <el-form :inline=true>
                    <!-- 表单，让所有空间尽量显示到一行 -->
                    <el-form-item>  
                        <!-- 控件1：搜索栏 -->
                        <el-input placeholder="请输入关键词" class="homeAdmin-searchbox" v-model="formData.keyword">
                            <template #prefix>
                              <el-icon>
                                <Search />
                              </el-icon>
                            </template>
                        </el-input>
                    </el-form-item>
                    <el-form-item>  
                        <!-- 控件2：搜索按钮 -->
                        <el-button type="primary" class="homeAdmin-button" @click="doSearch">
                            <span>搜索</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
    const doSearch=()=>{
        //进行关键词搜索
    };

    const formData = reactive({
        keyword: '',
    });
</script>

<style scoped>
/*背景图相关设置 */
.homeAdmin-page {
    /* 背景图片地址 */
    background-image: url('@/assets/home-administrator.png');
    /* 背景图片位置 */
    background-position: center center;
     /* 背景图片是否重复 */
    background-repeat: no-repeat;
     /* 背景图片大小 */
    background-size: 100% 100%;
     /* 背景图片宽高 */
    height: 98vh;
    width: 99vw;
}

.homeAdmin-position{
    /* 搜索栏的页面位置 */
    position: absolute;
    top: 45%;
    left: 4%; 
}

.homeAdmin-searchbox {
    /* 搜索栏的大小格式*/
    width: 800px;
	height: 50px;
}
:deep(.el-input__wrapper) {
    /* 修改搜索栏的背景、圆角、字体颜色 */
    background:#08664B; 
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
    color: rgb(235, 235, 235);
}
:deep(.el-input__inner) {
    /* 修改搜索栏的背景和圆角 */
    color: rgb(235, 235, 235);
}

.homeAdmin-button{
    width: 80px;
	height: 35px;
    border-radius: 30px;
    background-color: white;
    color:black;
    border-color: black; 
    box-shadow: 0px 4px 4px 0px gray;
}

</style>