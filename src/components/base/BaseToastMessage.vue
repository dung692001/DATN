<script>
import { toast } from '../../JS/toastMes';
export default {
    props: {
        // nội dung của toast message
        toastContent: {
            type: String,
            default: 'Thành công!'
        },
        // kiểu toast message
        toastType: {
            type: String,
            default: 'success'
        },
        // thời gian tồn tại của toast message
        toastDuration: {
            type: Number,
            default: 3000
        }
    },

    methods: {
        /**
         * Phương thức hiện toast message
         * @Author NDDung (04/08/2022)
         */
        showToast() {
            toast({
                message: this.toastContent,
                type: this.toastType,
                duration: this.toastDuration
            });
        }
    }
};
</script>

<template>
    <div id="toast"></div>
</template>

<style scoped></style>
