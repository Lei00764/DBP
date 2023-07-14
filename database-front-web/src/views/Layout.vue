<template>
    <div>
        <div id="container">
            <div id="header">
                <navTop></navTop>
            </div>
            <div id="content_wrapper">

            </div>
        </div>
    </div>
</template>

<script setup>
import navTop from "@/components/navTop.vue"
</script>

<style>
#container {
    height: 30vh;
    width: 100vw;
}

#header {
    /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 */
    height: 10vh;
    width: 100vw;
    box-shadow: 0 2px 6px 0 #ddd;
}

#content_warpper {
    width: 100vw;
}
</style>