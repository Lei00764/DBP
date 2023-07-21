<template>


  <el-row>
    <el-col
      v-for="(o, index) in 2"
      :key="o"
      :span="8"
      :offset="index > 0 ? 2 : 0"
    >
    <div
    :style="{'max-height': this.timeLineHeight + 'px' }"
    style="overflow-y:scroll;"
 >
</div>
      <el-card :body-style="{ padding: '0px' }">
        <img
          src="https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png"
          class="image"
        />
        <div style="padding: 14px">
          <span>Yummy hamburger</span>
          <div class="bottom">
            <time class="time">{{ currentDate }}</time>
            <el-button type="primary" round :icon="Edit" />
            <el-button type="danger" round :icon="Delete" />
            
          </div>
        </div>
      </el-card>
    </el-col>
  </el-row>


</template>


<script lang="ts" setup>
import { ref } from 'vue'
const currentDate = ref(new Date())//
import  {showArticle ,deleteAticle} from "@/api/article"
import { Delete, Edit } from '@element-plus/icons-vue'

// 存储返回的数据
// const allMenus = ref()
//获取数据的完整逻辑
// const getAllArticle = asyncu =>{
//   const res =  getAl1()
//   console.log(res)
// }
// getAllArtical()


const showArticle = async (PostId) => {//展示帖子缩略（包括标题、日期
    let result = await proxy.Request({
        url: api.getArticleDetail,
        params: {
            PostId: PostId,
        }
    });
    if (!result) {
        return;
    }
    articleInfo.value = result.data;
}




//
// 获取当前作者信息
const fetchAuthor = () => {
      // 暂时用1替代当前用户ID
      axios.get('http://localhost:5173/api/user/1')
        .then(response => {
          author = response.data;
        })
        .catch(error => {
          console.error(error);
          Message.error("您的信息拉取失败");
        });
    };

    // 获取当前作者的所有文章
    const fetchArticles = () => {
       // 暂时用1替代当前用户ID
      axios.get('http://localhost:5173/api/user/1/articles')
        .then(response => {
          articles = response.data;
        })
        .catch(error => {
          console.error(error);
          Message.error("文章列表拉取失败");
        });
    };

    // 添加新文章
    const addArticle = () => {
       // 暂时用1替代当前用户ID
      axios.post('http://localhost:5173/api/article', {
        authorId: 1,
        title: newArticle.title,
        createdAt: newArticle.createdAt
      })
      .then(response => {
        // 添加成功后将新文章加入articles数组中
        articles.push(response.data.article);
        newArticle.title = '';
        newArticle.createdAt = '';
      })
      .catch(error => {
        console.error(error);
        Message.error("新增文章失败");
      });
    };

    // 删除文章
    const deleteArticle = (articleId) => {
      axios.delete(`http://localhost:5173/api/article/${articleId}`)
        .then(() => {
          // 删除成功后从articles数组中移除该文章
          articles = articles.filter(article => article.id !== articleId);
        })
        .catch(error => {
          console.error(error);
          Message.error("删除文章失败");
        });
    };

    // 编辑文章
    const editArticle = (article) => {
      editingArticle.value = article.id;
      editedArticle.title = article.title;
      editedArticle.createdAt = article.createdAt;
      dialogVisible.value = true; // 打开对话框
    };

    // 更新文章
    const updateArticle = () => {
      const articleId = editingArticle.value;
      axios.put(`http://localhost:5173/api/article/${articleId}`, {
        title: editedArticle.title,
        createdAt: editedArticle.createdAt
      })
      .then(response => {
        const updatedArticle = response.data.article;
        const index = articles.findIndex(article => article.id === updatedArticle.id);
        if (index !== -1) {
          // 更新成功后更新articles数组中的对应文章数据
          articles.splice(index, 1, updatedArticle);
        }
        cancelEdit();
      })
      .catch(error => {
        console.error(error);
        Message.error("更新文章失败");
      });
    };

    // 取消编辑
    const cancelEdit = () => {
      editingArticle.value = null;
      editedArticle.title = '';
      editedArticle.createdAt = '';
      dialogVisible.value = false; // 关闭对话框
    };



</script>

<style scoped>
.time {
  font-size: 12px;
  color: #999;
}

.bottom {
  margin-top: 13px;
  line-height: 12px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}



.image {
  width: 100%;
  display: block;
}



</style>
