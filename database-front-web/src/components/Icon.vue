<template>
    <div class="icon">
        <img :src="iconPath" :alt="name" />
    </div>
</template>
  
<script>
export default {
    props: {
        name: String,
    },
    computed: {
        iconPath() {
            return `/icons/${this.name}.svg`;
        },
    },
};
</script>

<style scoped>
.icon {
    position: relative;
    width: 14px;
    height: 14px;
}
</style>