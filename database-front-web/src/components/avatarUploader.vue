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
import { uploadAvatar } from '@/api/files.js'

const store = useStore(); // 使用store必须加上

const formData = new FormData();

const beforeUpload = (file) => {
    // 添加头像文件到 form
    formData.avatarFile = file;
    return false; // 阻止默认上传
};

const submitForm = () => {
    formData.append('userId', store.state.Info.id);
    formData.append('avatarFile', formData.avatarFile);

    uploadAvatar(formData);
};
</script>

<style lang="scss"></style>
