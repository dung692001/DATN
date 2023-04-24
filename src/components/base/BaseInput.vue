<script>
import { Resource } from '../../common/Resource.js';
export default {
    name: 'BaseInput',
    emits: ['showDropdown', 'setToastMessage', 'showToastMessage'],
    props: {
        // label của input
        label: {
            type: String
        },
        // kiểm tra input có phải là bắt buộc nhập không
        inputRequire: {
            type: Boolean
        },
        // dữ liệu của input
        modelValue: {
            type: String
        },
        // kích thước của input
        size: {
            type: String
        },
        // nội dung trong placeholder
        placeHolder: {
            type: String
        },
        // icon của input
        inputIcon: {
            type: String
        },
        // biến kiểu bool để kiểm tra xem có đang show dropdown hay không
        isShowDropdown: {
            type: Boolean
        },
        // biến kiểu bool để kiểm tra chỉ đọc
        isReadOnly: {
            type: Boolean
        },
        // biến kiểu bool để gán input phải được validata(ví dụ email...)
        inputRequireValidate: {
            type: Boolean
        },
        // tooltip của label input khi viết tắt
        labelTitle: {
            type: String
        },
        // tabindex của input
        tabIndex: {
            type: [Number, String]
        },
        // biến chứa chiều dài tối đa của input
        maxLengthInput: {
            type: [String, Number]
        },
        // biến lưu input có tooltip không
        hasTooltip: {
            type: Boolean,
            default: false
        },
        // biến kiểm tra input có icon Không
        hasIcon: {
            type: Boolean,
            default: false
        }
    },
    methods: {
        /**
         * Kiểm tra input bắt buộc nhập có đang để trống không
         * @Author NDDung (27/07/2022)
         */
        checkInput() {
            try {
                // kiểm tra có để trống thông tin bắt buộc nhập không
                if (this.inputRequire == true) {
                    if (this.modelValue === '' || this.modelValue === undefined) {
                        this.inputTitle = Resource.messageMustType(this.label);
                        this.$refs['input'].classList.add('fieldinput__error--required');
                    } else {
                        this.inputTitle = '';
                        this.$refs['input'].classList.remove('fieldinput__error--required');
                    }
                }
                // kiểm tra các thông tin bắt buộc phải validate
                if (this.inputRequireValidate == true) {
                    if ((this.modelValue != '' || this.modelValue === undefined) && this.validateInput(this.modelValue) == false) {
                        this.inputTitle = Resource.messageNotValid(this.label);
                        this.$emit('setToastMessage', this.inputTitle, 'error', 2000);
                        this.$refs['input'].classList.add('fieldinput__error--required');
                        this.$emit('showToastMessage');
                    }
                    if (this.validateInput(this.modelValue) == true) {
                        this.inputTitle = '';
                        this.$refs['input'].classList.remove('fieldinput__error--required');
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * xử lý sự kiện khi ấn button drop down
         * @Author NDDung (28/07/2022)
         */
        inputIconOnClick() {
            if (this.isShowDropdown) {
                this.$emit('showDropdown', false, '');
                this.isRorateIcon = false;
            } else {
                this.$emit('showDropdown', true, '');
                this.isRorateIcon = true;
            }
        },

        /**
         * focus vào input
         * @Author NDDung (28/07/2022)
         */
        focusInput() {
            this.$refs['input'].focus();
        },

        /**
         * validateEmail
         * @Author NDDung (4/08/2022)
         */
        validateInput(value) {
            if (value == '' || value == undefined) {
                return true;
            }
            const regex = /^\w+([\\.-]?\w+)*@\w+([\\.-]?\w+)*(\.\w{2,3})+$/;
            const matches = regex.test(value);
            return matches;
        }
    },
    data() {
        return {
            // tooltip của input
            inputTitle: '',
            // xoay icon
            isRorateIcon: false
        };
    }
};
</script>
<template>
    <div :tooltip-content="inputTitle" :class="['dialog__item ', size, { 'tooltip-title-dialog': hasTooltip }]">
        <div v-if="label" class="dialog__label flex-align-center">
            <label class="dialog__lable" for="" :title="labelTitle">{{ label }} </label><span v-if="inputRequire" class="lb__red">*</span>
        </div>
        <div class="fieldinput__content">
            <input
                :tabIndex="tabIndex"
                type="text"
                class="fieldinput"
                :class="[{ fieldinput__paddingicon: hasIcon }]"
                :value="modelValue"
                ref="input"
                :readonly="isReadOnly"
                @input="$emit('update:modelValue', $event.target.value)"
                @blur="checkInput"
                :placeholder="placeHolder"
                :maxlength="maxLengthInput"
            />
            <div @click="inputIconOnClick" v-if="inputIcon" class="dropdown__icon dropdown__icon__item flex" :class="{ 'dropdown--open': isRorateIcon }">
                <div :class="'icon ' + inputIcon"></div>
            </div>
        </div>
        <slot></slot>
    </div>
</template>

<style scoped></style>
