<template>
    <div>
        <!-- 背景布局 -->
        <div class="background1">
            <div class="background2">
            </div>
        </div>
        <!-- 侧边栏展示 -->

        <div class="PersonSide">
            <el-form-item>
                <userAvatar :key="avatarKey" :userId=store.state.Info.id :width=50 :addLink="false"></userAvatar>
                <avatarUploader @avatarUploaded="handleAvatarUploaded"></avatarUploader>
            </el-form-item>
            <div class="PersonSide_text">
                <div class="user_name">
                    <span> {{ UserInfo.name }} </span>

                </div>
                <div class="user_text">
                    <span> level: {{ UserInfo.levels }} </span>
                </div>
            </div>
            <div class="Line">
            </div>
            <div class="information">
                <div class="fans">
                    Fans:{{ fansNumber }}
                </div>
                <div class="following">
                    Following:{{ followerNumber }}
                </div>
                <div class="content">
                    Content:{{ articleNumber }}
                </div>

            </div>


            <!-- 更换主题皮肤 -->
            <div class="theme-page">
                <el-form-item label="动态皮肤">
                    <el-select v-model="value" placeholder="请选择" size="large" @change="handleChange">
                        <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value" />
                    </el-select>
                </el-form-item>
                <!-- <el-row class="m-4">
                </el-row>
                <el-row class="mb-4">
                    <el-button plain>Plain</el-button>
                    <el-button type="primary" plain>Primary</el-button>
                    <el-button type="success" plain>Success</el-button>
                    <el-button type="info" plain>Info</el-button>
                    <el-button type="warning" plain>Warning</el-button>
                    <el-button type="danger" plain>Danger</el-button>
                </el-row> -->
            </div>
        </div>
        <!-- 签到积分 -->
        <div class="rectangle_point">
            <div class="point">
                积分：{{ UserInfo.points }}
            </div>
            <el-button class="pro" @click="applyForProfession">申请专业厨师认证</el-button>
        </div>
        <!-- 签到积分 -->
        <el-form-item>
            <el-button class=sign-button round color=transparent :class="{ 'disabled': formData.isSigned }" :disabled="formData.isSigned"
                @click="handleSignIn(2, UserInfo.points)" style="color: rgb(8, 102, 75);
                        background-color:rgba(224, 248, 242, 0.9);
                        ;border-radius: 15px;">
                <el-icon :size="23">
                    <CircleCheckFilled />
                </el-icon>
                {{ formData.buttonLabel }}
            </el-button>
        </el-form-item>


    </div>
</template>

<script setup="props">
import { ref, reactive, onMounted, onUnmounted, nextTick,defineEmits} from 'vue';
import { searchArticle, getArticleNumber } from "@/api/article.js"
import { GetInfoByID, changePoint } from "@/api/user.js"
import { getFansNumber, getFollowNumber } from "@/api/follow.js"
import { House, Star, User } from '@element-plus/icons-vue'
import { ElPagination } from 'element-plus'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex' // 引入store
import '../theme/theme.css';
const store = useStore(); // 使用store必须加上
const emit = defineEmits(['child-click'])
const router = useRouter()


const formData = reactive({
    keyword: '',
    isSigned: false,
    buttonLabel: '签到',
    posting: 0,
    index: 0,
});
const props = defineProps({
    title: {
        type: String, // 可以设置传来值的类型
        default: ""
    },
    fans_num: {
        type: Int16Array,

        default: 0
    },
    following_num: {
        type: Int16Array,
        default: 0
    },
    content_num: {
        type: Int16Array,
        default: 0
    },
})

const avatarKey = ref(0); // 添加一个标志位
const handleAvatarUploaded = () => {
    // 头像上传成功后，更新标志位来重新加载userAvatar组件
    avatarKey.value += 1;
}

onMounted(() => {
    fetchnum();
    fetchuser();
    fetchfollownum();
    fetchfansnum();
    performFunction();
})
const UserInfo = ref([]);
const articleNumber = ref(0);
const fansNumber = ref(0);
const followerNumber = ref(0);
//————————————————————————————————函数————————————————————————
const performFunction = () => {
    const lastExecutionDate = localStorage.getItem('lastExecutionDate');
    if (!lastExecutionDate || isNewDay(new Date(lastExecutionDate))) {
        localStorage.setItem('lastExecutionDate', new Date().toISOString());
        formData.isSigned = false;
    } 
    else {
        formData.buttonLabel = '今天已经签过了！';
        formData.isSigned = true;
    }
}
function isNewDay(date){
    const today = new Date();
    return (
    date.getDate() !== today.getDate() ||
    date.getMonth() !== today.getMonth() ||
    date.getFullYear() !== today.getFullYear()
    );
}
//签到
const handleSignIn = async (point, current_point) => {
    formData.isSigned = true;
    formData.buttonLabel = '签到成功，积分+2';
    let result, level;
    if (current_point + point > 20) {
        point -= 20;
        level = 1;
    }
    else {
        level = 0;
    }
    const params = {
        user_id: store.state.Info.id,
        point_add: point,
        level_add: level,
    };
    console.log(params);
    result = await changePoint(params);
    if (result.code == 200) {
        window.alert('success');
        emit('child-click',1)
    }
    else {
        window.alert('error');
    }
};
//文章数目
const fetchnum = async (stringValue = '') => {
    let result;
    if (!stringValue) {
        stringValue = "0"
        const params = {
            user_id: 6
        };
        result = await getArticleNumber(params);
    }
    else {
        const params = {
            keyword: stringValue
        };
        // 这里并没有写好还得改
        result = await getArticleNumber(params);
    }

    if (!result)
        return;
    articleNumber.value = result;

};
//关注数目
const fetchfollownum = async (stringValue = '') => {
    let result;
    if (!stringValue) {
        stringValue = "0"
        const params = {
            user_id: 8
        };
        result = await getArticleNumber(params);
    }
    else {
        const params = {
            keyword: stringValue
        };
        // 这里并没有写好还得改
        result = await getArticleNumber(params);
    }

    if (!result)
        return;
    fansNumber.value = result;

};
//粉丝数目
const fetchfansnum = async (stringValue = '') => {
    let result;
    if (!stringValue) {
        stringValue = "0"
        const params = {
            user_id: 8
        };
        result = await getFollowNumber(params);
    }
    else {
        const params = {
            keyword: stringValue
        };
        // 这里并没有写好还得改
        result = await getFollowNumber(params);
    }

    if (!result)
        return;
    followerNumber.value = result;

};
//获取当前用户信息
const fetchuser = async () => {
    const params = {
        ID:store.state.Info.id,
        type: 1
    }
    let result = await GetInfoByID(params);
    if (!result) {
        return;
    }
    UserInfo.value = result.data;
};





const value = ref("");
const input = ref("");
//主题颜色选择
const options = [
    {
        Label: "默认",
        value: "",
    },
    {
        Label: "暗黑",
        value: "dark",
    },
    {
        Label: "火山红",
        value: "red",
    },
];

const handleChange = (e) => {
    document.querySelector("htrl").className = e;
}

</script>

<style lang="scss" scoped>
/* 初始化 */
* {
    margin: 0px;
    padding: 0px;
    box-sizing: border-box;
    font-family: sans-serif;
}

.pro {
    position: absolute;
    left: 75px;
    top: -310px;
    border-radius: 15px;
}


.sign-button {
    position: absolute;
    width: 219px;
    height: 50px;
    left: 0px;
    top: 500px;
    border-radius: 15px;
}

.disabled {
    background-color: #888888;
    color: #ffffff;
    cursor: not-allowed;
}

/* 积分 */
.rectangle_point {
    position: absolute;
    width: 220px;
    height: 111px;
    left: 8px;
    top: 380px;
    background: rgba(224, 248, 242, 0.9);
    border-radius:
        27px;
}

/* 积分 */
.point {
    /* 积分：769 */
    position: absolute;
    left: 30px;
    top: 45px;
    color: rgb(8, 102, 75);
    font-family: Noto Sans SC;
    font-size: 20px;
    letter-spacing: 0px;
    display: flex;
}

.background1 {
    position: absolute;
    width: 1230px;
    height: 540px;
    left: 0;
    top: 0;
    bottom: 340px;
    background: linear-gradient(180.00deg, rgba(224, 248, 242, 0.9), rgba(224, 248, 242, 0) 100%);
}

.background2 {
    /* Rectangle 4 */
    position: absolute;
    left: 0;
    width: 260px;
    height: 800px;
    /* White */
    background: rgb(255, 255, 255);
    /* Elevation / 06 */
    box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.1);
}

/* 个人侧边栏 */
.PersonSide {
    position: absolute;
    width: 260px;
    height: 800px;
}


/* 昵称 */
.PersonSide_text {
    /* Elaine-GIFT */
    position: absolute;
    width: 252px;
    height: 47px;
    left: 100px;
    right: 84px;
    top: 10px;
    color: rgb(0, 0, 0);
    font-family: Cabin;
    font-size: 20px;
    text-align: left;
}

/* 信息 */
.user_text {
    /* Elaine-GIFT */
    color: rgb(98, 98, 98);
    font-size: 8px;
}

.Line {
    /* 直线 1 */
    position: absolute;
    width: 230px;
    left: 10px;
    height: 0;
    top: 100px;
    border: 1px solid rgb(148, 141, 141);
    border-radius: 20px;
    transform: rotate(-0.58deg);
}

/* 信息 */
.information {
    /* Elaine-GIFT */
    top: 160px;
    left: 40px;
    height: 500px;
    position: absolute;
    color: rgb(0, 0, 0);
    font-family: Georgia, serif;
    font-size: 18px;
    text-align: left;
}

.content {
    position: absolute;
    top: 80px;
}

.following {
    position: absolute;
    top: 160px;
}




/* 主题颜色 */
.theme-page {
    position: absolute;
    padding: 8px;
    top: 570px;
    width: 240px;

    ::v-deep .m-4 {
        margin: 4px;
    }
}
</style>