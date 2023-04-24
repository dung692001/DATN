<script>
import { DxValidator, DxRequiredRule } from 'devextreme-vue/validator';
import DxDateBox from 'devextreme-vue/date-box';
export default {
    name: 'BaseDate',
    emits: ['onValueChanged'],
    components: {
        DxDateBox,
        DxValidator,
        DxRequiredRule
    },
    props: {
        value: {
            type: [String, Date],
            default: null
        },
        labelMode: {
            type: String,
            required: false,
            default: 'static',
            note: 'mode hiển thị label ' // [ 'static', 'floating', 'hidden' ]
        },
        stylingMode: {
            type: String,
            required: false,
            default: 'outlined',
            note: 'mode hiển thị loại style datebox ' // [ 'outlined', 'filled', 'underlined' ]
        },
        displayFormat: {
            type: String,
            default: 'dd/MM/yyyy',
            note: 'mode hiển thị loại ngày datebox '
        }
    },
    data() {
        return {
            isCheckDate: true
        };
    },
    created() {},
    computed: {},
    methods: {
        /**
         * khi giá trị datebox thay đổi
         * @param {*} e event thay đổi
         * @Author: NDDung (21/08/2022)
         */
        onValueChanged(e) {
            // console.log(e.previousValue);
            this.isCheckDate = false;
            // console.log(e.value);
            this.$emit('onValueChanged', e);
        },
        a() {
            console.log('a');
        }
    }
};
</script>

<template>
    <DxDateBox class="ms-date-box" :styling-mode="stylingMode" :label-mode="labelMode" width="100%" :value="value" :display-format="displayFormat" placeholder="DD/MM/YYYY" label="Tiêu đề" @value-changed="onValueChanged" :blur="a">
        <DxValidator>
            <DxRequiredRule />
        </DxValidator>
    </DxDateBox>
</template>

<style scoped>
.ms-date-box {
    box-sizing: border-box;
    border-radius: 2px !important;
    border-color: #bbbbbb;
}

.dx-state-focused {
    border-color: #2ca01c !important;
}
</style>
