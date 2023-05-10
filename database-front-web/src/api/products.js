// 如果某个文件要使用这些方法，只需要在这个文件中导入这个文件即可
// import { getProducts, updateUser } from '@/api/products'

import Request from "@/utils/Request.js";

export function getProducts(params) {
    Request({
        method: 'GET',
        url: '/products',
        params: params
    })
}

export function updateUser(params) {
    Request({
        method: 'PUT',
        url: '/products',
        params: params
    })
}