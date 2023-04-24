<script>
import { deleteMode } from '../../common/Enum.js';
import { Resource } from '../../common/Resource.js';
export default {
    name: 'BaseContextMenu',
    emits: ['showWarningPopup', 'btnCloneOnClick'],
    props: {
        // Mã nhân viên truyền vào để tạo thành thông báo cho popup
        employeeCode: {
            type: String
        },

        // Khóa chính nhân viên truyền vào để mở ra form nhân bản
        employee: {
            type: Object
        }
    },
    methods: {
        /**
         * Phương thức thêm nội dung và gọi phương thức xác nhận xóa
         * @Author Nguyễn Đăng Dũng (25/07/2022)
         */
        btnDeleteOnClick() {
            this.popupNotification = Resource.messageDelete(this.employeeCode);
            this.$emit('showWarningPopup', true, this.popupNotification, deleteMode.single);
        },

        /**
         * Phương thức thêm nội dung và gọi phương thức xác nhận xóa
         * @Author Nguyễn Đăng Dũng (25/07/2022)
         */
        btnCloneOnClick() {
            this.$emit('btnCloneOnClick', this.employee);
        }
    },

    data() {
        return {
            // biến lưu text cho pop up thông báo
            popupNotification: []
        };
    }
};
</script>
<template>
    <div class="dropdown__data-table">
        <div class="dropdown__item-table" @click="btnCloneOnClick()">Nhân bản</div>
        <div class="dropdown__item-table btnDelete" @click="btnDeleteOnClick">Xóa</div>
        <div class="dropdown__item-table">Sử dụng</div>
    </div>
</template>

<style scoped></style>
