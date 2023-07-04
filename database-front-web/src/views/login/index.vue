<template>
    <div>
        <div class="login-page">
            <div class="login-form">
                <el-form>
                    <el-form-item>
                        <el-input placeholder="请输入邮箱" v-model="formData.email"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-input placeholder="请输入密码" v-model="formData.password" :type="showPassword ? 'text' : 'password'"
                            show-password></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" class="op-btn" @click="doSubmitLogin">
                            <span>登录</span>
                        </el-button>
                        <el-button type="primary" class="op-btn" @click="doSubmitRegister">
                            <span>注册</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
        </div>
    </div>
</template>
  
<script setup>
import { ref, reactive } from 'vue';
import { userLogin } from '@/api/user';
import Message from "@/utils/Message.js"
import router from "@/router/index.js"

const formData = reactive({
    email: '',
    password: '',
});

const showPassword = ref(false);

// 登录
const doSubmitLogin = () => {
    if (!formData.email || !formData.password) {
        Message.error("账号或密码不能为空");
        return;
    }

    console.log(formData);
    let params = {
        email: formData.email,
        password: formData.password,
    };

    userLogin(params); // 请求登录
};

// 注册
const doSubmitRegister = () => {
    router.push({ path: '/register' });
};

</script>
  
<style scoped>
.login-page {
    background-image: url('@/assets/log_in.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 100vh;
    width: 100vw;
}

.login-form {
    position: absolute;
    top: 50%;
    left: 70%;
    transform: translate(-50%, -50%);
    height: 10%;
    width: 20%;
}
</style>
  