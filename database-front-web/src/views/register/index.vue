<template>
    <div>
        <div class="register-page">
            <div class="register-form">
                <el-form>
                    <el-form-item>
                        <el-input placeholder="请输入用户名" v-model="formData.username"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <!-- 空一行 -->
                    </el-form-item>
                    <el-form-item>
                        <!-- 空一行 -->
                    </el-form-item>
                    <el-form-item>
                        <el-input placeholder="请输入邮箱" v-model="formData.email"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <!-- 空一行 -->
                    </el-form-item>
                    <el-form-item>
                        <!-- 空一行 -->
                    </el-form-item>
                    <el-form-item>
                        <el-input placeholder="请输入密码" v-model="formData.password" :type="showPassword ? 'text' : 'password'"
                            show-password></el-input>
                    </el-form-item>
                    <el-form-item>
                        <!-- 空一行 -->
                    </el-form-item>
                    <el-form-item>
                        <!-- 空一行 -->
                    </el-form-item>
                    <el-form-item>
                        <el-input placeholder="请确认密码" v-model="formData.confirmPassword"
                            :type="showPassword ? 'text' : 'password'" show-password></el-input>
                    </el-form-item>
                    <el-radio-group v-model="formData.type">
                        <el-radio :label="0">管理员</el-radio>
                        <el-radio :label="1">用户</el-radio>
                    </el-radio-group>
                </el-form>
            </div>
            <div class="register-form-button">
                <el-form-item>
                        <el-button class="button" @click="doSubmitRegister">
                            <span>注册</span>
                            <span class="iconfont icon-ic_play_black"></span>
                        </el-button>
                        <el-button class="button" @click="doSubmitLogin">
                            <span>登录</span>
                            <span class="iconfont icon-ic_play_black"></span>
                        </el-button>
                    </el-form-item>
            </div>
        </div>
    </div>
</template>
  
<script setup>
import { ref, reactive } from 'vue';
import Message from '@/utils/Message.js';
import { userRegister } from '@/api/user';
import router from '@/router/index.js';

const formData = reactive({
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
    type: 0, // 默认选中管理员
});

const showPassword = ref(false);

const doSubmitRegister = () => {
    if (!formData.username || !formData.email || !formData.password || !formData.confirmPassword) {
        Message.error('用户名或账号或密码不能为空');
        return;
    }

    if (formData.password !== formData.confirmPassword) {
        Message.error('密码不一致，请重新输入');
        return;
    }

    let params = {
        username: formData.username,
        email: formData.email,
        password: formData.password,
        type: formData.type,
    };

    userRegister(params) // 请求注册
        .then(function (result) {  // result 是 api /user/register 的返回值，在后端 api 定义
            console.log("注册成功");
            console.log(result);
        })
        .catch(function (error) {
            console.log(error);
        });
};

const doSubmitLogin = () => {
    router.push({ path: '/login' });
};

</script>
  
<style scoped>
.register-page {
    background-image: url('@/assets/sign_up.png');
    /* 背景图片地址 */
    background-position: center center;
    /* 背景图片位置 */
    background-repeat: no-repeat;
    /* 背景图片是否重复 */
    background-size: 100% 100%;
    /* 背景图片大小 */
    height: 98vh;
    /* 背景图片宽高 */
    width: 99vw;
}

.register-form {
    position: absolute;
    top: 41%;
    left: 17%;
    transform: translate(-50%, -50%);
    height: 10%;
    width: 20%;
}


.register-form-button{
    position: absolute;
    top: 90%;
    left: 17%;
    transform: translate(-50%, -50%);
    height: 10%;
    width: 20%;
}

.button{
    height:100%;
    width:42%;
    background-color: black;
    color:#ffffff;
}



:deep(.el-input__wrapper) {
    background: #ffffff;
    border-radius: 12px;

}
</style>
  