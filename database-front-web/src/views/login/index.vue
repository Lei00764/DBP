<template>
    <div>
        <div class="login-page">
            <div class="login-form-1">
                <el-form>
                    <el-form-item>
                        <el-input placeholder="请输入邮箱" v-model="formData.email"></el-input>
                    </el-form-item>
                </el-form>
            </div>
            <div class="login-form-2">
                <el-form>  
                    <el-form-item>
                        <el-input placeholder="请输入密码" v-model="formData.password" :type="showPassword ? 'text' : 'password'"
                            show-password></el-input>
                    </el-form-item>                   
                </el-form>
            </div>
            <div class="login-form-button">
                <elform>
                    <el-form-item>
                        <el-button class="button" @click="doSubmitLogin">
                            <span>登录</span>
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button class="button" @click="doSubmitRegister">
                            <span>注册</span>
                        </el-button>
                    </el-form-item>
                </elform>
            </div>
        </div>
    </div>
</template>
  
<script setup>
import { ref, reactive } from 'vue';
import { userLogin } from '@/api/user';  // 引入 api 请求函数 userLogin
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

    let params = {
        email: formData.email,
        password: formData.password,
    };

    userLogin(params)
        .then(function (result) {  // result 是 api /user/login 的返回值，在后端 api 定义
            // 接收返回值，放在 person_info 变量中
            let person_info = result
            // 在这里可以使用 person_info 变量  
            // eg. 登录完成后，调用其他函数
            afterLogin(person_info);

        })
        .catch(function (error) {
            console.log(error);
        });

    // 这里不能访问 person_info，因为异步操作可能还没有完成
};


const afterLogin = (person_info) => {
    // 在这里可以使用 person_info 变量
    console.log(person_info);
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

.login-form-1 {
    position: absolute;
    top: 48%;
    left: 86%;
    transform: translate(-50%, -50%);
    height: 10%;
    width: 20%;

}
.login-form-2 {
    position: absolute;
    top: 58%;
    left: 86%;
    transform: translate(-50%, -50%);
    height: 10%;
    width: 20%;
}

.login-form-button{
    position: absolute;
    top: 70%;
    left: 91%;
    transform: translate(-50%, -50%);
    height: 10%;
    width: 20%;
}

.button{
    height:120%;
    width:50%;
    background-color: black;
    color:#ffffff;
}



:deep(.el-input__wrapper) {
    background: #ffffff;
    border-radius: 12px;

}

</style>
  