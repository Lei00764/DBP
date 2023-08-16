<template>
    <div>
        <el-form :model="formData" ref="uploadForm" label-width="100px">
            <el-form-item label="头像上传" prop="avatar">
                <el-upload class="avatar-uploader" :show-file-list="false" :before-upload="beforeUpload">
                    <el-button size="small" type="primary">点击上传</el-button>
                </el-upload>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="submitForm">提交</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { useStore } from 'vuex' // 引入store
import axios from 'axios'
const store = useStore(); // 使用store必须加上


const formData = new FormData();

const beforeUpload = (file) => {
    // 添加头像文件到 form
    formData.avatarFile = file;
    return false; // 阻止默认上传
};

const uploadAvatar = async () => {
    formData.append('userId', 16);
    formData.append('avatarFile', formData.avatarFile);

    try {
        const response = await axios.post('http://localhost:5045/api/Files/uploadAvatar', formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });

        if (response.data.code === 200) {
            console.log('上传成功', response.data.msg);
        } else {
            console.error('上传失败', response.data.msg);
        }
    } catch (error) {
        console.error('请求失败', error);
    }
};

const submitForm = () => {
    uploadAvatar();
};
</script>

<style lang="scss"></style>
