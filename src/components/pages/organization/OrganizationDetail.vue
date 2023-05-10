<script>
import OrganizationApi from '../../../apis/OrganizationApi.js';
import DepartmentApi from '../../../apis/DepartmentApi.js';
import { resourceVN } from '../../../common/Resource.js';
import { Resource } from '../../../common/Resource.js';
export default {
    name: 'OrganizationDetail',
    emits: ['showOrganizationDetailDialog', 'loadDataByFilter', 'setToastMessage', 'showToastMessage', 'showToastMessage'],
    /**
     * Phương thức loadData khi lần đầu mở form
     * @Author NDDung (23/07/2022)
     */
    mounted() {
        this.getAllDepartment();
        // nếu là mode edit thì thực hiện lấy thông tin của nhân viên
        if (this.formMode === 1) {
            this.getOrganization(this.organizationSelectedId);
        }
        // nếu là mode clone thì thực hiện lấy thông tin của nhân viên sau đó gán mã mơi
        if (this.formMode === 2) {
            this.getOrganization(this.organizationSelectedId);
            this.organization.OrganizationCode = '';
            this.getNewCode();
        }
    },
    methods: {
        /**
         * Phương thức xử lý sự kiện khi ấn đóng dialog
         * @Author NDDung (23/07/2022)
         */
        btnCloseOnClick() {
            // showOrganizationDetailDialog chỉ là cái để tham chiếu đến @showOrganizationDetailDialog ở cha

            this.$emit('showOrganizationDetailDialog', false);
        },

        /**
         * Thực hiện cất dữ liệu
         * @param saveMode: nếu là 0 là add, 1 là edit, 2 là clone
         * @Author NDDung (23/07/2022)
         */
        btnSaveOnClick(saveMode) {
            try {
                let currentWindow = this;

                // 1. Validate dữ liệu:
                if (currentWindow.validateInput() == true) {
                    // 2. build object thông tin nhân viên:(đã xử lí khi dùng v-model)
                    // 3. Kiểm tra trạng thái và gọi api thực hiện cất dữ liệu:
                    // mode = 0 là add còn mode = 1 là sửa
                    if (currentWindow.formMode == 0 || currentWindow.formMode == 2) {
                        this.setToastMessage(Resource.messageSuccess('Thêm'), 'success', 2000);
                        // Thực hiện thêm mới
                        OrganizationApi.saveOrganizationByAdd(currentWindow.organization).then(
                            () => {
                                // Nếu thêm mới thành công hiện thị toast msg thông báo thành công:
                                currentWindow.showToastMessage();
                                // Load lại dữ liệu
                                currentWindow.$emit('loadData');
                                // Nếu ấn vào cất
                                if (saveMode == 0) {
                                    // Đóng form thêm mới
                                    currentWindow.$emit('showOrganizationDetailDialog', false);
                                }
                                // Nếu ấn vào cất và thêm
                                else {
                                    // Đóng form thêm mới
                                    currentWindow.$emit('showOrganizationDetailDialog', false);
                                    this.organization = {};
                                    currentWindow.$emit('btnAddOnClick');
                                }
                            },
                            (err) => {
                                if (err.code == 'ERR_BAD_REQUEST') {
                                    currentWindow.checkSameCode(err);
                                }
                            }
                        );
                    } else {
                        currentWindow.setToastMessage(Resource.messageSuccess('Sửa'), 'success', 2000);
                        // Thực hiên sửa nhân viên
                        OrganizationApi.saveOrganizationByEdit(currentWindow.organizationSelectedId, currentWindow.organization).then(
                            () => {
                                // Nếu sửa thành công hiện thị toast msg thông báo thành công:
                                currentWindow.showToastMessage();
                                // Load lại dữ liệu
                                currentWindow.$emit('loadData');

                                if (saveMode == 0) {
                                    // Đóng form thêm mới
                                    currentWindow.$emit('showOrganizationDetailDialog', false);
                                }
                                // Nếu ấn vào cất và thêm
                                else {
                                    // Đóng form thêm mới
                                    currentWindow.$emit('showOrganizationDetailDialog', false);
                                    this.organization = {};
                                    currentWindow.$emit('btnAddOnClick');
                                }
                            },
                            (err) => {
                                if (err.code == 'ERR_BAD_REQUEST') {
                                    currentWindow.checkSameCode(err);
                                }
                            }
                        );
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức mở popup cảnh báo lỗi
         * @param isShow biến true false để đóng mở popup
         * @Author NDDung (25/07/2022)
         */
        showOrganizationDetailPopup(isShow) {
            try {
                if (isShow == false) {
                    if (this.popupType == 'icon__error' || (this.popupType == 'icon__warning' && this.inputRequire.length > 0)) {
                        this.$refs[this.inputRequire[0]].focusInput();
                    }
                }
                this.isShowPopup = isShow;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức validate dữ liệu
         * @Author NDDung (25/07/2022)
         */
        validateInput() {
            try {
                let valid = true;
                this.inputRequire = [];
                this.popupNotification = [];
                // Kiểm tra mã có đang để trống hay không
                if (!this.organization.OrganizationCode) {
                    this.inputRequire.push('inputCode');
                    this.popupNotification.push(resourceVN.ValidateError_OrganizationCodeNotEmpty);
                    valid = false;
                }
                // Kiểm tra tên có đang để trống hay không
                if (!this.organization.OrganizationName) {
                    this.inputRequire.push('inputName');
                    this.popupNotification.push(resourceVN.ValidateError_OrganizationNameNotEmpty);
                    valid = false;
                }

                if (valid == false) {
                    this.showErrorPopup();
                }
                return valid;
            } catch (error) {
                console.log(error);
            }
        },

        /**Phương thức show popup báo lỗi
         * @Author NDDung (25/07/2022)
         *
         */
        showErrorPopup() {
            this.popupType = 'icon__error';
            this.popupButtonType = 'button__error';
            this.showOrganizationDetailPopup(true);
        },

        /**
         * Phương thức quay trở về tabindex đầu tiên
         * @Author NDDung (25/07/2022)
         */
        returnFirstTabIndex() {
            try {
                this.$refs['inputCode'].focusInput();
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức quay trở về tabindex cuối cùng
         * @Author NDDung (25/07/2022)
         */
        returnLastTabIndex() {
            try {
                this.$refs['btnDeny'].focusInput();
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức get organization từ organizationId
         * @param organizationId: id truyền vào để lấy toàn bộ thông tin nhân viên
         * @Author NDDung (25/07/2022)
         */
        async getOrganization(organizationId) {
            let currentWindow = this;
            try {
                this.$refs['inputCode'].focusInput();
                await OrganizationApi.getOrganizationById(organizationId).then(
                    (res) => {
                        currentWindow.organization = res.data;
                        setTimeout(function () {
                            for (let item of currentWindow.lstDepartment) {
                                if (currentWindow.organization.DepartmentId == item.DepartmentId) {
                                    currentWindow.organization.DepartmentName = item.DepartmentName;
                                }
                            }
                            currentWindow.organizationTemp = JSON.stringify(currentWindow.organization);
                        }, 100);
                    },
                    (err) => {
                        console.log(err);
                    }
                );
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức kiểm tra dữ liệu đã thay đổi hay chưa
         * @Author NDDung (29/07/2022)
         */
        checkDataChange() {
            if (JSON.stringify(this.organization) == JSON.stringify(this.organizationTemp)) {
                this.$emit('showOrganizationDetailDialog', false);
                this.organization = {};
                this.btnCloseOnClick();
            } else {
                this.popupNotification = [];
                this.popupNotification.push(resourceVN.DataChanged_DoYouWantToSave);
                this.popupType = 'icon__info';
                this.popupButtonType = 'button__info';
                this.showOrganizationDetailPopup(true);
            }
        },

        /**
         * Phương thức set toast message
         * @Author NDDung (03/08/2022)
         */
        setToastMessage(content, type, duration) {
            this.toastContent = content;
            this.toastType = type;
            this.toastDuration = duration;
        },

        /**
         * Phương thức show toast message
         * @Author NDDung (03/08/2022)
         */
        showToastMessage() {
            this.$refs['toastMesDetail'].showToast();
        },

        /**
         * Phương thức set các giá trị của organization
         * @value organization đã chọn
         * @Author NDDung (03/08/2022)
         */
        setDepartmentName(value) {
            if (value != '') {
                this.organization.DepartmentId = value.DepartmentId;
                this.organization.DepartmentName = value.DepartmentName;
            }
        },
        /**
         * Phương thức lấy phòng ban
         * @Author NDDung (28/07/2022)
         */
        getAllDepartment() {
            let currentWindow = this;
            try {
                DepartmentApi.getAllDepartment().then(
                    (res) => {
                        currentWindow.lstDepartment = res.data;
                    },
                    (err) => {
                        console.log(err);
                    }
                );
            } catch (error) {
                console.log(error);
            }
        }

        // end methods
    },
    props: {
        // biến bool hiển thị việc ẩn hiện dialog detail
        isShow: {
            type: Boolean
        },
        // id của nhân viên đang được chọn
        organizationSelectedId: {
            type: String
        },
        // lưu mode của dialog là thêm hay sửa
        formMode: {
            type: [Object, Number],
            required: true
        }
    },
    watch: {
        organizationSelectedId: function (value) {
            this.getAllDepartment();
            if (value) {
                this.getOrganization(this.organizationSelectedId);
            }
        },
        organizationCode: function (value) {
            this.organization.organizationCode = value;
        }
    },
    data() {
        return {
            // object lưu 1 nhân viên được truyền vào từ organizationList
            organization: {},
            //Biến lưu danh sách phòng ban
            lstDepartment: [],
            // biến kiểu bool lưu việc đóng mở popup
            isShowPopup: false,
            // biến lưu text cho pop up thông báo
            popupNotification: [],
            //  biến kiểu bool lưu việc đóng mở dropdown
            isShowDropdown: false,
            // array chứa các input require chưa nhập
            inputRequire: [],
            // json chứa thông tin của nhân viên để kiểm tra dữ liệu đã thay đổi Chưa
            organizationTemp: {},
            // kiểu của popup (ví dụ success, error)
            popupType: null,
            // kiểu nút của popup
            popupButtonType: null,
            // Nội dung của toast message
            toastContent: '',
            // Kiểu toast message success, warning, info, error
            toastType: '',
            // Thời gian tồn tại toast message
            toastDuration: 0,
            // Kiểu của nút cất dữ liệu
            saveMode: {
                saveDefault: 0, // Cất
                saveAndAdd: 1 // Cất và thêm
            }
        };
    }
};
// end script
</script>

<template>
    <div id="dlgOrganizationDetail" class="dialog__popup" @keydown="hanldeOnKeyDown" @keyup="hanldeOnKeyUp" v-bind:class="{ isShowDialog: true }">
        <div class="dialog width-600">
            <div class="dialog__header">
                <div class="dialog__title flex">
                    <div class="title__popup">{{ formMode != 1 ? 'Thêm Dự Án' : 'Sửa Dự Án' }}</div>
                </div>
                <div class="dialog__header__button">
                    <div class="border__icon32 tooltip-title-dialog" tooltip-content="Giúp (F1)">
                        <div class="icon icon__help"></div>
                    </div>
                    <div class="border__icon32 tooltip-title-dialog" tooltip-content="Đóng (ESC)" @click="checkDataChange">
                        <div class="icon icon__close popup--close"></div>
                    </div>
                </div>
            </div>
            <div class="dialog__container">
                <div class="dialog__content">
                    <div class="dialog__content__up">
                        <div class="dialog__content__left">
                            <div class="dialog__row">
                                <button tabIndex="1" @keyup="returnLastTabIndex" class="button_hidden"></button>
                                <BaseInput ref="inputCode" tabIndex="2" :label="'Mã'" :inputRequire="true" :size="'p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="organization.OrganizationCode"> </BaseInput>
                            </div>
                        </div>
                        <div class="dialog__content__right">
                            <div class="dialog__row">
                                <BaseInput ref="inputName" :label="'Tên'" tabIndex="3" :inputRequire="true" :size="''" :maxLengthInput="100" :hasTooltip="true" v-model="organization.OrganizationName"> </BaseInput>
                            </div>
                        </div>
                    </div>
                    <div class="dialog__content__down">
                        <div class="dialog__row">
                            <!-- Combobox khối dự án -->
                            <BaseCombobox
                                ref="inputOrganization"
                                :label="'Phòng Ban'"
                                :tabIndex="5"
                                :inputRequire="true"
                                :inputIcon="'icon__dropdown'"
                                :dropdownTitleLeft="'Mã phòng ban'"
                                :dropdownTitleRight="'Tên phòng ban'"
                                :dataSelectedText="organization.DepartmentName"
                                :dataSelectedId="organization.DepartmentId"
                                :data="lstDepartment"
                                :idData="'DepartmentId'"
                                :codeData="'DepartmentCode'"
                                :nameData="'DepartmentName'"
                                :hasTooltip="true"
                                @setDepartmentName="setDepartmentName"
                            >
                            </BaseCombobox>
                        </div>
                    </div>
                </div>
                <div class="dialog__footer">
                    <div class="dialog__line"></div>
                    <div class="dialog__footer__button">
                        <div class="dialog__footer__left">
                            <BaseButton ref="btnDeny" tabIndex="22" :text="'Hủy'" :type="'button__outline button__outline__dialog'" @click="checkDataChange"> </BaseButton>
                        </div>

                        <div class="dialog__footer__right flex">
                            <div style="padding: 0 10px">
                                <BaseButton tabIndex="20" tooltip-content="Cất (Ctrl + S)" :text="'Cất'" :type="'button__outline button__outline__dialog tooltip-title-dialog'" @click="btnSaveOnClick(this.saveMode.saveDefault)"> </BaseButton>
                            </div>
                            <div>
                                <BaseButton tabIndex="21" tooltip-content="Cất và thêm (Ctrl + Shift + S)" :text="'Cất và Thêm'" :type="'button__dialog tooltip-title-dialog'" @click="btnSaveOnClick(this.saveMode.saveAndAdd)"> </BaseButton>
                                <button tabIndex="23" @keyup="returnFirstTabIndex" class="button_hidden"></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <BaseToastMessage ref="toastMesDetail" :toastContent="toastContent" :toastType="toastType" :toastDuration="toastDuration"> </BaseToastMessage>
        <BasePopup
            :isShowPopup="isShowPopup"
            :popupText="popupNotification"
            :iconType="popupType"
            :buttonType="popupButtonType"
            @showPopup="showOrganizationDetailPopup"
            @closeDetail="btnCloseOnClick"
            @saveOrganization="btnSaveOnClick(this.saveMode.saveDefault)"
        >
        </BasePopup>
    </div>
</template>

<style scoped>
@import url('../../../css/base/dialog.css');
</style>
