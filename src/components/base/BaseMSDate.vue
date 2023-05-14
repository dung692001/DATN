<script>
export default {
    emits: ['setDateTime'],
    props: {
        // Dữ liệu truyền vào là ngày tháng
        dateSelect: {
            type: [String, Number, Date]
        },

        // Tabindex của date
        tabIndex: {
            type: Number
        },

        isDisabled: {
            type: Boolean,
            default: false
        }
    },
    watch: {
        dateSelect: function (value) {
            this.dateTime = value;
        }
    },
    methods: {
        /**
         * Hàm trả về ngày
         * @Author NDDung(21/08/2022)
         */
        setDateTime() {
            let tempDate = new Date();
            if (tempDate > this.dateTime) {
                this.$emit('setDateTime', this.dateTime);
                this.hasErrorDateType = false;
            } else {
                this.$emit('setDateTime', this.dateTime);
                this.$emit('showErrorInput');
                this.hasErrorDateType = true;
                this.focusInput();
            }
        },

        /**
         * Hàm set focus cho input date
         * @Author NDDung(21/08/2022)
         */
        focusInput() {
            this.$refs['inputDate'].focus();
        }
    },
    data() {
        return {
            // v-model của date
            dateTime: '',
            // nếu bằng true thì nghĩa là ngày đã sai
            hasErrorDateType: false
        };
    }
};
</script>

<template>
    <div>
        <el-date-picker
            ref="inputDate"
            type="date"
            format="DD/MM/YYYY"
            placeholder="DD/MM/YYYY"
            size="default"
            @change="setDateTime"
            :class="[{ 'fieldinput__date--error': hasErrorDateType }]"
            :tabindex="tabIndex"
            v-model="dateTime"
            :disabled="isDisabled"
        />
    </div>
</template>

<style scoped></style>
