import { createStore } from 'vuex'

const store = createStore({
    //存放全局数据
    state: {
        Info:{//用户信息
            avatar:"",
            id:"",
            name:"1",
            tel:"",
            password:"",
            email:"",
        },
        login: false  //登录状态
    },
    //计算属性，获取state里的数据内容
    //只可读取不可修改
    getters:{

    },
    //定义对state的各种操作，只能同步不能异步
    mutations: {
        doLogin(state) {//登录
            state.login = true;
        },
        doLogout(state) {//退出
            state.login = false;
        },
        SaveInfo(state, person_info){
            state.Info=person_info
        }
    },
    //调用mmutations的操作，异步执行
    actions: {
        //increment({ commit }, person_info) {
            //commit('UpdateInfo', person_info);
         // }
    },
    //state信息过长时，用以进行分割
    modules: {
    }
})

export default store