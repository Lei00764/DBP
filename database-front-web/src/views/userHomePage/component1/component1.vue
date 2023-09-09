<template>
    <div>
        <!-- 背景布局 -->
        <div class="box">
            <div class="background2">
                <div class="PersonSide">
                <el-form-item class="img">
                    <userAvatar :key="avatarKey" :userId=store.state.Info.id :width=50 :addLink="false"></userAvatar>
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
                        粉丝：{{ fansNumber }}
                    </div>
                    <div class="following">
                        关注：{{ followerNumber }}
                    </div>
                    <div class="content">
                        内容：{{ articleNumber }}
                    </div>
                    <div class="point">
                        积分：{{ UserInfo.points }}
                    </div>

                </div>
            </div>
            <el-form-item>
                <el-button class=sign-button round color=transparent :class="{ 'disabled': formData.isSigned }"
                    :disabled="formData.isSigned" @click="handleSignIn(2, UserInfo.points)" style="color: rgb(0, 0, 0);
                        background-color:rgba(255, 255, 255, 0.383);
                        ;border-radius: 15px;">
                    <el-icon :size="23">
                        <CircleCheckFilled />
                    </el-icon>
                    {{ formData.buttonLabel }}
                </el-button>
            </el-form-item>
            </div>
                <div class="header">
                    <div>
                        <el-select class="themeSelect" v-model="currentSkinName" style="width: 120px" placeholder="浅色主题" @change="switchTheme">
                            <el-option v-for="(item, index) in Object.keys(themeColorObj)" :key="index"
                                :label="themeColorObj[item].title" :value="item">
                            </el-option>
                        </el-select>
                    </div>
                </div>
            <!-- 侧边栏展示 -->

            
        </div>
    </div>
</template>

<script setup="props">
import { ref, reactive, onMounted, computed, nextTick, defineEmits } from 'vue';
import { searchArticle, getArticleNumber } from "@/api/article.js"
import { GetInfoByID, changePoint } from "@/api/user.js"
import { getFansNumber, getFollowNumber } from "@/api/follow.js"
import { House, Star, User } from '@element-plus/icons-vue'
import { ElPagination } from 'element-plus'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex' // 引入store
import themes from '@/utils/themes'
import { colorMix } from "@/utils/theme-tool"

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
const flash = () => {
    location.reload()
    this.$router.go(0)
}
const performFunction = () => {
    let flag =1;
    let i = 0;
    const lastExecutionDate = localStorage.getItem('lastExecutionDate');
    const lastExecutionuser = eval(localStorage.getItem('users'));
    if(!lastExecutionuser){
        var array=[]
        localStorage.setItem('users', JSON.stringify(array));
    }
    else{
        for(i;i<lastExecutionuser.length;i++){
            if(lastExecutionuser[i] == store.state.Info.id){
                flag=0;
            }
        }
    }
    if (!lastExecutionuser||!lastExecutionDate || isNewDay(new Date(lastExecutionDate))||flag) {
        localStorage.setItem('lastExecutionDate', new Date().toISOString());
        lastExecutionuser.push(store.state.Info.id);
        localStorage.setItem('users', JSON.stringify(lastExecutionuser));
        formData.isSigned = false;
    }
    else {
        formData.buttonLabel = '今天已经签过了！';
        formData.isSigned = true;
    }
}
function isNewDay(date) {
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
        emit('child-click', 1)
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
            user_id: store.state.Info.id,
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
            user_id: store.state.Info.id,
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
            user_id: store.state.Info.id,
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
        ID: store.state.Info.id,
        type: 1
    }
    let result = await GetInfoByID(params);
    if (!result) {
        return;
    }
    UserInfo.value = result.data;
};
//主题换色
const currentSkinName = ref('darkTheme');
const themeColorObj = computed(() => ({
    defaultTheme: {
        title: '浅色主题'
    },
    darkTheme: {
        title: '深色主题'
    }
}));

const themeObj = ref({});

onMounted(() => {
    switchTheme();
});

const switchTheme = (type) => {
    currentSkinName.value = type || 'defaultTheme';
    themeObj.value = themes[currentSkinName.value];
    getsTheColorScale();
    Object.keys(themeObj.value).map(item => {
        document.documentElement.style.setProperty(item, themeObj.value[item]);
    });
};

const getsTheColorScale = () => {
    const colorList = ['primary', 'success', 'warning', 'danger', 'error', 'info'];
    const prefix = '--el-color-';
    colorList.map(colorItem => {
        for (let i = 1; i < 10; i += 1) {
            if (i === 2) {
                // todo 深色变量生成未完成 以基本色设置
                themeObj.value[`${prefix}${colorItem}-dark-${2}`] = colorMix(themeObj.value[`${prefix}black`], themeObj.value[`${prefix}${colorItem}`], 1);
            } else {
                themeObj.value[`${prefix}${colorItem}-light-${10 - i}`] = colorMix(themeObj.value[`${prefix}white`], themeObj.value[`${prefix}${colorItem}`], i * 0.1);
            }
        }
    });
};
</script>

<style lang="scss" scoped>
/* 初始化 */
* {
    margin: 0px;
    padding: 0px;
    box-sizing: border-box;
    font-family: sans-serif;
}
.themeSelect{
    position: absolute;
    left: 1100px;
    top: 25px;
}
.box {
    width: 100vw;
    height: 100vh;
    box-sizing: border-box;
    background: var(--el-bg-color);

    .header {
        display: absolute;
        align-items: center;
        height: 65px;
        color: var(--el-text-color-primary);
        background: var(--el-bg-color);
        border-bottom: 4px solid var(--el-color-black);
    }

    .container {
        margin: .5vw 1vh;
        width: 100vw;
    }
}

.img {
    position: absolute;
    left: 25px;
    top: 10px;

}
.sign-button {
    position: absolute;
    width: 219px;
    height: 50px;
    left: 15px;
    top: 550px;
    border-radius: 15px;
}

.disabled {
    background-color: #888888;
    color: #ffffff;
    cursor: not-allowed;
}
.background2 {
    /* Rectangle 4 */
    position: absolute;
    left: 0px;
    width: 260px;
    height: 100vh;
    /* White */
    background: rgb(218, 244, 250);
    /* Elevation / 06 */
    box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.1);
}

/* 个人侧边栏 */
.PersonSide {
    position: absolute;
    width: 260px;
    height: 100vh;
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
    top: 110px;
    border: 1px solid rgb(148, 141, 141);
    border-radius: 20px;
    transform: rotate(-0.58deg);
}

/* 信息 */
.information {
    /* Elaine-GIFT */
    top: 180px;
    left: 40px;
    height: 500px;
    width: 300px;
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

/* 积分 */
.point {
    /* 积分：769 */
    position: absolute;
    top: 240px;
    color: rgb(0, 0, 0);
    letter-spacing: 0px;
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