<script>
export default {
    name: 'BaseRadio',
    emits: ['changeGender', 'changeMarrieStatus'],
    props: {
        // Tên của radio
        radioName: {
            type: String,
            required: true
        },
        // nội dung của radio
        radioText: {
            type: String,
            required: true
        },
        // giá trị của radio được truyền vào từ v-model
        modelValue: {
            type: Number,
            default: 0
        },
        // giá trị của radio mặc định
        value: {
            type: Number,
            default: 0
        },
        // Giá trị của tabIndex
        tabIndex: {
            type: Number
        },
        radioType: {
            type: Number
        }
    },
    methods: {
        /**
         * Hàm khởi tạo trạng thái radio
         * @Author NDDung (27/07/2022)
         */
        radioStatus() {
            if (this.value === this.modelValue) {
                this.isChecked = true;
            } else {
                this.isChecked = false;
            }
        },

        /**
         * Xử lý sự kiện khi click chọn giới tính
         * @Author NDDung (27/07/2022)
         */
        rdoOnClick() {
            if (this.radioType == 0) {
                this.$emit('changeGender', this.value);
            } else if (this.radioType == 1) {
                this.$emit('changeMarrieStatus', this.value);
            }
        },

        /**
         * Xử lý sự kiện key up của input radio
         * @Author NDDung (27/07/2022)
         */
        rdoKeyUpHandle(e) {
            if (e.key === 'Enter' || e.key === ' ') {
                this.isBorder = false;
                if (this.radioType == 0) {
                    this.$emit('changeGender', this.value);
                } else if (this.radioType == 1) {
                    this.$emit('changeMarrieStatus', this.value);
                }
            }
            if (e.key === 'Tab') {
                this.isBorder = true;
            }
        },

        /**
         * Xử lý sự kiện key down của input radio
         * @Author NDDung (27/07/2022)
         */
        rdoKeyDownHandle(e) {
            if (e.shiftKey && e.key === 'Tab') {
                this.isBorder = false;
            }
            if (e.key === 'Tab') {
                this.isBorder = false;
            }
        }
    },
    data() {
        return {
            // biến kiểu tra việc chọn của các radio
            isChecked: false,
            // nếu bằng true thì radio có border và ngược lại
            isBorder: false
        };
    }
};
</script>

<template>
    {{ radioStatus() }}
    <div :class="['flex', { 'radio--selected': isBorder }]">
        <input :tabIndex="tabIndex" :name="radioName" type="radio" class="radiobutton__input" :value="value" :checked="isChecked" @click="rdoOnClick()" @keyup="rdoKeyUpHandle" @keydown="rdoKeyDownHandle" />
    </div>

    <span class="radiobutton__text">{{ radioText }}</span>
</template>

<style scoped></style>
