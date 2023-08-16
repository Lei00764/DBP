// 与文件传输相关的 API，主要指的是图片

import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件

// 显示图片/用户头像不需要api，直接通过url就行

const config = {
    headers: {
        'Content-Type': 'multipart/form-data'
    }
};

// 上传用户头像
// 将参数放到 formData 表单里面
// database-front-web/src/components/avatarUploader.vue
export function uploadAvatar(formData) {
    return Request({
        method: "POST",
        url: 'files/uploadAvatar',
        data: formData,
        headers: config.headers
    }).then(function (response) {
        if (response.data.code === 200) {
            Message.success("上传成功");
        } else {
            Message.error("请求成功，但上传失败");
        }
    }).catch(function (error) {  // catch 表示接收到错误响应后的操作        
        Message.error("请求失败，且上传失败");
    });
}