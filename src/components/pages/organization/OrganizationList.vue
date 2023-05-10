<script>
import OrganizationApi from '../../../apis/OrganizationApi.js';
import OrganizationDetail from './OrganizationDetail.vue';
import { resourceVN } from '../../../common/Resource.js';
import { eventMode } from '../../../common/Enum.js';
import { deleteMode } from '../../../common/Enum.js';
export default {
    name: 'OrganizationList',
    components: {
        OrganizationDetail
    },

    //GĐ 2: created (setup)
    created() {
        this.loadDataByFilter();
    },
    methods: {
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
                await OrganizationApi.loadDataByFilter(this.recordNumber, this.currentPage, currentWindow.currentSearchValue).then(
                    (res) => {
                        currentWindow.totalPage = res.data.TotalPage;
                        currentWindow.organizations = res.data.Data;
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
         * Phương thức thêm hoặc xóa khóa chính của nhiều nhân viên trong 1 trang
         * @Author NDDung (16/08/2022)
         */
        tickAllCheckBox() {
            try {
                let i = (this.currentPage - 1) * this.recordNumber;
                let maxNumberInPage = (this.currentPage - 1) * this.recordNumber + this.organizations.length;
                let j = 0;
                if (this.countCheckBoxCheckedInPage != this.organizations.length) {
                    for (i; i < maxNumberInPage; i++, j++) {
                        if (this.arrayCheckBoxSelectedId[i] != null) {
                            this.countIdInArray = this.countIdInArray - 1;
                        }
                        this.countIdInArray = this.countIdInArray + 1;
                        this.arrayCheckBoxSelectedId[i] = this.organizations[j].OrganizationId;
                    }
                    this.countCheckBoxCheckedInPage = this.organizations.length;
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
                let maxNumberInPage = (this.currentPage - 1) * this.recordNumber + this.organizations.length;
                for (i; i < maxNumberInPage; i++) {
                    if (this.arrayCheckBoxSelectedId[i] != null) {
                        this.countCheckBoxCheckedInPage = this.countCheckBoxCheckedInPage + 1;
                    }
                }

                if (this.countCheckBoxCheckedInPage == this.organizations.length) {
                    return true;
                } else {
                    return false;
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức tìm kiếm theo tên, mã nhân viên
         * @param value Kết quả của input search
         * @Author NDDung (28/07/2022)
         */
        searchObject(value) {
            let currentWindow = this;
            clearTimeout(this.countDown);
            currentWindow.countDown = setTimeout(function () {
                console.log(value);
                currentWindow.currentSearchValue = value;
                currentWindow.loadByIdOrName();
            }, 1000);
        },

        /**
         * Thực hiện loadData theo tên hoặc id
         * @Author NDDung (28/07/2022)
         */
        loadByIdOrName() {
            this.currentPage = 1;
            // Gọi api thực hiện lấy dữ liệu
            this.loadDataByFilter();
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
         * Phương thức chuẩn bị xóa nhiều
         * @Author NDDung (17/08/2022)
         */
        prepareDeleteMultidOrganizations() {
            this.showWarningPopup(true, resourceVN.DeleteMultiorganization, deleteMode.multi);
        },

        /**
         * Xử lý sự kiện load lại dữ liệu
         * @Author NDDung (27/07/2022)
         */
        btnRefreshOnClick() {
            this.loadDataByFilter();
        },

        /**
         * Phương thức export data ra file excel
         * @Author NDDung (03/08/2022)
         */
        btnExportExcel() {
            try {
                OrganizationApi.exportorganization().then(
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
         * Phương thức chọn các bản ghi trong table
         * @param depatmentId Khóa chính của nhân viên
         * @return true dòng đã được chọn
         * @return false dòng không được chọn
         * @Author NDDung (17/08/2022)
         */
        toggleRowSelected(depatmentId) {
            if (this.organizationRowId == depatmentId) {
                return true;
            } else {
                return false;
            }
        },

        /**
         * Phương thức trả về trạng thái của check box
         * @param index index được truyền vào từ v-for
         * @param depatmentId là khóa chính của nhân viên
         * @return true là select
         * @return false là unSelect
         * @Author NDDung (16/08/2022)
         */
        tickCheckBoxSelected(index, depatmentId) {
            try {
                let currentIndex = (this.currentPage - 1) * this.recordNumber + index;
                if (this.arrayCheckBoxSelectedId[currentIndex] == null) {
                    return false;
                }
                if (this.arrayCheckBoxSelectedId[currentIndex] == depatmentId) {
                    return true;
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Phương thức chọn các bản ghi trong table
         * @param depatmentId Khóa chính của nhân viên
         * @Author NDDung (17/08/2022)
         */
        selectedRow(depatmentId) {
            if (this.organizationRowId == depatmentId) {
                this.organizationRowId = '';
            } else {
                this.organizationRowId = depatmentId;
            }
        },

        /**
         * Hiển thị form chi tiết nhân viên ấn nút thêm mới nhân viên
         * @Author NDDung (23/07/2022)
         */
        btnAddOnClick() {
            this.organizationSelectedId = '';
            this.showOrganizationDetailDialog(true);
            this.formMode = eventMode.add;
        },

        /**
         * Hiển thị form chi tiết nhân viên khi ấn nút sửa tại dòng dữ liệu
         * @param organization thông tin nhân viên
         * @Author NDDung (23/07/2022)
         */
        btnEditOnClick(organization) {
            this.showOrganizationDetailDialog(true);
            this.organizationSelectedId = organization.OrganizationId;
            this.organizationContextMenuId = '';
            this.formMode = eventMode.edit;
        },

        /**
         * Hiển thị viền bao quanh nút context menu
         * @param organizationId Khóa chính của nhân viên
         * @Author NDDung (25/07/2022)
         */
        btnContextMenuOnClick(organizationId) {
            if (this.organizationContextMenuId == organizationId) {
                this.organizationContextMenuId = '';
            } else {
                this.organizationContextMenuId = organizationId;
            }
        },

        /**
         * ẩn hiện context menu
         * @param depatmentId Khóa chính của nhân viên
         * @Author NDDung (25/07/2022)
         */
        checkShowContextMenu(organizationId) {
            if (this.organizationContextMenuId == organizationId) {
                return true;
            } else {
                return false;
            }
        },

        /**
         * Phương thức xử lý sự kiện khi ấn nút nhân bản
         * @param organization Thông tin của nhân viên
         * @Author NDDung (03/08/2022)
         */
        btnCloneOnClick(organization) {
            this.showOrganizationDetailDialog(true);
            this.depatmentSelectedId = organization.DepatmentId;
            this.organizationContextMenuId = '';
            this.formMode = eventMode.clone;
        },

        /**
         * Phương thức đóng mở dialog
         * @param isShowDialog isShowDialog = true mở form và false là đóng form
         * @Author NDDung (23/07/2022)
         */
        showOrganizationDetailDialog(isShowDialog) {
            this.isShowDialog = isShowDialog;
        },

        /**
         * Phương thức đóng mở dropdown chọn số bản ghi
         * @param isShow Biến ẩn hiện dropdown
         * @param value Số bản ghi đã chọn
         * @Author NDDung (29/07/2022)
         */
        showRecordInPageDropdown(isShow, value) {
            this.isShowDropdown = isShow;
            this.$refs.inputOrganization.isRorateIcon = false;
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
         * Phương thức xử lí sự kiện xác nhận xóa
         * @Author NDDung (25/07/2022)
         */
        async confirmDelete() {
            // Khi xóa đơn
            if (this.deleteStatus == 0) {
                let currentWindow = this;
                this.setToastMessage('Xóa thành công!', 'success', 2000);
                try {
                    await OrganizationApi.deleteOrganization(currentWindow.organizationContextMenuId).then(
                        (res) => {
                            res;
                            this.loadDataByFilter();
                            this.showToastMessage();
                        },
                        (err) => {
                            console.log(err);
                        }
                    );
                } catch (error) {
                    console.log(error);
                }
            }
        }
    },
    // Kết thúc methods organization
    data() {
        return {
            /**
             * Biến lưu danh sách nhân viên
             */
            organizations: [],
            /** Biến lưu id nhân viên đang được chọn để xử lý sự kiện sửa*/
            organizationSelectedId: null,
            /** Biến lưu id nhân viên đang được chọn để xử lý sự kiện xóa*/
            organizationContextMenuId: null,
            /** Biến lưu id nhân viên khi tick vào checkbox*/
            organizationIdCheckboxSelected: null,
            /** Biến lưu kết quả của input search và sử dụng v-model*/
            searchValue: null,
            /** Biến lưu kết quả của input search*/
            currentSearchValue: null,
            /** object chưa mode đang là add hay edit hay clone */
            formMode: 0, // add là 0, edit là 1, clone là 2
            /** Biến chứa tổng bản ghi*/
            totalRecord: null,
            /** Biến kiểu bool để show OrganizationDetail*/
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
            organizationRowId: '',
            /** biến chứa loại delete, 0 là xóa 1, 1 là xóa nhiều */
            deleteStatus: 0,
            dataLoadImg: 'images/9317f7fa-21c7-48a8-8770-c47e25258c8d'
        };
    }
};
</script>
<template>
    <div class="content__header">
        <p class="title">Dự án</p>
        <BaseButton :text="'Thêm mới Dự án'" @click="btnAddOnClick"> </BaseButton>
    </div>
    <div class="content__main">
        <div class="content__main__header">
            <div class="base__relative"></div>
            <div class="flex">
                <div class="content__search">
                    <BaseInput v-model="searchValue" :hasIcon="true" @keyup="searchObject(searchValue)" :placeHolder="'Tìm theo mã Dự án'" :inputIcon="'icon__search'"> </BaseInput>
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
                <table id="tbOrganizationList">
                    <thead>
                        <th class="w-125px">MÃ DỰ ÁN</th>
                        <th class="w-200px">TÊN DỰ ÁN</th>
                        <th class="w-200px">TÊN PHÒNG BAN</th>
                        <th class="w-100px table__center">CHỨC NĂNG</th>
                    </thead>
                    <tbody>
                        <!-- Dùng v-for để binding dữ liệu -->
                        <tr
                            v-for="(emp, index) in organizations"
                            :key="emp"
                            :class="{
                                table__item: true,
                                'row--selected': toggleRowSelected(emp.OrganizationId),
                                'rows--checked': tickCheckBoxSelected(index, emp.OrganizationId)
                            }"
                            @click="selectedRow(emp.OrganizationId)"
                            @dblclick="btnEditOnClick(emp)"
                        >
                            <td class="tbOrganizationCode">{{ emp.OrganizationCode }}</td>
                            <td>{{ emp.OrganizationName }}</td>
                            <td>{{ emp.DepartmentName }}</td>
                            <td class="table__center">
                                <div class="table__combo__button flex">
                                    <div @click="btnEditOnClick(emp)">
                                        <button class="button__edit btnEdit">Sửa</button>
                                    </div>

                                    <div :class="['button__material btnMaterial', emp.OrganizationId == organizationContextMenuId ? 'button__material__border' : '']" @click="btnContextMenuOnClick(emp.OrganizationId)">
                                        <button class="icon icon__table__material button24"></button>
                                    </div>
                                </div>
                            </td>
                            <td class="table__popup">
                                <BaseContextMenu :class="{ isShowBlock: checkShowContextMenu(emp.OrganizationId) }" :organizationCode="emp.OrganizationCode" :organization="emp" @showWarningPopup="showWarningPopup" @btnCloneOnClick="btnCloneOnClick">
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
                    <BaseInput ref="inputOrganization" :isShowDropdown="isShowDropdown" :inputIcon="'icon__dropdown'" :hasIcon="true" :isReadOnly="true" @showDropdown="showRecordInPageDropdown" v-model="recordInPage">
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
    <OrganizationDetail
        v-if="isShowDialog"
        :organizationSelectedId="organizationSelectedId"
        :formMode="formMode"
        @showOrganizationDetailDialog="showOrganizationDetailDialog"
        @loadData="loadDataByFilter"
        @setDataToastMessage="setToastMessage"
        @showToast="showToastMessage"
        @btnAddOnClick="btnAddOnClick"
    />
    <BaseLoading :isShowLoadingChild="isShowLoading"> </BaseLoading>
</template>
