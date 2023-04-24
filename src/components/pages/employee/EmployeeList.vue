<script>
import EmployeeDetail from './EmployeeDetail.vue';
import EmployeeApi from '../../../apis/EmployeeApi.js';
import axios from 'axios';
import { resourceVN } from '../../../common/Resource.js';
import { genderEmployee } from '../../../common/Enum.js';
import { genderNameEmployee } from '../../../common/Enum.js';
import { eventMode } from '../../../common/Enum.js';
import { deleteMode } from '../../../common/Enum.js';
import { myApi } from '../../../common/Resource.js';

export default {
    name: 'EmployeeList',
    components: {
        EmployeeDetail
    },

    //GĐ 2: created (setup)
    created() {
        this.loadDataByFilter();
    },
    watch: {
        totalRecord: function (value) {
            // khởi tạo mảng rỗng
            this.arrayCheckBoxSelectedId = new Array(value).fill(null);
        }
    },
    methods: {
        /**
         * Thưc hiện việc load data
         * @Author NDDung (23/07/2022)
         */
        loadData() {
            var currentWindow = this;
            // Gọi api thực hiện lấy dữ liệu
            try {
                this.isShowLoading = true;
                EmployeeApi.getAllEmployee().then(
                    (res) => {
                        currentWindow.employees = res.data;
                        currentWindow.totalRecord = res.data.length;
                        this.isShowLoading = false;
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
         * Thưc hiện việc load data bởi số bản ghi 1 trang và trang số
         * @Author NDDung (23/07/2022)
         */
        async loadDataByFilter() {
            var currentWindow = this;
            // Gọi api thực hiện lấy dữ liệu
            if (currentWindow.currentSearchValue == null) {
                currentWindow.currentSearchValue = '';
            }
            try {
                this.isShowLoading = false;
                await EmployeeApi.loadDataByFilter(this.recordNumber, this.currentPage, currentWindow.currentSearchValue).then(
                    (res) => {
                        currentWindow.totalPage = res.data.TotalPage;
                        currentWindow.employees = res.data.Data;
                        currentWindow.totalRecord = res.data.TotalRecord;
                        currentWindow.isShowLoading = false;
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
         * Hiển thị form chi tiết nhân viên ấn nút thêm mới nhân viên
         * @Author NDDung (23/07/2022)
         */
        btnAddOnClick() {
            this.employeeSelectedId = '';
            this.showEmployeeDetailDialog(true);
            this.formMode = eventMode.add;
        },

        /**
         * Hiển thị form chi tiết nhân viên khi ấn nút sửa tại dòng dữ liệu
         * @param employee thông tin nhân viên
         * @Author NDDung (23/07/2022)
         */
        btnEditOnClick(employee) {
            this.showEmployeeDetailDialog(true);
            this.employeeSelectedId = employee.EmployeeId;
            this.employeeContextMenuId = '';
            this.formMode = eventMode.edit;
        },

        /**
         * Phương thức đóng mở dialog
         * @param isShowDialog isShowDialog = true mở form và false là đóng form
         * @Author NDDung (23/07/2022)
         */
        showEmployeeDetailDialog(isShowDialog) {
            this.isShowDialog = isShowDialog;
        },

        /**
         * Phương thức đóng mở loading
         * @param isShowDialog isShowLoading = true hiện loading và false là ẩn loading
         * @Author NDDung (27/07/2022)
         */
        showLoading(isShowLoading) {
            this.isShowLoading = isShowLoading;
        },

        /**
         * Hiển thị viền bao quanh nút context menu
         * @param employeeId Khóa chính của nhân viên
         * @Author NDDung (25/07/2022)
         */
        btnContextMenuOnClick(employeeId) {
            if (this.employeeContextMenuId == employeeId) {
                this.employeeContextMenuId = '';
            } else {
                this.employeeContextMenuId = employeeId;
            }
        },

        /**
         * ẩn hiện context menu
         * @param employeeId Khóa chính của nhân viên
         * @Author NDDung (25/07/2022)
         */
        checkShowContextMenu(employeeId) {
            if (this.employeeContextMenuId == employeeId) {
                return true;
            } else {
                return false;
            }
        },

        /**
         * Phương thức định dạng lại ngày tháng
         * @param date Ngày tháng truyền vào
         * @return Định dạng ngày tháng năm
         * @Author NDDung (23/07/2022)
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
                    return `${newDate}/${month}/${year}`;
                }
            } catch (error) {
                console.log(error);
                return '';
            }
        },

        /**
         * Phương thức mở popup warning
         * @param isShow kiểu true false để mở popup warning
         * @param popupMessage Nội dung popup warning
         * @param deleteMode Trạng thái của popup warning
         * @Author NDDung (25/07/2022)
         */
        showWarningPopup(isShow, popupMessage, deleteMode) {
            this.popupNotification = [];
            this.isShowPopup = isShow;
            this.popupNotification.push(popupMessage);
            this.deleteStatus = deleteMode;
        },

        /**
         * Phương thức xử lí sự kiện xác nhận xóa
         * @Author NDDung (25/07/2022)
         */
        async confirmDelete() {
            // Khi xóa đơn
            if (this.deleteStatus == 0) {
                let currentWindow = this;
                this.setToastMessage('Xóa thành công!', 'success', 2000);
                try {
                    await EmployeeApi.deleteEmployee(currentWindow.employeeContextMenuId).then(
                        (res) => {
                            res;
                            this.loadDataByFilter();
                            this.showToastMessage();
                            // kiểm tra bản ghi vừa xóa có nằm trong danh sách xóa nhiều không
                            const temp = currentWindow.arrayCheckBoxSelectedId.filter(function (e) {
                                return e !== currentWindow.employeeContextMenuId;
                            });
                            if (temp != undefined) {
                                currentWindow.arrayCheckBoxSelectedId = temp;
                            }

                            currentWindow.isShowButtonDeleteMulti();
                        },
                        (err) => {
                            console.log(err);
                        }
                    );
                } catch (error) {
                    console.log(error);
                }
                // currentWindow.arrayCheckBoxSelectedId.forEach(function (value, index) {
                //   if(value != null){
                //     currentWindow.tickCheckBoxSelected(index, value);
                //   }
                //})
            }
            // Khí xóa nhiều bản ghi
            else {
                this.deleteMultiEmployees();
            }
        },

        /**
         * Xử lý sự kiện load lại dữ liệu
         * @Author NDDung (27/07/2022)
         */
        btnRefreshOnClick() {
            this.loadDataByFilter();
        },

        /**
         * Phương thức tìm kiếm theo tên, mã nhân viên
         * @param value Kết quả của input search
         * @Author NDDung (28/07/2022)
         */
        searchEmployee(value) {
            let currentWindow = this;
            clearTimeout(this.countDown);
            currentWindow.countDown = setTimeout(function () {
                console.log(value);
                currentWindow.currentSearchValue = value;
                currentWindow.loadEmployeeByIdOrName();
            }, 1000);
        },

        /**
         * Thực hiện loadData theo tên hoặc id
         * @Author NDDung (28/07/2022)
         */
        loadEmployeeByIdOrName() {
            this.currentPage = 1;
            // Gọi api thực hiện lấy dữ liệu
            this.loadDataByFilter();
        },

        /**
         * Phương thức đóng mở dropdown chọn số bản ghi
         * @param isShow Biến ẩn hiện dropdown
         * @param value Số bản ghi đã chọn
         * @Author NDDung (29/07/2022)
         */
        showRecordInPageDropdown(isShow, value) {
            this.isShowDropdown = isShow;
            this.$refs.inputDepartment.isRorateIcon = false;
            if (value != '') {
                this.recordInPage = value + ' bản ghi 1 trang';
                this.recordNumber = value;
                this.currentPage = 1;
                this.loadDataByFilter();
            }
        },

        /**
         * Phương thức xử lý sự kiện ấn nút trước
         * @Author NDDung (31/07/2022)
         */
        prevPageOnClick() {
            if (this.currentPage > 1) {
                this.currentPage = this.currentPage - 1;
                this.loadDataByFilter();
            }
        },

        /**
         * Phương thức xử lý sự kiện ấn nút Sau
         * @Author NDDung (31/07/2022)
         */
        nextPageOnClick() {
            if (this.currentPage < this.totalPage) {
                this.currentPage = this.currentPage + 1;
                this.loadDataByFilter();
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
            }
            if (gender == genderEmployee.feMale) {
                genderName = genderNameEmployee.feMale;
            }
            if (gender == genderEmployee.other) {
                genderName = genderNameEmployee.other;
            }
            return genderName;
        },

        /**
         * Phương thức xử lý sự kiện chọn trang
         * @param item Số trang
         * @Author NDDung (31/07/2022)
         * */
        btnPagingOnClick(item) {
            this.currentPage = item;
            this.countCheckBoxCheckedInPage = 0;
            this.loadDataByFilter();
        },

        /**
         * Phương thức set toast message
         * @param content Nội dung toast message
         * @param type Kiểu toast message
         * @param durantion Thời gian
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
         * Phương thức export data ra file excel
         * @Author NDDung (03/08/2022)
         */
        btnExportExcel() {
            try {
                EmployeeApi.exportEmployee().then(
                    () => {},
                    (err) => {
                        console.log(err);
                    }
                );
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức xử lý sự kiện khi ấn nút nhân bản
         * @param employee Thông tin của nhân viên
         * @Author NDDung (03/08/2022)
         */
        btnCloneOnClick(employee) {
            this.showEmployeeDetailDialog(true);
            this.employeeSelectedId = employee.EmployeeId;
            this.employeeContextMenuId = '';
            this.formMode = eventMode.clone;
        },

        /**
         * Phương thức thêm hoặc xóa khóa chính của 1 nhân viên trong danh sách để xóa nhiều nhân viên
         * @param index index được truyền vào từ v-for
         * @param employeeId là khóa chính của nhân viên
         * @Author NDDung (16/08/2022)
         */
        tickCheckBox(index, employeeId) {
            try {
                let currentIndex = (this.currentPage - 1) * this.recordNumber + index;
                if (this.arrayCheckBoxSelectedId[currentIndex] == null) {
                    this.arrayCheckBoxSelectedId[currentIndex] = employeeId;
                    this.countIdInArray = this.countIdInArray + 1;
                } else {
                    this.arrayCheckBoxSelectedId[currentIndex] = null;
                    this.countIdInArray = this.countIdInArray - 1;
                }
                this.isShowButtonDeleteMulti();
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức trả về trạng thái của check box
         * @param index index được truyền vào từ v-for
         * @param employeeId là khóa chính của nhân viên
         * @return true là select
         * @return false là unSelect
         * @Author NDDung (16/08/2022)
         */
        tickCheckBoxSelected(index, employeeId) {
            try {
                let currentIndex = (this.currentPage - 1) * this.recordNumber + index;
                if (this.arrayCheckBoxSelectedId[currentIndex] == null) {
                    return false;
                }
                if (this.arrayCheckBoxSelectedId[currentIndex] == employeeId) {
                    return true;
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức thêm hoặc xóa khóa chính của nhiều nhân viên trong 1 trang
         * @Author NDDung (16/08/2022)
         */
        tickAllCheckBox() {
            try {
                let i = (this.currentPage - 1) * this.recordNumber;
                let maxNumberInPage = (this.currentPage - 1) * this.recordNumber + this.employees.length;
                let j = 0;
                if (this.countCheckBoxCheckedInPage != this.employees.length) {
                    for (i; i < maxNumberInPage; i++, j++) {
                        if (this.arrayCheckBoxSelectedId[i] != null) {
                            this.countIdInArray = this.countIdInArray - 1;
                        }
                        this.countIdInArray = this.countIdInArray + 1;
                        this.arrayCheckBoxSelectedId[i] = this.employees[j].EmployeeId;
                    }
                    this.countCheckBoxCheckedInPage = this.employees.length;
                } else {
                    for (i; i < maxNumberInPage; i++) {
                        this.arrayCheckBoxSelectedId[i] = null;
                        this.countIdInArray = this.countIdInArray - 1;
                    }
                    this.countCheckBoxCheckedInPage = 0;
                }
                this.isShowButtonDeleteMulti();
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức trả về trạng thái của check box trong 1 trang
         * @return true là select
         * @return false là unSelect
         * @Author NDDung (16/08/2022)
         */
        tickAllCheckBoxSelected() {
            try {
                this.countCheckBoxCheckedInPage = 0;
                let i = (this.currentPage - 1) * this.recordNumber;
                let maxNumberInPage = (this.currentPage - 1) * this.recordNumber + this.employees.length;
                for (i; i < maxNumberInPage; i++) {
                    if (this.arrayCheckBoxSelectedId[i] != null) {
                        this.countCheckBoxCheckedInPage = this.countCheckBoxCheckedInPage + 1;
                    }
                }

                if (this.countCheckBoxCheckedInPage == this.employees.length) {
                    return true;
                } else {
                    return false;
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Ẩn hiện nút xóa của thực hiện hàng loạt
         * @Author NDDung (16/08/2022)
         */
        showButtonDeleteMulti() {
            if (this.isShowBatchExecution == false && this.countIdInArray >= 1) {
                this.isShowBatchExecution = true;
            } else {
                this.isShowBatchExecution = false;
            }
        },

        /**
         * Kiểm tra xem số bản ghi đang chọn là bao nhiêu nếu < 2 thì ẩn nút xóa
         * @Author NDDung (16/08/2022)
         */
        isShowButtonDeleteMulti() {
            if (this.countIdInArray < 1) {
                this.isShowBatchExecution = false;
            }
        },

        /**
         * Phương thức xóa trong thực hiện hàng loạt
         * @Author NDDung (17/08/2022)
         */
        deleteMultiEmployees() {
            var currentWindow = this;
            const temp = currentWindow.arrayCheckBoxSelectedId.filter(function (e) {
                return e != null;
            });
            currentWindow.arrayCheckBoxSelectedId = temp;
            // Gọi api thực hiện xóa dữ liệu
            try {
                currentWindow.isShowLoading = true;
                axios({
                    headers: {
                        'Content-type': 'application/json'
                    },
                    method: 'delete',
                    url: myApi.DeleteMultiEmployee,
                    data: JSON.stringify(currentWindow.arrayCheckBoxSelectedId)
                }).then(function () {
                    currentWindow.isShowLoading = false;
                    currentWindow.loadDataByFilter();
                    currentWindow.countIdInArray = 0;
                    currentWindow.arrayCheckBoxSelectedId = new Array(currentWindow.TotalRecord).fill(null);
                    currentWindow.isShowButtonDeleteMulti();
                });
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức chuẩn bị xóa nhiều
         * @Author NDDung (17/08/2022)
         */
        prepareDeleteMultiEmployees() {
            this.showWarningPopup(true, resourceVN.DeleteMultiEmployee, deleteMode.multi);
        },

        /**
         * Phương thức chọn các bản ghi trong table
         * @param employeeId Khóa chính của nhân viên
         * @Author NDDung (17/08/2022)
         */
        selectedRow(employeeId) {
            if (this.employeeRowId == employeeId) {
                this.employeeRowId = '';
            } else {
                this.employeeRowId = employeeId;
            }
        },

        /**
         * Phương thức chọn các bản ghi trong table
         * @param employeeId Khóa chính của nhân viên
         * @return true dòng đã được chọn
         * @return false dòng không được chọn
         * @Author NDDung (17/08/2022)
         */
        toggleRowSelected(employeeId) {
            if (this.employeeRowId == employeeId) {
                return true;
            } else {
                return false;
            }
        }
    },

    data() {
        return {
            /**
             * Biến lưu danh sách nhân viên
             */
            employees: [],
            /** Biến lưu id nhân viên đang được chọn để xử lý sự kiện sửa*/
            employeeSelectedId: null,
            /** Biến lưu id nhân viên đang được chọn để xử lý sự kiện xóa*/
            employeeContextMenuId: null,
            /** Biến lưu id nhân viên khi tick vào checkbox*/
            employeeIdCheckboxSelected: null,
            /** Biến lưu kết quả của input search và sử dụng v-model*/
            searchValue: null,
            /** Biến lưu kết quả của input search*/
            currentSearchValue: null,
            /** object chưa mode đang là add hay edit hay clone */
            formMode: 0, // add là 0, edit là 1, clone là 2
            /** Biến chứa tổng bản ghi*/
            totalRecord: null,
            /** Biến kiểu bool để show EmployeeDetail*/
            isShowDialog: false,
            /** Biến kiểu bool để material chưa nút xóa*/
            isShowMaterial: false,
            /** Biến kiểu bool để thể hiện việc đóng mở loading*/
            isShowLoading: false,
            /** Biến kiểu bool để thể hiện việc đóng mở dropdown*/
            isShowDropdown: false,
            /** Text số bản ghi 1 trang*/
            recordInPage: '10 bản ghi trên 1 trang',
            /** Số bản ghi 1 trang*/
            recordNumber: 10,
            /** Hiện đang ở trang số mấy*/
            currentPage: 1,
            /** Tổng số trang*/
            totalPage: 0,
            /** biến kiểu bool lưu việc đóng mở popup*/
            isShowPopup: false,
            /** biến lưu text cho pop up thông báo*/
            popupNotification: [],
            /** biến đếm ngươc*/
            countDown: 0,
            /** Nội dung của toast message*/
            toastContent: '',
            /** Kiểu toast message success, warning, info, error*/
            toastType: '',
            /** Thời gian tồn tại toast message*/
            toastDuration: 0,
            /** Biến kiểu bool show checkbox */
            isShowTick: false,
            /** array chưa id nhân viên cần xóa */
            arrayCheckBoxSelectedId: new Array(),
            /** Số id trong arrayCheckBoxSelectedId*/
            countIdInArray: 0,
            /*** biến ẩn hiện thao tác thực hiện hàng loạt*/
            isShowBatchExecution: false,
            /** Đếm số check box đã chọn trong 1 trang*/
            countCheckBoxCheckedInPage: 0,
            /** biến chưa id theo hàng*/
            employeeRowId: '',
            /** biến chứa loại delete, 0 là xóa 1, 1 là xóa nhiều */
            deleteStatus: 0
        };
    }
};
</script>

<template>
    <div class="content__header">
        <p class="title">Nhân viên</p>
        <BaseButton :text="'Thêm mới nhân viên'" @click="btnAddOnClick"> </BaseButton>
    </div>
    <div class="content__main">
        <div class="content__main__header">
            <div class="base__relative">
                <BaseButton :type="'button__batch__execution flex'" :text="'Thực hiện hàng loạt'" @click="showButtonDeleteMulti()">
                    <div class="dropdown__icon__item flex">
                        <div class="icon icon__dropdown"></div>
                    </div>
                </BaseButton>
                <div class="dropdown__menu display__none" :class="{ isShowBlock: isShowBatchExecution }">
                    <div class="dropdown__menu__item" @click="prepareDeleteMultiEmployees()">Xóa</div>
                </div>
            </div>
            <div class="flex">
                <div class="content__search">
                    <BaseInput v-model="searchValue" :hasIcon="true" @keyup="searchEmployee(searchValue)" :placeHolder="'Tìm theo mã, tên nhân viên'" :inputIcon="'icon__search'"> </BaseInput>
                </div>
                <div class="content__refresh border__icon32">
                    <div @click="btnRefreshOnClick" class="icon icon__refresh" tooltip-title="Lấy lại dữ liệu"></div>
                </div>
                <div class="content__refresh border__icon32">
                    <div @click="btnExportExcel" class="icon icon__excel--export" tooltip-title="Xuất excel"></div>
                </div>
            </div>
        </div>
        <div class="content__table">
            <div class="table">
                <table id="tbEmployeeList">
                    <thead>
                        <th class="w-20px">
                            <BaseCheckBox
                                @click="tickAllCheckBox()"
                                :class="{
                                    checkbox: true,
                                    'checkbox--checked icon icon__checkbox': tickAllCheckBoxSelected()
                                }"
                                :isShowTick="isShowTick"
                            >
                            </BaseCheckBox>
                            <!-- <input type="checkbox" class="checkbox__input" /> -->
                        </th>
                        <th class="w-125px">MÃ NHÂN VIÊN</th>
                        <th class="w-200px">TÊN NHÂN VIÊN</th>
                        <th class="w-100px">GIỚI TÍNH</th>
                        <th class="w-125px table__center">NGÀY SINH</th>
                        <th class="w-150px" title="Số chứng minh nhân dân">Số CMND</th>
                        <th class="w-150px">CHỨC DANH</th>
                        <th class="w-150px">TÊN ĐƠN VỊ</th>
                        <th class="w-170px">SỐ TÀI KHOẢN</th>
                        <th class="w-170px">TÊN NGÂN HÀNG</th>
                        <th class="w-170px" title="Chi nhánh tài khoản ngân hàng">CHI NHANH TK NGÂN HÀNG</th>
                        <th class="w-100px table__center">CHỨC NĂNG</th>
                    </thead>
                    <tbody>
                        <!-- Dùng v-for để binding dữ liệu -->
                        <tr
                            v-for="(emp, index) in employees"
                            :key="emp"
                            :class="{
                                table__item: true,
                                'row--selected': toggleRowSelected(emp.EmployeeId),
                                'rows--checked': tickCheckBoxSelected(index, emp.EmployeeId)
                            }"
                            @click="selectedRow(emp.EmployeeId)"
                            @dblclick="btnEditOnClick(emp)"
                        >
                            <td>
                                <BaseCheckBox
                                    @click="tickCheckBox(index, emp.EmployeeId)"
                                    :class="{
                                        checkbox: true,
                                        'checkbox--checked icon icon__checkbox row--selected': tickCheckBoxSelected(index, emp.EmployeeId)
                                    }"
                                    :isShowTick="isShowTick"
                                >
                                </BaseCheckBox>
                            </td>
                            <td class="tbEmployeeCode">{{ emp.EmployeeCode }}</td>
                            <td>{{ emp.FullName }}</td>
                            <td>{{ formatGender(emp.Gender) || '' }}</td>
                            <td class="table__center">
                                {{ formatDate(emp.DateOfBirth) || '' }}
                            </td>
                            <td>{{ emp.IdentityNumber }}</td>
                            <td>{{ emp.PositionName }}</td>
                            <td>{{ emp.DepartmentName }}</td>
                            <td>{{ emp.BankAccountNumber }}</td>
                            <td>{{ emp.BankName }}</td>
                            <td>{{ emp.BankBranch }}</td>
                            <td class="table__center">
                                <div class="table__combo__button flex">
                                    <div @click="btnEditOnClick(emp)">
                                        <button class="button__edit btnEdit">Sửa</button>
                                    </div>

                                    <div :class="['button__material btnMaterial', emp.EmployeeId == employeeContextMenuId ? 'button__material__border' : '']" @click="btnContextMenuOnClick(emp.EmployeeId)">
                                        <button class="icon icon__table__material button24"></button>
                                    </div>
                                </div>
                            </td>
                            <td class="table__popup">
                                <BaseContextMenu :class="{ isShowBlock: checkShowContextMenu(emp.EmployeeId) }" :employeeCode="emp.EmployeeCode" :employee="emp" @showWarningPopup="showWarningPopup" @btnCloneOnClick="btnCloneOnClick">
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
                    <BaseInput ref="inputDepartment" :isShowDropdown="isShowDropdown" :inputIcon="'icon__dropdown'" :hasIcon="true" :isReadOnly="true" @showDropdown="showRecordInPageDropdown" v-model="recordInPage">
                        <BaseDropdown :isShow="isShowDropdown" :dropdownKind="'dropdown__data'" :contentArray="[10, 20, 30, 50, 100]" :pageSize="recordNumber" :dropdownText="' bản ghi trên 1 trang'" @showDropdown="showRecordInPageDropdown">
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
                            :class="[item == currentPage ? 'btn__focus isShowBlock' : '', item == 1 ? 'isShowBlock' : '', (item > currentPage + 3 && item < totalPage) || (item < currentPage - 1 && item < totalPage - 3) ? 'display__none' : '']"
                        >
                        </BaseButtonPaging>
                    </div>
                    <button :class="[currentPage == totalPage ? 'button--disable' : '']" @click="nextPageOnClick">Sau</button>
                </div>
            </div>
        </div>
    </div>
    <BasePopup :isShowPopup="isShowPopup" :popupText="popupNotification" :iconType="'icon__warning'" :buttonType="'button__warning'" @showPopup="showWarningPopup" @confirmDelete="confirmDelete"> </BasePopup>
    <BaseToastMessage ref="toastMesDetail" :toastContent="toastContent" :toastType="toastType" :toastDuration="toastDuration"> </BaseToastMessage>
    <EmployeeDetail
        v-if="isShowDialog"
        :employeeSelectedId="employeeSelectedId"
        :formMode="formMode"
        @showEmployeeDetailDialog="showEmployeeDetailDialog"
        @loadData="loadDataByFilter"
        @setDataToastMessage="setToastMessage"
        @showToast="showToastMessage"
        @btnAddOnClick="btnAddOnClick"
    />
    <BaseLoading :isShowLoadingChild="isShowLoading"> </BaseLoading>
</template>
