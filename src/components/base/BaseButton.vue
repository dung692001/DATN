<script>
export default {
    props: {
        // Nội dung của button
        text: {
            type: String,
            required: true
        },

        // kiểu button
        type: {
            type: String,
            default: 'normal'
        },

        // sự kiện của button
        event: {
            type: String,
            default: 'btnAddOnClick'
        }
    },

    methods: {
        /**
         * focus vào button
         * @Author NDDung (28/07/2022)
         */
        focusInput() {
            this.$refs['btn'].focus();
        }
    }
};
</script>

<template>
    <button ref="btn" id="btnAdd" :class="['button', type]">
        {{ text }}
        <slot></slot>
    </button>
</template>

<style scoped></style>
