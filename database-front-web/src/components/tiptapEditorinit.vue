<template>
    <div class="editor">
        <input type="text" v-model="articleTitle"/>
        <div class="tag-selector">
            <label>选择标签：</label>
            <el-radio-group v-model="selectedTag">
                <el-radio label="中餐">中餐</el-radio>
                <el-radio label="西餐">西餐</el-radio>
                <el-radio label="甜点">甜点</el-radio>
                <el-radio label="其他">其他</el-radio>
            </el-radio-group>
        </div>
        <div class="menubar">
            <span v-for="actionName in activeButtons" :key="actionName">
                <button v-if="actionName === 'bold'" class="menubar__button"
                    :class="{ 'is-active': editor.isActive('bold') }" @click="editor.chain().focus().toggleBold().run()">
                    <icon name="bold" />
                </button>
                <button v-if="actionName === 'italic'" class="menubar__button"
                    :class="{ 'is-active': editor.isActive('italic') }"
                    @click="editor.chain().focus().toggleItalic().run()">
                    <icon name="italic" />
                </button>

                <button v-if="actionName === 'strike'" class="menubar__button"
                    :class="{ 'is-active': editor.isActive('strike') }"
                    @click="editor.chain().focus().toggleStrike().run()">
                    <icon name="strike" />
                </button>

                <button v-if="actionName === 'underline'" class="menubar__button"
                    :class="{ 'is-active': editor.isActive('underline') }"
                    @click="editor.chain().focus().toggleUnderline().run()">
                    <icon name="underline" />
                </button>

                <button v-if="actionName === 'code'" class="menubar__button"
                    :class="{ 'is-active': editor.isActive('code') }" @click="editor.chain().focus().toggleCode().run()">
                    <icon name="code" />
                </button>

                <button v-if="actionName === 'image'" class="menubar__button"
                    :class="{ 'is-active': editor.isActive('image') }" @click="uploadImage">
                    <icon name="image" />
                </button>

                <button v-if="actionName === 'h1'" class="menubar__button menubar__button_h1"
                    :class="{ 'is-active': editor.isActive('heading', { level: 1 }) }"
                    @click="editor.chain().focus().toggleHeading({ level: 1 }).run()">
                    H1
                </button>

                <button v-if="actionName === 'h2'" class="menubar__button menubar__button_h2"
                    :class="{ 'is-active': editor.isActive('heading', { level: 2 }) }"
                    @click="editor.chain().focus().toggleHeading({ level: 2 }).run()">
                    H2
                </button>

                <button v-if="actionName === 'h3'" class="menubar__button menubar__button_h3"
                    :class="{ 'is-active': editor.isActive('heading', { level: 3 }) }"
                    @click="editor.chain().focus().toggleHeading({ level: 3 }).run()">
                    H3
                </button>

                <button v-if="actionName === 'bulletList'" class="menubar__button"
                    :class="{ 'is-active': editor.isActive('bulletList') }"
                    @click="editor.chain().focus().toggleBulletList().run()">
                    <icon name="ul" />
                </button>

                <button v-if="actionName === 'orderedList'" class="menubar__button"
                    :class="{ 'is-active': editor.isActive('orderedList') }"
                    @click="editor.chain().focus().toggleOrderedList().run()">
                    <icon name="ol" />
                </button>

                <button v-if="actionName === 'blockquote'" class="menubar__button"
                    :class="{ 'is-active': editor.isActive('blockquote') }"
                    @click="editor.chain().focus().toggleBlockquote().run()">
                    <icon name="quote" />
                </button>

                <button v-if="actionName === 'codeBlock'" class="menubar__button"
                    :class="{ 'is-active': editor.isActive('codeBlock') }"
                    @click="editor.chain().focus().toggleCodeBlock().run()">
                    <icon name="code" />
                </button>

                <button v-if="actionName === 'horizontalRule'" class="menubar__button"
                    @click="editor.chain().focus().setHorizontalRule().run()">
                    <icon name="hr" />
                </button>

                <button v-if="actionName === 'undo'" class="menubar__button" @click="editor.chain().focus().undo().run()">
                    <icon name="undo" />
                </button>

                <button v-if="actionName === 'redo'" class="menubar__button" @click="editor.chain().focus().redo().run()">
                    <icon name="redo" />
                </button>
            </span>
        </div>
        <editor-content class="editor__content" :editor="editor" />

        <button @click="publishArticle">发布文章</button>
    </div>
</template>
  
<script>
import StarterKit from '@tiptap/starter-kit';
import { Editor, EditorContent } from '@tiptap/vue-3';
import Underline from '@tiptap/extension-underline';
import Icon from '@/components/Icon.vue';
import { postArticle, editArticle } from "@/api/article.js"
import { useStore } from 'vuex' // 引入store
import Image from '@tiptap/extension-image'
import uploadFileToCOS from "@/utils/TXcos.js"

export default {
    name: 'Editor',
    components: {
        EditorContent,
        Icon,
    },
    setup() {
        const store = useStore(); // 使用useStore函数获取Vuex store

        // 在组件中访问Info模块的id属性
        const infoId = store.state.Info.id;

        return {
            infoId,
        };
    },
    props: {
        articleTitlegiven:{
            type: String,
            required: true,
        },
        articleId:{
            type: Int16Array,
            required: true,
        },
        initialContent: {
            type: String,
            required: true,
            default: '<em>editable text</em>',
        },
        activeButtons: {
            type: Array,
            validator: function (list) {
                for (let el of list) {
                    // The value must match one of these strings
                    if (
                        // 修改这里的同时还要修改调用组件的地方
                        [
                            'bold',
                            'italic',
                            'strike',
                            'underline',
                            'code',
                            'image',
                            'h1',
                            'h2',
                            'h3',
                            'bulletList',
                            'orderedList',
                            'blockquote',
                            'codeBlock',
                            'horizontalRule',
                            'undo',
                            'redo',
                        ].indexOf(el) === -1
                    ) {
                        return -1;
                    }
                }
                return 1;
            },
            default: () => ['bold', 'italic'],
        },
    },
    emits: ['update'],
    data() {
        return {
            html: '',
            json: '',
            articleTitle:this.articleTitlegiven,
            editor: null,
            selectedTag: '美食', // 新的选定标签属性，默认为"美食"
        };
    },
    created() {
        this.editor = new Editor({
            content: this.initialContent,
            extensions: [StarterKit, Underline, Image],
        });

        this.html = this.editor.getHTML();
        this.json = this.editor.getJSON();

        this.editor.on('update', () => {
            this.html = this.editor.getHTML();
            this.json = this.editor.getJSON();
            this.$emit('update', this.html);
        });
    },
    beforeUnmount() {
        this.editor.destroy();
    },
    methods: {
        async publishArticle() {
            let result;
            const params = {
                title: this.articleTitlegiven,
                post_id: this.articleId,
                content: this.editor.getHTML(),
            };
            console.log(params);
            result = await editArticle(params);
            if (result.code == 200) {
                window.alert('success');
            }
            else {
                window.alert('error');
            }
        },
        async uploadImage() {
            try {
                // 弹出文件选择框
                const fileInput = document.createElement('input');
                fileInput.type = 'file';
                fileInput.accept = 'image/*';
                fileInput.addEventListener('change', async () => {
                    if (fileInput.files && fileInput.files[0]) {
                        const uploadedFile = fileInput.files[0];
                        const response = await uploadFileToCOS(uploadedFile);
                        // 处理上传成功后的响应，可能是图片的 URL 等等
                        const imageUrl = response.Location; // 假设 COS 返回图片的 URL
                        // 拼接 http + imageUrl
                        const url = 'https://' + imageUrl;
                        // console.log(imageUrl);
                        // 插入图片到编辑器
                        this.editor.chain().focus().setImage({ src: url }).run();
                    }
                });
                fileInput.click();
            } catch (error) {
                // 处理上传失败的情况
                console.error('上传图片失败', error);
            }
        }
    },
};
</script>

<style>
.editor {
    position: relative;
    max-width: 80rem;
    margin: 0 auto 5rem auto;
}

.menubar__button {
    /* 取消原有背景 */
    background: none;
    width: 25px;
    height: 30px;
    /* 去除边框 */
    border: none;
    cursor: pointer;
    /* 设置字体大小 */
    margin: 2px;
    /* 设置字体大小 */
    font-size: 16px;
    transition: box-shadow 0.2s;
    /* 添加过渡效果，以平滑显示阴影 */
}

.menubar__button:hover,
.menubar__button:active {
    /* 鼠标悬停和点击时显示阴影效果 */
    box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
}


.menubar__button_h1 {

    transform: translateY(-2px);
}

.menubar__button_h2 {

    transform: translateY(-2px);
    /* 向上移动 2px */
}

.menubar__button_h3 {

    transform: translateY(-2px);
}
</style>
  