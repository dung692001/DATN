<script>
import EmployeeApi from '../../../apis/EmployeeApi.js';
import { genderEmployee } from '../../../common/Enum.js';
import { genderNameEmployee } from '../../../common/Enum.js';
// import { resourceVN } from '../../../common/Resource.js';
// import { Resource } from '../../../common/Resource.js';
export default {
    components: {},
    mounted() {
        this.getEmployee(this.$store.state.employeeId);
    },
    data() {
        return {
            id: '',
            dataLoadImg: '',
            text: 'a',
            employee: {},
            employeeBeforeEdit: {},
            isDisable: true,
            dropdownValues: [
                { name: 'Nam', code: 0 },
                { name: 'Nữ', code: 1 },
                { name: 'khác', code: 2 }
            ],
            genderSelected: '',
            employeeCode: '',
            employeeName: '',
            positionsName: '',
            organizationName: ''
        };
    },
    methods: {
        setPayloadToast(content, type, duration) {
            this.payloadToast = {
                toastContent: content,
                toastType: type,
                toastDuration: duration
            };
        },

        getImgId(value) {
            this.id = value;
        },

        /**
         * Phương thức get employee từ employeeId
         * @param employeeId: id truyền vào để lấy toàn bộ thông tin nhân viên
         * @Author NDDung (25/07/2022)
         */
        async getEmployee(employeeId) {
            let me = this;
            try {
                await EmployeeApi.getEmployeeById(employeeId).then(
                    (res) => {
                        me.employee = res.data;
                        me.dataLoadImg = 'images/' + me.employee.Avatar;
                        let genderName = '';
                        if (me.employee.Gender == 0) {
                            genderName = 'Nam';
                        } else if (me.employee.Gender == 1) {
                            genderName = 'Nữ';
                        } else if (me.employee.Gender == 2) {
                            genderName = 'Khác';
                        }
                        me.genderSelected = {
                            name: genderName,
                            code: me.employee.Gender
                        };
                        me.employeeCode = me.employee.EmployeeCode;
                        me.employeeName = me.employee.EmployeeName;
                        me.positionsName = me.employee.PositionsName;
                        me.organizationName = me.employee.OrganizationName;
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
            if (status == true) {
                this.employeeBeforeEdit = JSON.parse(JSON.stringify(this.employee));
                this.isDisable = !status;
            } else {
                this.isDisable = !status;
            }
        },

        denySaveOnClick() {
            this.isDisable = true;
            this.employee = JSON.parse(JSON.stringify(this.employeeBeforeEdit));
        },

        btnSaveOnClick() {
            try {
                let me = this;

                // format lại DateOfBirth và IdentityDate
                me.employee.DateOfBirth = me.formatDate(me.employee.DateOfBirth);
                me.employee.IdentifyIssuedDate = me.formatDate(me.employee.IdentifyIssuedDate);
                // Thực hiên sửa nhân viên
                EmployeeApi.saveEmployeeByEdit(me.$store.state.employeeId, me.employee).then(
                    () => {
                        // Nếu sửa thành công hiện thị toast msg thông báo thành công:
                        me.setPayloadToast('Sửa thông tin thành công!', 'success', 2000);
                        me.$store.dispatch('showToast', me.payloadToast);
                        me.changeEditStatus(me.isDisable);
                    },
                    (err) => {
                        me.setPayloadToast('Sửa thất bại!', 'error', 2000);
                        me.$store.dispatch('showToast', me.payloadToast);
                        console.log(err);
                    }
                );
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức format date để cất dữ liệu
         * @param date Ngày tháng truyền vào
         * @return Định dạng năm tháng ngày
         * @Author NDDung (27/07/2022)
         */
        formatDate(date) {
            try {
                if (date) {
                    date = new Date(date);
                    let newDate = date.getDate();
                    let month = date.getMonth() + 1;
                    let year = date.getFullYear();
                    if (month <= 9) {
                        month = '0' + month;
                    }
                    if (newDate <= 9) {
                        newDate = '0' + newDate;
                    }
                    return `${year}/${month}/${newDate}`;
                }
            } catch (error) {
                console.log(error);
                return '';
            }
        }
    },
    watch: {
        genderSelected: function (value) {
            this.employee.Gender = value.code;
        }
    }
};
</script>

<template>
    <div class="employee-component">
        <div class="user-profile-header">
            <div class="view-header-content">
                <div class="profile-info-header">
                    <div class="user-avatar"><BaseDownload :urlLink="dataLoadImg"> </BaseDownload></div>
                    <div class="user-info">
                        <div class="header-user-name">
                            <div class="user-name">{{ employeeName }}</div>
                            <div class="user-code">({{ employeeCode }})</div>
                        </div>
                        <div class="header-more-info">
                            <span>{{ positionsName }}</span>
                            <span class="dash-info"> - </span>
                            <span>{{ organizationName }}</span>
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
                        <BaseButton :text="'Hủy'" :type="'button__outline button__outline__dialog'" @click="denySaveOnClick()"> </BaseButton>
                    </div>
                    <div class="btn-save">
                        <BaseButton tooltip-content="Lưu" :text="'Lưu'" :type="'button__dialog tooltip-title-dialog'" @click="btnSaveOnClick()" :class="{ 'hide-btn': isDisable }"> </BaseButton>
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
                            <div class="user-profile-dropdown w-21">
                                <div class=""><label class="dialog__lable" for="">Giới tính </label></div>
                                <Dropdown v-model="genderSelected" :options="dropdownValues" optionLabel="name" placeholder="Select" class="user-dropdown p-r-6" :disabled="isDisable" />
                            </div>
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
                            <BaseInput :label="'Facebook'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.Facebook" :isDisabled="isDisable"> </BaseInput>
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
                            <BaseInput :label="'Quan hệ'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.ContactRelationship" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'ĐT di động'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.ContactNumber" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Địa chỉ'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.ContactAddress" :isDisabled="isDisable"> </BaseInput>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail" :class="[{ 'hide-when-edit': !isDisable }]">
                        <div class="user-sheet-content-title">Thông tin công việc</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Đơn vị công tác'" :inputRequire="true" :size="'w-47 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.OrganizationName" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Địa chỉ làm việc'" :inputRequire="true" :size="'w-47 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.WorkAddress" :isDisabled="isDisable"> </BaseInput>
                        </div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Vị trí công việc'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.PositionsName" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Loại hợp đồng'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.ContractType" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Trạng thái lao động'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.WorkStatus" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Tính chất lao động'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.LaborNature" :isDisabled="isDisable"> </BaseInput>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail" :class="[{ 'hide-when-edit': !isDisable }]">
                        <div class="user-sheet-content-title">Thông tin lương</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Lương thử việc'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.ProbationarySalary" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Lương thỏa thuận chính thức'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.Salary" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Lương cơ bản'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.SalaryInsurance" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Thưởng hiệu quả'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.BonusSalary" :isDisabled="isDisable"> </BaseInput>
                        </div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'TK ngân hàng'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.BankNumber" :isDisabled="isDisable"> </BaseInput>
                            <BaseInput :label="'Ngân hàng'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="employee.BankName" :isDisabled="isDisable"> </BaseInput>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail" :class="[{ 'hide-when-edit': !isDisable }]">
                        <div class="user-sheet-content-title">Thông tin bảo hiểm</div>
                        <div class="user-profile-detai-row">
                            <BaseInput :label="'Mã số BHXH'" :inputRequire="true" :size="'w-21 p-r-6'" :maxLengthInput="20" :hasTooltip="true" v-model="text" :isDisabled="isDisable"> </BaseInput>
                        </div>
                    </div>
                    <div class="user-sheet-content-detail" :class="[{ 'hide-when-edit': !isDisable }]">
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
