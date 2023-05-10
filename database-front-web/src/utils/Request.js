import axios from 'axios'

const request = axios.create({  // 创建axios实例
    baseURL: '/api',
    timeout: 5000
})

export default request;