<template>
    <div>
        <h1>申请成为专业厨师</h1>
        <el-form @submit.native.prevent="submitApplication">
            <el-form-item label="阐述：">
                <el-input type="textarea" v-model="form.illustrate" />
            </el-form-item>
            <el-form-item label="证明：">
                <el-input type="textarea" v-model="form.evidence" />
            </el-form-item>
            <el-button type="primary" native-type="submit">提交申请</el-button>
        </el-form>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { ApplyProfession } from "@/api/profession.js"
import { useStore } from 'vuex' // 引入store
import Message from "@/utils/Message.js"


const store = useStore(); // 使用store必须加上

const form = ref({
    illustrate: '',
    evidence: '',
});

const submitApplication = () => {
    // 提交申请的逻辑，这里暂时只做 console.log 输出
    // console.log(form.value);
    let params = {
        user_id: store.state.Info.id,
        illustrate: form.value.illustrate,
        evidence: form.value.evidence
    }
    console.log(params);
    ApplyProfession(params);


};
</script>

<style lang="scss"></style>