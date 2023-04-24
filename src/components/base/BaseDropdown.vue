<script>
export default {
    name: 'BaseDropdown',
    emits: ['showDropdown'],
    props: {
        // biến lưu đóng mở dropdown
        isShow: {
            type: Boolean
        },
        // kiểu dropdown
        dropdownKind: {
            type: String,
            required: true
        },
        // dữ liệu truyền vào
        contentArray: {
            type: Array
        },
        // text của dropdown
        dropdownText: {
            type: String
        },
        /**
         * Kết quả đã chọn
         */
        pageSize: {
            type: Number
        }
    },
    methods: {
        /**
         * Phương thức chọn phòng ban
         * @Author NDDung (28/07/2022)
         */
        dropdownItemOnClick(value) {
            this.dropdownItemSelected(value);
        },

        /**
         * xử lý sự kiện khi chọn phong ban
         * @Author NDDung (28/07/2022)
         */
        dropdownItemSelected(value) {
            if (this.isShow) {
                this.$emit('showDropdown', false, value);
            } else {
                this.$emit('showDropdown', true, '');
            }
        }
    },
    data() {
        return {};
    }
};
</script>

<template>
    <div>
        <div v-bind:class="[dropdownKind, { isShowBlock: isShow }]">
            <div @click="dropdownItemOnClick(item)" :class="['dropdown__item', { 'dropdown__item--selected': item == pageSize ? true : false }]" :title="item + dropdownText" v-for="item in contentArray" :key="item">{{ item }} {{ dropdownText }}</div>
        </div>
    </div>
</template>

<style scoped></style>
