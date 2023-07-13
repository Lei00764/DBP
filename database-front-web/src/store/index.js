import { createStore } from 'vuex'

const store = createStore({
    //存放全局数据
    state: {
        id:"",
        email:"",
        name:"",
        avatar:"",
        password:""
    },
    //计算属性，获取state里的数据内容
    //只可读取不可修改
    getters:{

    },
    //定义对state的各种操作，只能同步不能异步
    mutations: {
        
    },
    //调用mmutations的操作，异步执行
    actions: {
    },
    //state信息过长时，用以进行分割
    modules: {
    }
})

export default store