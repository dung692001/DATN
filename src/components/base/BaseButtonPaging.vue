<script>
export default {
    props: {
        // nội dung của nút chuyển trang
        textButton: {
            type: [Number, String]
        }
    },
    methods: {}
};
</script>
<template>
    <button>{{ textButton }}</button>
</template>

<style scoped></style>
