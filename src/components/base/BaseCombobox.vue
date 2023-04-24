<script>
import { Resource } from '../../common/Resource.js';
export default {
    emits: ['setDepartmentName'],
    props: {
        // label của input
        label: {
            type: String
        },

        // icon của input được truyền vào
        inputIcon: {
            type: String
        },

        // nội dung của title bên trái
        dropdownTitleLeft: {
            type: String
        },
        // nội dung của title bên phải
        dropdownTitleRight: {
            type: String
        },
        // dữ liệu được truyền vào
        data: {
            type: [Array, Object, String]
        },
        // thuộc tính tên của dữ liệu truyền vào (ví dụ: departmentName)
        nameData: {
            type: String,
            required: false
        },
        // id của dữ liệu truyền vào (ví dụ: departmentId)
        idData: {
            type: String,
            required: false
        },
        // mã của dữ liệu truyền vào (ví dụ: departmentCode)
        codeData: {
            type: String,
            required: false
        },
        // dữ liệu truyền vào v-model
        dataSelectedText: {
            type: String
        },
        // id được chọn
        dataSelectedId: {
            type: String
        },
        // tabIndex của combobox
        tabIndex: {
            type: Number
        },
        // kiểm tra input có phải là bắt buộc nhập không
        inputRequire: {
            type: Boolean
        },
        // kích thước của input combobox
        size: {
            type: String
        },
        // title của combobox
        labelTitle: {
            type: String
        },
        // biến lưu input có tooltip không
        hasTooltip: {
            type: Boolean,
            default: false
        }
    },
    /**
     * Hàm khởi tạo giá trị cho data
     * @Author NDDung (28/07/2022)
     */
    created() {
        this.inputId = this.dataSelectedId;
        this.inputText = this.dataSelectedText;
        this.dataTemp = this.data;
        this.maxIndex = this.data.length;
    },
    watch: {
        /**
         * Hàm xử lý sự kiện khi giá trị của data thay đổi
         * @param value: data được truyền vào
         * @Author NDDung (28/07/2022)
         */
        data: function (value) {
            this.dataTemp = value;
            this.setIndexSelected(this.dataTemp);
        },

        /**
         * Hàm xử lý sự kiện khi giá trị của dataSelectedText thay đổi
         * @param value: dataSelectedText được thay đổi
         * @Author NDDung (28/07/2022)
         */
        dataSelectedText: function (value) {
            this.inputText = value;
        },

        /**
         * Hàm xử lý sự kiện khi giá trị của dataSelectedId thay đổi
         * @param value: dataSelectedId được thay đổi
         * @Author NDDung (28/07/2022)
         */
        dataSelectedId: function (value) {
            this.inputId = value;
            this.setIndexSelected(this.dataTemp);
        }
    },
    methods: {
        /**
         * Phương thức chọn item
         * @param index: vị trị của index đang chọn
         * @param value: giá trị mà mình đã chọn
         * @Author NDDung (28/07/2022)
         */
        dropdownItemOnClick(index, value) {
            try {
                this.dataSelected = value;
                this.indexSelected = index;
                this.inputText = this.dataSelected[this.nameData];
                this.inputId = this.dataSelected[this.idData];
                this.showCombobox(false);
                this.$emit('setDepartmentName', this.dataSelected);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức đóng mở combobox
         * @param isShow: true là mở form, false là đóng form
         * @Author NDDung (28/07/2022)
         */
        showCombobox(isShow) {
            if (isShow == true) {
                this.$refs['input'].focus();
            }
            this.isShowData = isShow;
        },

        /**
         * Phương thức chọn tạm thời item
         * @param value: giá trị đang chọn tạm thời
         * @Author NDDung (04/08/2022)
         */
        selectingData(value) {
            this.dataSelecting = value;
            this.inputText = this.dataSelecting[this.nameData];
        },

        /**
         * Hàm trả về giá trị đã selected
         * @Author NDDung (04/08/2022)
         */
        unSelectingData() {
            this.inputText = this.dataSelected[this.nameData];
            this.indexSelected = this.indexSelecting;
        },

        /**
         * Thực hiện nhập vào input để filter dữ liệu từ data
         * @Author NDDung (04/08/2022)
         */
        filterInput() {
            var currentWindow = this;
            currentWindow.dataTemp = this.data.filter((e) => {
                return e[currentWindow.nameData].includes(currentWindow.inputText);
            });
        },

        /**
         * Thực hiện gán css dòng đã được selected chọn
         * @param index: nếu index từ v-for trùng với index đang chọn thì sẽ có thêm css selected
         * @Author NDDung (18/08/2022)
         */
        checkIndexSelected(index) {
            if (this.indexSelected == index) {
                return true;
            } else {
                return false;
            }
        },

        /**
         * Thực hiện kiểm tra đã chọn index nào Chưa
         * @param values: dữ liệu của bảng đc truyền vào
         * @Author NDDung (18/08/2022)
         */
        setIndexSelected(values) {
            try {
                for (let i = 0; i < values.length; i++) {
                    if (this.inputId == values[i][this.idData]) {
                        this.indexSelected = i;
                        this.indexSelecting = i;
                        this.dataSelected = values[i];
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Xử lý sự kiện keyup
         * @param e: event trả về của keyup
         * @Author NDDung (18/08/2022)
         */
        handleKeyUp(e) {
            try {
                this.showCombobox(true);
                if (e.key === 'ArrowDown') {
                    if (this.indexSelected <= this.data.length - 1) {
                        this.indexSelected = this.indexSelected + 1;
                    }
                    if (this.indexSelected > this.data.length - 1) {
                        this.indexSelected = 0;
                    }
                    this.selectingData(this.dataTemp[this.indexSelected]);
                }

                if (e.key === 'ArrowUp') {
                    if (this.indexSelected >= 0) {
                        this.indexSelected = this.indexSelected - 1;
                    }
                    if (this.indexSelected < 0) {
                        this.indexSelected = this.data.length - 1;
                    }
                    this.selectingData(this.dataTemp[this.indexSelected]);
                }
                if (e.key === 'Escape') {
                    e.stopPropagation();
                    this.showCombobox(false);
                }
                if (e.key === 'Enter' || e.key === ' ') {
                    this.dropdownItemOnClick(this.indexSelected, this.dataSelecting);
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Xử lý sự kiện keydown
         * @param e: event trả về của keyup
         * @Author NDDung (20/08/2022)
         */
        handleKeyDown(e) {
            if (e.key === 'Tab') {
                this.showCombobox(false);
            }
        },

        /**
         * Kiểm tra input có đang để trống không
         * @Author NDDung (18/08/2022)
         */
        checkInput() {
            try {
                if (this.inputRequire == true) {
                    if (this.inputText === '' || this.inputText === undefined) {
                        this.inputTitle = Resource.messageMustType(this.label);
                        this.$refs['input'].classList.add('fieldinput__error--required');
                    } else {
                        this.inputTitle = '';
                        this.$refs['input'].classList.remove('fieldinput__error--required');
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * focus vào input
         * @Author NDDung (28/07/2022)
         */
        focusInput() {
            this.$refs['input'].focus();
        }
    },
    data() {
        return {
            // data được truyền vào
            // biến bool để đóng mở dropdown
            isShowData: false,
            // text sử dụng để v-model cho input
            inputText: '',
            // id sử dụng để v-model cho input
            inputId: null,
            // chứa thông tin item đẫ chọn
            dataSelected: {},
            // chứa thông tin item đang chọn
            dataSelecting: {},
            // data tạm thời để thực hiện filter
            dataTemp: [],
            // index của item đã được chọn,
            indexSelected: 0,
            // index của item đang được chọn,
            indexSelecting: 0,
            // Tổng số phòng ban
            maxIndex: 0,
            // tooltip của input
            inputTitle: ''
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
            <input type="text" class="fieldinput fieldinput__paddingicon" :tabIndex="tabIndex" ref="input" @keyup="handleKeyUp" @keydown="handleKeyDown" v-model="inputText" @input="filterInput()" @focus="showCombobox(true)" @blur="checkInput" />
            <div @click="showCombobox(true)" v-if="inputIcon" class="dropdown__icon dropdown__icon__item flex">
                <div :class="'icon ' + inputIcon"></div>
            </div>
        </div>
        <div class="dialog__dropdown__data" :class="{ isShowBlock: isShowData }">
            <div v-if="dropdownTitleLeft || dropdownTitleRight" class="dropdown__item combobox__data--row dropdown__header">
                <div class="combobox__data--col1">{{ dropdownTitleLeft }}</div>
                <div class="combobox__data--col2">{{ dropdownTitleRight }}</div>
            </div>
            <div
                v-for="(item, index) in dataTemp"
                :key="index"
                @click="dropdownItemOnClick(index, item)"
                @mousemove="selectingData(item)"
                @mouseout="unSelectingData()"
                class="dropdown__item combobox__data--row"
                :class="{ 'combobox__data--selected': checkIndexSelected(index) }"
            >
                <div class="combobox__data--col1">
                    {{ item[this.codeData] }}
                </div>
                <div class="combobox__data--col2">
                    {{ item[this.nameData] }}
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped></style>
