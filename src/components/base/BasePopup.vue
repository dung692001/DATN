<script>
import BaseButton from './BaseButton.vue';
export default {
    name: 'BaseDate',
    emits: ['showPopup', 'confirmDelete', 'closeEmployeeDetail', 'saveEmployee'],
    components: {
        BaseButton
    },
    methods: {
        /**
         * Phương thức xử lý sự kiện khi ấn đóng popup
         * @Author ND Dũng (25/07/2022)
         */
        btnClosePopupOnclick() {
            this.$emit('showPopup', false);
        },

        /**
         * Phương thức xử lý sự kiện khi ấn đóng dialog
         * @Author ND Dũng (25/07/2022)
         */
        btnCloseEmployeeDetail() {
            this.$emit('closeEmployeeDetail');
        },

        /**
         * Phương thức xử lý sự kiện lưu nhân viên
         * @Author ND Dũng (25/07/2022)
         */
        btnSaveEmployee() {
            this.$emit('saveEmployee');
        },

        /**
         * Phương thức xử lý sự kiện xác nhận xóa bản ghi
         * @Author ND Dũng (25/07/2022)
         */
        confirmDelete() {
            this.$emit('confirmDelete');
            this.btnClosePopupOnclick();
        }
    },
    props: {
        // biến truyền vào đóng mở popup
        isShowPopup: {
            type: Boolean,
            required: true
        },
        // biến truyền vào chứa nội dung popup
        popupText: {
            type: [String, Array],
            required: true
        },
        // biến truyền vào chứa kiểu icon
        iconType: {
            type: String,
            default: 'icon__error'
        },
        // biến truyền vào chứa kiểu nút của popup
        buttonType: {
            type: String,
            default: 'button__error'
        }
    },
    data() {
        return {
            // các kiểu button của popup
            errorMode: 'button__error', // dùng để thông báo lỗi ví dụ như không được để trông thông tin
            warningMode: 'button__warning', // dùng để thông báo có muôn xóa không
            infoMode: 'button__info', // dùng để thông báo dữ liệu đã thay đổi có muôn xóa không
            sameCodeMode: 'button__SameCode' // dùng để thông báo mã đã tồn tại
        };
    }
};
</script>

<template>
    <div class="dialog__popup popup--error" v-bind:class="{ isShowPopup: isShowPopup }">
        <div class="popup">
            <div class="popup__content">
                <div class="popup__item" v-for="text in popupText" :key="text">
                    <div class="border__icon48">
                        <div class="icon" :class="iconType"></div>
                    </div>
                    <p class="popup__message">{{ text }}</p>
                </div>
            </div>
            <div class="popup__line"></div>
            <div class="popup__footer">
                <!-- Những nút của popup error -->
                <div :class="['popup__footer--error', buttonType == errorMode ? 'isShowPopup' : '']">
                    <BaseButton :text="'Đóng'" :type="'normal'" @click="btnClosePopupOnclick"> </BaseButton>
                </div>

                <!-- Những nút của popup warning delete-->
                <div :class="['popup__footer--warning', buttonType == warningMode ? 'isShowPopup' : '']">
                    <BaseButton :text="'Không'" :type="'button__outline'" @click="btnClosePopupOnclick"> </BaseButton>

                    <BaseButton :text="'Có'" :type="'normal'" @click="confirmDelete"> </BaseButton>
                </div>

                <!-- Những nút của popup info -->
                <div :class="['popup__footer--info', buttonType == infoMode ? 'isShowPopup' : '']">
                    <BaseButton :text="'Hủy'" :type="'button__outline'" @click="btnClosePopupOnclick"> </BaseButton>
                    <div>
                        <BaseButton class="margin-left-right-10" :text="'Không'" :type="'button__outline'" @click="btnCloseEmployeeDetail"> </BaseButton>

                        <BaseButton :text="'Có'" :type="'normal'" @click="btnSaveEmployee"> </BaseButton>
                    </div>
                </div>

                <!-- Những nút của popup warning trùng mã -->
                <div :class="['popup__footer--agree', buttonType == sameCodeMode ? 'isShowPopup' : '']">
                    <BaseButton :text="'Đồng ý'" :type="'normal'" @click="btnClosePopupOnclick"> </BaseButton>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
@import url('../../css/base/dialog.css');
</style>
