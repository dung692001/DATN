<script>
import AttendanceStatistApi from '../../../apis/AttendanceStatistApi.js';
import LeaveRequest from './LeaveRequest.vue';
import { eventMode } from '../../../common/Enum.js';
export default {
    name: 'AttendanceStatist',
    components: {
        LeaveRequest
    },
    mounted() {
        var currentDate = new Date();
        this.yearSelected = currentDate.getFullYear();
        this.getAttendance(this.$store.state.employeeId, this.yearSelected);
    },
    data() {
        return {
            attendance: {},
            yearSelected: 0,
            leaveSelectedId: '',
            isShowDialog: false,
            formMode: 0
        };
    },
    methods: {
        /**
         * Phương thức get employee từ employeeId
         * @param employeeId: id truyền vào để lấy toàn bộ thông tin nhân viên
         * @Author NDDung (25/07/2022)
         */
        async getAttendance(employeeId, workYear) {
            let me = this;
            try {
                await AttendanceStatistApi.getAttendanceStatistByWorkYear(employeeId, workYear).then(
                    (res) => {
                        me.attendance = res.data;
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
         * Phương thức đóng mở dialog
         * @param isShowDialog isShowDialog = true mở form và false là đóng form
         * @Author NDDung (23/07/2022)
         */
        showLeaveDialog(isShowDialog) {
            this.isShowDialog = isShowDialog;
        },

        /**
         * Hiển thị form chi tiết nhân viên ấn nút thêm mới nhân viên
         * @Author NDDung (23/07/2022)
         */
        btnAddOnClick() {
            this.leaveSelectedId = '';
            this.showLeaveDialog(true);
            this.formMode = eventMode.add;
        }
    },
    watch: {
        yearSelected: function () {
            this.getAttendance(this.$store.state.employeeId, this.yearSelected);
        }
    }
};
</script>
<template>
    <div class="attendance-component">
        <div class="attendance-header">
            <div class="attendance-title">Đơn xin nghỉ</div>
            <div class="attendance-toolbar">
                <div class="attendance-work-year">
                    <input type="number" class="work-year" v-model="yearSelected" />
                </div>

                <BaseButton :text="'Thêm'" @click="btnAddOnClick"> </BaseButton>
            </div>
        </div>
        <div class="attendance-content">
            <div class="attendance-info">
                <div class="attendance-leave-info">
                    <div class="leave-info-content">
                        <div class="leave-info-title">Số ngày phép cả năm</div>
                        <div class="leave-info-date">
                            <div class="date-info">{{ attendance.TotalLeaveDay }}</div>
                        </div>
                    </div>
                </div>
                <div class="attendance-leave-info">
                    <div class="leave-info-content">
                        <div class="leave-info-title">Số ngày phép đã nghỉ</div>
                        <div class="leave-info-date">
                            <div class="date-info">{{ attendance.TotalUsedLeaveDay }}</div>
                        </div>
                    </div>
                </div>
                <div class="attendance-leave-info">
                    <div class="leave-info-content">
                        <div class="leave-info-title">Số ngày phép còn lại</div>
                        <div class="leave-info-date">
                            <div class="date-info">{{ attendance.RemainLeaveDay }}</div>
                        </div>
                    </div>
                </div>
                <div class="attendance-leave-info">
                    <div class="leave-info-content">
                        <div class="leave-info-title">Số ngày phép được sử dụng</div>
                        <div class="leave-info-date">
                            <div class="date-info">{{ attendance.TotalUseLeaveDayToMonth }}</div>
                        </div>
                    </div>
                </div>
                <div class="attendance-leave-info">
                    <div class="leave-info-content">
                        <div class="leave-info-title">Số ngày nghỉ không lương</div>
                        <div class="leave-info-date">
                            <div class="date-info">{{ attendance.TotalAttendanceNoSalary }}</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="leave-content">
                <div class="leave-content-container">
                    <div class="leave-content-left"></div>
                    <div class="leave-content-right">
                        <div class="content__main">
                            <div class="content__table">
                                <div class="table">
                                    <table id="tbPositionsList">
                                        <thead>
                                            <th class="w-100px">Từ ngày</th>
                                            <th class="w-100px">Đến ngày</th>
                                            <th class="w-100px table__right">Số ngày nghỉ</th>
                                            <th class="w-100px">Loại nghỉ</th>
                                            <th class="w-200px">Lý do nghỉ</th>
                                            <th class="w-100px table__center">Trạng thái</th>
                                            <th class="w-100px table__center">Chức năng</th>
                                        </thead>
                                        <tbody>
                                            <!-- Dùng v-for để binding dữ liệu -->
                                            <tr
                                                v-for="(emp, index) in positions"
                                                :key="emp"
                                                :class="{
                                                    table__item: true,
                                                    'row--selected': toggleRowSelected(emp.PositionsId),
                                                    'rows--checked': tickCheckBoxSelected(index, emp.PositionsId)
                                                }"
                                                @click="selectedRow(emp.PositionsId)"
                                                @dblclick="btnEditOnClick(emp)"
                                            >
                                                <td class="tbPositionsCode">{{ emp.PositionsCode }}</td>
                                                <td>{{ emp.PositionsName }}</td>
                                                <td class="table__center">
                                                    <div class="table__combo__button flex">
                                                        <div @click="btnEditOnClick(emp)">
                                                            <button class="button__edit btnEdit">Sửa</button>
                                                        </div>

                                                        <div :class="['button__material btnMaterial', emp.PositionsId == positionContextMenuId ? 'button__material__border' : '']" @click="btnContextMenuOnClick(emp.PositionsId)">
                                                            <button class="icon icon__table__material button24"></button>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="table__popup">
                                                    <BaseContextMenu
                                                        :class="{ isShowBlock: checkShowContextMenu(emp.PositionsId) }"
                                                        :positionCode="emp.PositionsCode"
                                                        :position="emp"
                                                        @showWarningPopup="showWarningPopup"
                                                        @btnCloneOnClick="btnCloneOnClick"
                                                    >
                                                    </BaseContextMenu>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <!-- Phần footer phân trang -->
                            <div class="content__footer">
                                <p>
                                    Tổng số:
                                    <span class="footer__total__record">{{ totalRecord }}</span> bản ghi
                                </p>
                                <div class="footer__right flex">
                                    <div class="footer__dropdown dropdown dropdown__page">
                                        <BaseInput ref="inputPositions" :isShowDropdown="isShowDropdown" :inputIcon="'icon__dropdown'" :hasIcon="true" :isReadOnly="true" @showDropdown="showRecordInPageDropdown" v-model="recordInPage">
                                            <BaseDropdown
                                                :isShow="isShowDropdown"
                                                :dropdownKind="'dropdown__data'"
                                                :contentArray="[10, 20, 30, 50, 100]"
                                                :pageSize="recordNumber"
                                                :dropdownText="' bản ghi trên 1 trang'"
                                                @showDropdown="showRecordInPageDropdown"
                                            >
                                            </BaseDropdown>
                                        </BaseInput>
                                    </div>
                                    <div class="footer__button">
                                        <button :class="[currentPage == 1 ? 'button--disable' : '']" @click="prevPageOnClick">Trước</button>
                                        <div class="footer__page flex">
                                            <BaseButtonPaging
                                                v-for="item in totalPage"
                                                :key="item"
                                                :textButton="item == 1 || item == totalPage || item < currentPage + 3 ? item : '...'"
                                                @click="btnPagingOnClick(item)"
                                                :class="[
                                                    item == currentPage ? 'btn__focus isShowBlock' : '',
                                                    item == 1 ? 'isShowBlock' : '',
                                                    (item > currentPage + 3 && item < totalPage) || (item < currentPage - 1 && item < totalPage - 3) ? 'display__none' : ''
                                                ]"
                                            >
                                            </BaseButtonPaging>
                                        </div>
                                        <button :class="[currentPage == totalPage ? 'button--disable' : '']" @click="nextPageOnClick">Sau</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <LeaveRequest v-if="isShowDialog" :leaveSelectedId="leaveSelectedId" :formMode="formMode" @showLeaveDialog="showLeaveDialog" @btnAddOnClick="btnAddOnClick" />
</template>

<style lang="scss">
@import './AttendanceStatist.scss';
</style>
