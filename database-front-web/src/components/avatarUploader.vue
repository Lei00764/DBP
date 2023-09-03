<template>
    <div>
        <el-form :model="formData" ref="uploadForm" label-width="100px">
            <el-row>
                <el-col :span="12">
                    <el-upload class="avatar-uploader" :show-file-list="false" :before-upload="beforeUpload">
                        <el-button type="primary">上传</el-button>
                    </el-upload>
                </el-col>
                <el-col :span="12">
                    <el-button type="primary" @click="submitForm">提交</el-button>
                </el-col>
            </el-row>
        </el-form>
    </div>
</template>
  
<script setup>
import { ref, onMounted, defineEmits } from 'vue';
import { useStore } from 'vuex';
import { uploadAvatar } from '@/api/files.js';

const store = useStore();
const formData = new FormData();
const emit = defineEmits(['avatarUploaded']);

const beforeUpload = (file) => {
    formData.avatarFile = file;
    return false;
};

const submitForm = () => {
    formData.append('userId', store.state.Info.id);
    formData.append('avatarFile', formData.avatarFile);

    uploadAvatar(formData)
        .then(() => {
            // 上传成功后触发自定义事件
            emit('avatarUploaded');
        });
};
</script>
  
<style lang="scss"></style>
  