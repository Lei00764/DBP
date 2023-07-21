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
                            <span class="iconfont icon-ic_play_black"></span>
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button class="button" @click="doSubmitRegister">
                            <span>注册</span>
                            <span class="iconfont icon-ic_play_black"></span>
                        </el-button>
                    </el-form-item>
                </elform>
            </div>
        </div>
    </div>
</template>
  
<script setup>
import { ref, reactive, computed } from 'vue';
import { userLogin, GetInfoByEmail } from '@/api/user';  // 引入 api 请求函数 userLogin,GetInfo
import Message from "@/utils/Message.js"
import router from "@/router/index.js"
import { useStore } from 'vuex' // 引入store
const store = useStore(); // 使用store必须加上


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
            // console.log(person_info);  // {code: 200, msg: '普通用户登录成功', type: 1}
            afterLogin();
        })
        .catch(function (error) {
            console.log(error);
        });

    // 这里不能访问 person_info，因为异步操作可能还没有完成
};


const afterLogin = (type) => {
    store.commit("doLogin"); // 修改登录状态
    // console.log(store.state.login); / 获取state值
    let params = {
        Email: formData.email,
    };
    GetInfoByEmail(params)// 获取信息
        .then(function (result) {
            // result 包括 code、msg和data三部分，只需要其中的 data 部分
            result = result.data;
            console.log(result);
            let person_info = {
                avatar: result.avatar,
                id: result.id,
                name: result.name,
                tel: result.tel,
                password: formData.password,
                email: result.email,
            }
            // 进行store存储
            // console.log(person_info);
            store.commit('SaveInfo', person_info); // 调用mutations，将信息传入store
            // console.log(store.state.Info)
        })
        .catch(function (error) {
            console.log(error);
        });
    // 到现在为止，保存登录信息已经实现，可通过store.state.Info获取相应的值
};


// 注册
const doSubmitRegister = () => {
    router.push({ path: '/register' });
};

</script>
  
<style scoped>
.login-page {
    background-image: url('@/assets/log_in.png');
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

.login-form-button {
    position: absolute;
    top: 70%;
    left: 91%;
    transform: translate(-50%, -50%);
    height: 10%;
    width: 20%;
}

.button {
    height: 100%;
    width: 50%;
    background-color: black;
    color: #ffffff;
}



:deep(.el-input__wrapper) {
    background: #ffffff;
    border-radius: 12px;

}
</style>
  