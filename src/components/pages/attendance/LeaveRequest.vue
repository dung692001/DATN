<script>
export default {
    name: 'LeaveRequest',
    emits: ['showLeaveDialog'],
    mounted() {
        // nếu là mode edit thì thực hiện lấy thông tin của nhân viên
        if (this.formMode === 1) {
            //this.getOrganization(this.organizationSelectedId);
        }
        // nếu là mode clone thì thực hiện lấy thông tin của nhân viên sau đó gán mã mơi
        if (this.formMode === 2) {
            //.getOrganization(this.organizationSelectedId);
            //this.organization.OrganizationCode = '';
            //this.getNewCode();
        }
    },
    props: {
        // biến bool hiển thị việc ẩn hiện dialog detail
        isShow: {
            type: Boolean
        },
        // id của nhân viên đang được chọn
        leaveSelectedId: {
            type: String
        },
        // lưu mode của dialog là thêm hay sửa
        formMode: {
            type: [Object, Number],
            required: true
        }
    },
    data() {
        return {
            leaveData: {}
        };
    },
    methods: {
        /**
         * Phương thức xử lý sự kiện khi ấn đóng dialog
         * @Author NDDung (23/07/2022)
         */
        btnCloseOnClick() {
            this.$emit('showLeaveDialog', false);
        }
    }
};
</script>
<template>
    <div id="dlgOrganizationDetail" class="dialog__popup" @keydown="hanldeOnKeyDown" @keyup="hanldeOnKeyUp" v-bind:class="{ isShowDialog: true }">
        <div class="dialog">
            <div class="dialog__header">
                <div class="dialog__title flex">
                    <div class="title__popup">{{ formMode != 1 ? 'Thêm đơn xin nghỉ' : 'Sửa đơn xin nghỉ' }}</div>
                </div>
                <div class="dialog__header__button">
                    <div class="border__icon32 tooltip-title-dialog" tooltip-content="Giúp (F1)">
                        <div class="icon icon__help"></div>
                    </div>
                    <div class="border__icon32 tooltip-title-dialog" tooltip-content="Đóng (ESC)" @click="btnCloseOnClick">
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
                                <div class="dialog__item w-60">
                                    <div class="bold__label dialog__label">
                                        <label for="">Từ ngày</label>
                                    </div>
                                    <div class="cboIdentityDate">
                                        <BaseMSDate ref="inputIdentityDate" :dateSelect="leaveData.IdentifyIssuedDate" :tabIndex="14" @setDateTime="setIdentityDate" @showErrorInput="showErrorInput"> </BaseMSDate>
                                    </div>
                                </div>
                            </div>
                            <div class="dialog__row">
                                <button tabIndex="1" @keyup="returnLastTabIndex" class="button_hidden"></button>
                                <div class="dialog__item w-70">
                                    <div class="bold__label dialog__label">
                                        <label for="">Đến ngày</label>
                                    </div>
                                    <div class="cboIdentityDate">
                                        <BaseMSDate ref="inputIdentityDate" :dateSelect="leaveData.IdentifyIssuedDate" :tabIndex="14" @setDateTime="setIdentityDate" @showErrorInput="showErrorInput"> </BaseMSDate>
                                    </div>
                                </div>
                            </div>
                            <div class="dialog__row">
                                <BaseInput tabIndex="15" :label="'Số ngày nghỉ'" :maxLengthInput="255" v-model="leaveData.IdentifyIssuePlace"> </BaseInput>
                            </div>
                            <div class="dialog__row">
                                <BaseInput tabIndex="15" :label="'Loại nghỉ'" :maxLengthInput="255" v-model="leaveData.IdentifyIssuePlace"> </BaseInput>
                            </div>
                            <div class="dialog__row">
                                <BaseInput tabIndex="15" :label="'Tổng nghỉ phép cả năm'" :maxLengthInput="255" v-model="leaveData.IdentifyIssuePlace"> </BaseInput>
                            </div>
                            <div class="dialog__row">
                                <BaseInput tabIndex="15" :label="'Số NP đã nghỉ'" :maxLengthInput="255" v-model="leaveData.IdentifyIssuePlace"> </BaseInput>
                            </div>
                        </div>
                        <div class="dialog__content__right">
                            <div class="dialog__row">
                                <BaseInput tabIndex="15" :label="'Số NP còn lại'" :maxLengthInput="255" v-model="leaveData.IdentifyIssuePlace"> </BaseInput>
                            </div>
                            <div class="dialog__row">
                                <BaseInput tabIndex="15" :label="'Số NP được nghỉ'" :maxLengthInput="255" v-model="leaveData.IdentifyIssuePlace"> </BaseInput>
                            </div>
                            <div class="dialog__row">
                                <BaseInput tabIndex="15" :label="'Tỷ lệ hưởng lương'" :maxLengthInput="255" v-model="leaveData.IdentifyIssuePlace"> </BaseInput>
                            </div>
                            <div class="dialog__row">
                                <BaseInput tabIndex="15" :label="'Lý do nghỉ'" :maxLengthInput="255" v-model="leaveData.IdentifyIssuePlace"> </BaseInput>
                            </div>
                            <div class="dialog__row">
                                <BaseInput tabIndex="15" :label="'Người duyệt'" :maxLengthInput="255" v-model="leaveData.IdentifyIssuePlace"> </BaseInput>
                            </div>
                            <div class="dialog__row">
                                <BaseInput tabIndex="15" :label="'Người thay thế'" :maxLengthInput="255" v-model="leaveData.IdentifyIssuePlace"> </BaseInput>
                            </div>
                        </div>
                    </div>
                    <div class="dialog__content__down">
                        <div class="dialog__row">
                            <BaseInput tabIndex="15" :label="'Ghi chú'" :maxLengthInput="255" v-model="leaveData.IdentifyIssuePlace"> </BaseInput>
                        </div>
                    </div>
                </div>
                <div class="dialog__footer">
                    <div class="dialog__line"></div>
                    <div class="dialog__footer__button">
                        <div class="dialog__footer__left">
                            <BaseButton ref="btnDeny" tabIndex="22" :text="'Hủy'" :type="'button__outline button__outline__dialog'" @click="btnCloseOnClick"> </BaseButton>
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
