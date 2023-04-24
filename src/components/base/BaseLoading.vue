<script>
export default {
    props: {
        // biến kiểu bool ẩn hiện loading
        isShowLoadingChild: {
            type: Boolean,
            required: true
        }
    }
};
</script>

<template>
    <div id="loading" class="loading dialog__popup" :class="{ isShowDialog: isShowLoadingChild }">
        <div class="loading__content">
            <div class="loading__icon"></div>
            <div class="loading__text">Đang xử lý</div>
        </div>
    </div>
</template>

<style scoped></style>
