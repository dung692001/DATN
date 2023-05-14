<script>
import EmployeeApi from '../../../apis/EmployeeApi.js';
import { genderEmployee } from '../../../common/Enum.js';
import { genderNameEmployee } from '../../../common/Enum.js';
// import { resourceVN } from '../../../common/Resource.js';
// import { Resource } from '../../../common/Resource.js';
export default {
    components: {},
    mounted() {
        this.getEmployee('ace035fc-e8dd-11ed-b40b-0897988259b7');
    },
    data() {
        return {
            id: '',
            dataLoadImg: 'images/ec497542-3b26-4f66-8ed7-1383e442ed1c',
            text: 'a',
            employee: {},
            isDisable: true
        };
    },
    methods: {
        getImgId(value) {
            this.id = value;
        },

        /**
         * Phương thức get employee từ employeeId
         * @param employeeId: id truyền vào để lấy toàn bộ thông tin nhân viên
         * @Author NDDung (25/07/2022)
         */
        async getEmployee(employeeId) {
            let currentWindow = this;
            try {
                await EmployeeApi.getEmployeeById(employeeId).then(
                    (res) => {
                        currentWindow.employee = res.data;
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
         * Phương thức format gender
         * @param gender Giới tính
         * @Author NDDung (31/07/2022)
         */
        formatGender(gender) {
            var genderName = '';
            if (gender == genderEmployee.male) {
                genderName = genderNameEmployee.male;
            } else if (gender == genderEmployee.feMale) {
                genderName = genderNameEmployee.feMale;
            } else {
                genderName = genderNameEmployee.other;
            }
            return genderName;
        },

        /**
         * Sự kiện thay đổi khi chọn ngày sinh
         * @param e: kết quả khi chọn datebox date of birth
         * @Author NDDung (27/08/2022)
         */
        setDateOfBirth(e) {
            this.employee.DateOfBirth = e;
        },

        /**
         * Sự kiện thay đổi khi chọn ngày cấp
         * @param e: kết quả khi chọn datebox identityDate
         * @Author NDDung (21/08/2022)
         */
        setIdentityDate(e) {
            this.employee.IdentifyIssuedDate = e;
        },

        changeEditStatus(status) {
            this.isDisable = !status;
        }
    }
};
</script>

<template>
    <div class="employee-component">
        <div class="user-profile-header">
            <div class="view-header-content">
                <div class="profile-info-header">
                    <div class="user-avatar"><BaseDownload :linkImg="dataLoadImg"> </BaseDownload></div>
                    <div class="user-info">
                        <div class="header-user-name">
                            <div class="user-name">Nguyễn Dăng Dũng</div>
                            <div class="user-code">(B1099)</div>
                        </div>
                        <div class="header-more-info">
                            <span>Lập trình viên</span>
                            <span class="dash-info"> - </span>
                            <span>Quản lý ngân sách</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="header-custom-button">
                <div class="turn-on-edit" :class="{ 'hide-btn': !isDisable }">
                    <BaseButton :type="'button__batch__execution flex'" :text="'Cập nhật thông tin'" @click="changeEditStatus(isDisable)">
                        <div class="dropdown__icon__item flex">
                            <div class="icon icon__edit"></div>
                        </div>
                    </BaseButton>
                </div>

                <div class="btn-when-edit">
                    <div class="btn-deny" :class="{ 'hide-btn': isDisable }">
                        <BaseButton :text="'Hủy'" :type="'button__outline button__outline__dialog'" @click="changeEditStatus(isDisable)"> </BaseButton>
                    </div>
                    <div class="btn-save">
                        <BaseButton tooltip-content="Lưu" :text="'Lưu'" :type="'button__dialog tooltip-title-dialog'" @click="changeEditStatus(isDisable)" :class="{ 'hide-btn': isDisable }"> </BaseButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="user-profile-content">
            <div class="user-profile-sheet">
                <div class="user-profile-sheet-header">Thông tin cơ bản</div>
                <div class="user-profile-sheet-content">
                    <div class="user-sheet-content-detail">
                        <div class="user-sheet-content-title">Thông tin chung</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Mã nhân viên'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.EmployeeCode" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Họ và Tên'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.EmployeeName" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Giới tính'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.Gender" :isDisabled="isDisable"> </BaseInput>
                            <div class="user-date-content">
                                <div class="dialog__item">
                                    <div class="bold__label dialog__label">
                                        <label for="">Ngày sinh</label>
                                    </div>
                                    <div class="cboDateOfBirth">
                                        <BaseMSDate ref="inputDateOfBirth" :dateSelect="employee.DateOfBirth" :tabIndex="12" @setDateTime="setDateOfBirth" @showErrorInput="showErrorInput" :isDisabled="isDisable"> </BaseMSDate>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail">
                        <div class="user-sheet-content-title">Thẻ căn cước</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Loại giấy tờ'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Số CCCD'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.IdentifyNumber" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Nơi cấp'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.IdentifyIssuePlace" :isDisabled="isDisable"> </BaseInput>
                            <div class="user-date-content">
                                <div class="dialog__item">
                                    <div class="bold__label dialog__label">
                                        <label for="">Ngày cấp</label>
                                    </div>
                                    <div class="cboDateOfBirth">
                                        <BaseMSDate ref="inputDateOfBirth" :dateSelect="employee.IdentifyIssuedDate" :tabIndex="12" @setDateTime="setDateOfBirth" @showErrorInput="showErrorInput" :isDisabled="isDisable"> </BaseMSDate>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail">
                        <div class="user-sheet-content-title">Thông tin liên hệ</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'ĐT di động'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.PhoneNumber" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Email cá nhân'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.Email" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Email cơ quan'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.OfficeEmail" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Facebook'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail">
                        <div class="user-sheet-content-title">Thông tin nơi ở</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Địa chỉ'" :inputRequire="true" :size="'w-47 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.Address" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Địa chỉ thường trú'" :inputRequire="true" :size="'w-47 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.CurrentAddress" :isDisabled="isDisable"> </BaseInput>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail">
                        <div class="user-sheet-content-title">Liên hệ khẩn cấp</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Họ và Tên'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.ContactName" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Quan hệ'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'ĐT di động'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.ContactNumber" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Địa chỉ'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail">
                        <div class="user-sheet-content-title">Thông tin công việc</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Đơn vị công tác'" :inputRequire="true" :size="'w-47 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.OrganizationName" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Địa chỉ làm việc'" :inputRequire="true" :size="'w-47 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                        </div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Vị trí công việc'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.PositionsName" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Loại hợp đồng'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Trạng thái lao động'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Tính chất lao động'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail">
                        <div class="user-sheet-content-title">Thông tin lương</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Lương thử việc'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Lương thỏa thuận chính thức'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Lương cơ bản'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Thưởng hiệu quả'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                        </div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'TK ngân hàng'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.BankNumber" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Ngân hàng'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail">
                        <div class="user-sheet-content-title">Thông tin bảo hiểm</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Mã số BHXH'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail">
                        <div class="user-sheet-content-title">Thông tin thuế</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Mã số thuế'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.TaxCode" :isDisabled="isDisable"> </BaseInput>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style lang="scss">
@import './EmployeeProfile.scss';
</style>
