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

const api = {
    login: 'login',
}
const { proxy } = getCurrentInstance();

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

        proxy.Request({  // 发送请求
            method: 'GET',
            url: api.login,
            params: params
        }).then(function (response) {
            if (response.data.code == 200) {
                proxy.Message.success("登录成功");
                // router.push({ path: '/home' });  // 路由跳转，将页面跳转到路径为 '/home' 的页面
            } else {
                proxy.Message.error("登录失败");
            }
        }).catch(function (error) {
            console.log(error);
        });
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