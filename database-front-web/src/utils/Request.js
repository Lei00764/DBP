import axios from 'axios'

// const baseURL = 'http://localhost:5045/api';
const baseURL = 'http://124.220.110.93:5045/api'

const request = axios.create({  // 创建axios实例
    baseURL: baseURL,
    timeout: 5000
})

export default request;