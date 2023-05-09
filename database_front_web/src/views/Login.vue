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
import { ref, reactive, getCurrentInstance, nextTick } from "vue";
import { useRouter, useRoute } from "vue-router";
import axios from 'axios'

const { proxy } = getCurrentInstance();  // 
const router = useRouter();
const route = useRoute();

const api = {
    login: '/api/login',
}

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
        let url = api.login;
        // 使用axios 对网址为 url，参数为 params 的网页发起请求
        axios.get(url, { params: params }).then(function (response) {
            console.log(response);
            if (response.data.code == 200) {
                proxy.Message.success("登录成功");
                router.push({ path: '/home' });
            } else {
                proxy.Message.error("登录失败");
            }
        }).catch(function (error) {
            console.log(error);
        });
    });
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