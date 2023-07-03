<template>
    <div>
        <div class="login-page">
            <div class="login-form">
                <el-form>
                    <el-form-item>
                        <el-input placeholder="请输入邮箱" v-model="formData.email"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-input placeholder="请输入密码" v-model="formData.password" type="password"></el-input>
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


const formData = reactive({
    email: '',
    password: ''
});

// 登陆
const doSubmitLogin = () => {
    console.log(formData)
}

// 注册
const doSubmitRegister = () => {

}

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
