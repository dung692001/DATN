<script>
export default {
    components: {},
    data() {
        return {
            id: '',
            dataLoadImg: 'images/26596a20-0b90-4ca6-9094-a2058d598bd3'
        };
    },
    methods: {
        getImgId(value) {
            this.id = value;
        }
    }
};
</script>

<template>
    <div class="content__header">
        <p class="title">Nhân viên</p>
        <BaseUpload @url-bind="getImgId"> </BaseUpload>
        <BaseDownload :linkImg="dataLoadImg"> </BaseDownload>
    </div>
</template>
