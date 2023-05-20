<template>
    <div>
        <el-form class="login" :model="formData" :rules="rules" ref="formDataRef">
            <el-form-item prop="id">
                <el-input size="large" clearable placeholder="请输入用户名" v-model="formData.id" maxLength="150">
                </el-input>
            </el-form-item>
            <el-form-item prop="password">
                <el-input size="large" clearable placeholder="请输入密码" v-model="formData.password" maxLength="150">
                </el-input>
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
</template>

<script setup>
import { ref, getCurrentInstance } from "vue";
import { userLogin } from '@/api/user'

const api = {
    login: 'login',
}
const { proxy } = getCurrentInstance();  // 获取当前实例

const formData = ref({});
const formDataRef = ref();
const rules = {}

const doSubmitLogin = () => {
    formDataRef.value.validate(async (valid) => {  // 校验
        let params = {
            id: formData.value.id,
            password: formData.value.password,
        };
        console.log(params);

        userLogin(params);  // 请求登录
    });
}

const doSubmitRegister = () => {

}
</script>

<style lang="scss">
.login {
    width: 400px;
    margin: 0 auto;
    padding-top: 100px;
}

.op-btn {
    display: block;
    margin: 0 auto;
    width: 40%;
    margin-top: 20px;
}
</style>