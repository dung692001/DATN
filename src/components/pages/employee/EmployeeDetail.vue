<template>
  <div
    id="dlgEmployeeDetail"
    class="dialog__popup"
    @keydown ="hanldeOnKeyDown"
    @keyup ="hanldeOnKeyUp"
    v-bind:class="{ isShowDialog: true }"
  >
    <div class="dialog">
      <div class="dialog__header">
        <div class="dialog__title flex">
          <div class="title__popup">{{formMode != 1 ? 'Thêm Nhân Viên' : "Sửa Nhân viên"}}</div>
          <div class="dialog__checkbox flex">
            <BaseCheckBox class="checkbox"></BaseCheckBox>
            <span class="dialog__component__text title2">Là khách hàng</span>
          </div>

          <div class="dialog__checkbox flex">
            <BaseCheckBox class="checkbox"></BaseCheckBox>
            <span class="dialog__component__text">Là nhà cung cấp</span>
          </div>
        </div>
        <div class="dialog__header__button">
          <div class="border__icon32 
            tooltip-title-dialog "
            tooltip-content="Giúp (F1)">
            <div class="icon icon__help" ></div>
          </div>
          <div
            class="border__icon32 tooltip-title-dialog" 
            tooltip-content="Đóng (ESC)"
            @click="checkDataChange"
          >
            <div class="icon icon__close popup--close"></div>
          </div>
        </div>
      </div>
      <div class="dialog__container">
        <div class="dialog__content">
          <div class="dialog__content__up">
            <div class="dialog__content__left">
              <div class="dialog__row">
                <button
                  tabIndex="1"
                  @keyup="returnLastTabIndex"
                  class="button_hidden"
                ></button>
                <BaseInput
                  ref="inputCode"
                  tabIndex="2"
                  :label="'Mã'"
                  :inputRequire="true"
                  :size="'w-40 p-r-6'"
                  :maxLengthInput="20"
                  :hasTooltip="true"
                  v-model="employee.EmployeeCode"
                >
                </BaseInput>
                <BaseInput
                  ref="inputName"
                  :label="'Tên'"
                  tabIndex="3"
                  :inputRequire="true"
                  :size="'w-60'"
                  :maxLengthInput="100"
                  :hasTooltip="true"
                  v-model="employee.FullName"
                >
                </BaseInput>
              </div>

              <div class="dialog__row">
                <BaseCombobox
                  ref="inputDepartment"
                  :label="'Đơn vị'"
                  :tabIndex="4"
                  :inputRequire="true"
                  :inputIcon="'icon__dropdown'"
                  :dropdownTitleLeft="'Mã đơn vị'"
                  :dropdownTitleRight="'Tên đơn vị'"
                  :dataSelectedText="employee.DepartmentName"
                  :dataSelectedId="employee.DepartmentId"
                  :data="departments"
                  :idData="'DepartmentId'"
                  :codeData="'DepartmentCode'"
                  :nameData="'DepartmentName'"
                  :hasTooltip="true"
                  @setDepartmentName="setDepartmentName"
                >
                </BaseCombobox>
              </div>

              <div class="dialog__row">
                <BaseInput
                  :label="'Chức danh'"
                  tabIndex="5"
                  :maxLengthInput="100"
                  v-model="employee.PositionName"
                >
                </BaseInput>
              </div>
            </div>
            <div class="dialog__content__right">
              <div class="dialog__row">
                <div class="dialog__item w-40 p-r-6">
                  <div class="dialog__label">
                    <label for="">Ngày sinh</label>
                  </div>
                  <div class="cboDateOfBirth">
                    <BaseMSDate
                      ref="inputDateOfBirth"
                      :dateSelect="employee.DateOfBirth"
                      :tabIndex="6"
                      @setDateTime="setDateOfBirth"
                      @showErrorInput="showErrorInput"
                      >
                    </BaseMSDate>
                  </div>
                </div>
                <div class="dialog__item w-60 padding-left-10">
                  <div class="dialog__label">
                    <label for="">Giới tính</label>
                  </div>
                  <div class="flex-align-center height-100">
                    <div class="flex margin-20">
                      <BaseRadio
                        :tabIndex="7"
                        :radioName="'rdoGender'"
                        :radioText="'Nam'"
                        :value="0"
                        :modelValue="employee.Gender"
                        @changeGender="changeGender"
                      >
                      </BaseRadio>
                    </div>
                    <div class="flex margin-20">
                      <BaseRadio
                        :tabIndex="8"
                        :radioName="'rdoGenderFe'"
                        :radioText="'Nữ'"
                        :value="1"
                        :modelValue="employee.Gender"
                        @changeGender="changeGender"
                      >
                      </BaseRadio>
                    </div>
                    <div class="flex margin-20">
                      <BaseRadio
                        :tabIndex="9"
                        :radioName="'rdoGenderOther'"
                        :radioText="'Khác'"
                        :value="2"
                        :modelValue="employee.Gender"
                        @changeGender="changeGender"
                      >
                      </BaseRadio>
                    </div>
                  </div>
                </div>
              </div>

              <div class="dialog__row">
                <BaseInput
                  tabIndex="10"
                  :labelTitle="'Số chứng minh nhân dân'"
                  :label="'Số CMND'"
                  :size="'w-60 p-r-6'"
                  :maxLengthInput="25"
                  v-model="employee.IdentityNumber"
                >
                </BaseInput>
                <div class="dialog__item w-40">
                  <div class="dialog__label">
                    <label for="">Ngày cấp</label>
                  </div>
                  <div class="cboIdentityDate">
                    <BaseMSDate
                      ref="inputIdentityDate"
                      :dateSelect="employee.IdentityDate"
                      :tabIndex="11"
                      @setDateTime="setIdentityDate"
                      @showErrorInput="showErrorInput"
                      >
                    </BaseMSDate>
                  </div>
                </div>
              </div>

              <div class="dialog__row">
                <BaseInput
                  tabIndex="12"
                  :label="'Nơi Cấp'"
                  :maxLengthInput="255"
                  v-model="employee.IdentityPlace"
                >
                </BaseInput>
              </div>
            </div>
          </div>
          <div class="dialog__content__down">
            <div class="dialog__row">
              <BaseInput
                tabIndex="13"
                :label="'Địa chỉ'"
                :maxLengthInput="255"
                v-model="employee.Address"
              >
              </BaseInput>
            </div>

            <div class="dialog__row">
              <BaseInput
                tabIndex="14"
                :labelTitle="'Điện thoại di động'"
                :label="'ĐT di động'"
                :size="'w-25 p-r-6'"
                :maxLengthInput="50"
                v-model="employee.PhoneNumber"
              >
              </BaseInput>

              <BaseInput
                tabIndex="15"
                :labelTitle="'Điện thoại cố định'"
                :label="'ĐT cố định'"
                :size="'w-25 p-r-6'"
                :maxLengthInput="50"
                v-model="employee.TelephoneFax"
              >
              </BaseInput>

              <BaseInput
                tabIndex="16"
                :label="'Email'"
                :size="'w-25 p-r-6'"
                :inputRequireValidate="true"
                @setToastMessage="setToastMessage"
                @showToastMessage="showToastMessage"
                :maxLengthInput="100"
                v-model="employee.Email"
              >
              </BaseInput>
            </div>

            <div class="dialog__row">
              <BaseInput
                tabIndex="17"
                :label="'Tài khoản ngân hàng'"
                :size="'w-25 p-r-6'"
                :maxLengthInput="25"
                v-model="employee.BankAccountNumber"
              >
              </BaseInput>

              <BaseInput
                tabIndex="18"
                :label="'Tên ngân hàng'"
                :size="'w-25 p-r-6'"
                :maxLengthInput="250"
                v-model="employee.BankName"
              >
              </BaseInput>

              <BaseInput
                tabIndex="19"
                :label="'Chi nhánh'"
                :size="'w-25 p-r-6'"
                :maxLengthInput="250"
                v-model="employee.BankBranch"
              >
              </BaseInput>
            </div>
          </div>
        </div>
        <div class="dialog__footer">
          <div class="dialog__line"></div>
          <div class="dialog__footer__button">
            <div class="dialog__footer__left">
              <BaseButton
                ref="btnDeny"
                tabIndex="22"
                :text="'Hủy'"
                :type="'button__outline button__outline__dialog'"
                @click="checkDataChange"
              >
              </BaseButton>
            </div>

            <div class="dialog__footer__right flex">
              <div style="padding: 0 10px">
                <BaseButton
                  tabIndex="20"
                  tooltip-content="Cất (Ctrl + S)"
                  :text="'Cất'"
                  :type="'button__outline button__outline__dialog tooltip-title-dialog'"
                  @click="btnSaveOnClick(this.saveMode.saveDefault)"
                >
                </BaseButton>
              </div>
              <div>
                <BaseButton
                  tabIndex="21"
                  tooltip-content="Cất và thêm (Ctrl + Shift + S)" 
                  :text="'Cất và Thêm'"
                  :type="'button__dialog tooltip-title-dialog'"
                  @click="btnSaveOnClick(this.saveMode.saveAndAdd)"
                >
                </BaseButton>
                <button
                  tabIndex="23"
                  @keyup="returnFirstTabIndex"
                  class="button_hidden"
                ></button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <BaseToastMessage
      ref="toastMesDetail"
      :toastContent="toastContent"
      :toastType="toastType"
      :toastDuration="toastDuration"
    >
    </BaseToastMessage>
    <BasePopup
      :isShowPopup="isShowPopup"
      :popupText="popupNotification"
      :iconType="popupType"
      :buttonType="popupButtonType"
      @showPopup="showEmployeeDetailPopup"
      @closeEmployeeDetail="btnCloseOnClick"
      @saveEmployee="btnSaveOnClick(this.saveMode.saveDefault)"
    >
    </BasePopup>
  </div>
</template>

<script>
import EmployeeApi from "../../../apis/EmployeeApi.js";
import DepartmentApi from "../../../apis/DepartmentApi.js";
import { resourceVN } from "../../../common/Resource.js";
import { Resource } from "../../../common/Resource.js";
export default {
  name: "EmployeeDetail",
  emits: [
    "showEmployeeDetailDialog",
    "loadDataByFilter",
    "setToastMessage",
    "showToastMessage",
    "showToastMessage",
  ],
  /**
   * Phương thức loadData khi lần đầu mở form
   * @Author NDDung (23/07/2022)
   */
  mounted() {
    this.getAllDepartment();
    // nếu là mode add thì thực hiện lấy mã nhân viên mới
    if (this.formMode === 0) {
      this.getNewCode();
      this.employee.Gender = 0;
    }
    // nếu là mode edit thì thực hiện lấy thông tin của nhân viên 
    if (this.formMode === 1) {
      this.getEmployee(this.employeeSelectedId);
    }
    // nếu là mode clone thì thực hiện lấy thông tin của nhân viên sau đó gán mã mơi
    if (this.formMode === 2) {
      this.getEmployee(this.employeeSelectedId); 
      this.employee.employeeCode = ""
      this.getNewCode();
    }

  },
  methods: {
    /**
     * Phương thức xử lý sự kiện khi ấn đóng dialog
     * @Author NDDung (23/07/2022)
     */
    btnCloseOnClick() {
      // showEmployeeDetailDialog chỉ là cái để tham chiếu đến @showEmployeeDetailDialog ở cha

      this.$emit("showEmployeeDetailDialog", false);
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
          if (
            currentWindow.formMode == 0 ||
            currentWindow.formMode == 2
          ) {
            this.setToastMessage(Resource.messageSuccess("Thêm"), "success", 2000);
            // format lại DateOfBirth và IdentityDate
            currentWindow.employee.DateOfBirth = currentWindow.formatDate(
              currentWindow.employee.DateOfBirth
            );
            currentWindow.employee.IdentityDate = currentWindow.formatDate(
              currentWindow.employee.IdentityDate
            );
            // Thực hiện thêm mới 
            EmployeeApi.saveEmployeeByAdd(currentWindow.employee).then(
              () => {
                // Nếu thêm mới thành công hiện thị toast msg thông báo thành công:
                currentWindow.showToastMessage();
                // Load lại dữ liệu
                currentWindow.$emit("loadData");
                // Nếu ấn vào cất
                if (saveMode == 0) {
                  // Đóng form thêm mới
                  currentWindow.$emit("showEmployeeDetailDialog", false);
                }
                // Nếu ấn vào cất và thêm
                else {
                  // Đóng form thêm mới
                  currentWindow.$emit("showEmployeeDetailDialog", false);
                  this.employee = {};
                  // Mở 1 form thêm mới
                  this.getNewCode();
                  this.employee.Gender = 0;
                  currentWindow.$emit("btnAddOnClick");
                }
              },
              (err) => {
                if (err.code == "ERR_BAD_REQUEST") {
                  currentWindow.checkSameCode(err);
                }
              }
            );
          } else {
            
            currentWindow.setToastMessage(Resource.messageSuccess("Sửa"), "success", 2000);
            // format lại DateOfBirth và IdentityDate
            currentWindow.employee.DateOfBirth = currentWindow.formatDate(
              currentWindow.employee.DateOfBirth
            );
            currentWindow.employee.IdentityDate = currentWindow.formatDate(
              currentWindow.employee.IdentityDate
            );
            // Thực hiên sửa nhân viên
            EmployeeApi.saveEmployeeByEdit(
              currentWindow.employeeSelectedId,
              currentWindow.employee
            ).then(
              () => {
                // Nếu sửa thành công hiện thị toast msg thông báo thành công:
                currentWindow.showToastMessage();
                // Load lại dữ liệu
                currentWindow.$emit("loadData");

                if (saveMode == 0) {
                  // Đóng form thêm mới
                  currentWindow.$emit("showEmployeeDetailDialog", false);
                }
                // Nếu ấn vào cất và thêm
                else {
                  // Đóng form thêm mới
                  currentWindow.$emit("showEmployeeDetailDialog", false);
                  this.employee = {};
                  // Mở 1 form thêm mới
                  this.getNewCode();
                  this.employee.Gender = 0;
                  currentWindow.$emit("btnAddOnClick");
                }
              },
              (err) => {
                if (err.code == "ERR_BAD_REQUEST") {
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
    showEmployeeDetailPopup(isShow) {
      try {
        if (isShow == false) {
          if (this.popupType == "icon__error" || this.popupType == "icon__warning"  && this.inputRequire.length > 0) {
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
        if (!this.employee.EmployeeCode) {
          this.inputRequire.push("inputCode");
          this.popupNotification.push(resourceVN.ValidateError_EmployeeCodeNotEmpty);
          valid = false;
        }
        // Kiểm tra tên có đang để trống hay không
        if (!this.employee.FullName) {
          this.inputRequire.push("inputName");
          this.popupNotification.push(resourceVN.ValidateError_EmployeeFullNameNotEmpty);
          valid = false;
        }
        // Kiểm tra đơn vị có đang để trống hay không
        if (!this.employee.DepartmentName) {
          this.inputRequire.push("inputDepartment");
          this.popupNotification.push(resourceVN.ValidateError_DepartmentCodeNotEmpty);
          valid = false;
        }

        // Kiểm tra ngày có đúng định dạng hay không
        if (this.checkDate(this.employee.DateOfBirth)) {
          this.inputRequire.push("inputDateOfBirth");
          this.popupNotification.push(resourceVN.DateMustLessThenDateTimeNow);
          valid = false;
        }

        if (this.checkDate(this.employee.IdentityDate)) {   
          this.inputRequire.push("inputEmployee");    
          this.popupNotification.push(resourceVN.DateMustLessThenDateTimeNow);
          valid = false;
        }

        if (this.validateEmail(this.employee.Email)==false) { 
          this.inputRequire.push("inputEmail");       
          this.popupNotification.push(resourceVN.ValidateError_Email);
          valid = false;
        }

        if(valid == false){
          this.showErrorPopup()
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
    showErrorPopup(){
      this.popupType="icon__error";
      this.popupButtonType="button__error";
      this.showEmployeeDetailPopup(true);
    },

    /**
     * Phương thức quay trở về tabindex đầu tiên
     * @Author NDDung (25/07/2022)
     */
    returnFirstTabIndex() {
      try{
        this.$refs["inputCode"].focusInput();
      }catch(error) {
        console.log(error);
      }   
    },

    /**
     * Phương thức quay trở về tabindex cuối cùng
     * @Author NDDung (25/07/2022)
     */
    returnLastTabIndex() {
      try{
        this.$refs["btnDeny"].focusInput();
      }catch(error) {
        console.log(error);
      }   
    },

    /**
     * Phương thức nhận code mới từ api khi ấn vào nút add
     * @Author NDDung (25/07/2022)
     */
    async getNewCode() {
      let currentWindow = this;
      try {
        currentWindow.$refs["inputCode"].focusInput();
        await EmployeeApi.getNewEmployeeCode().then(
          (res) => {
            currentWindow.employee.EmployeeCode = "";
            currentWindow.employeeTemp = JSON.stringify(currentWindow.employee);
            currentWindow.employee.EmployeeCode = res.data;
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
     * Phương thức get employee từ employeeId
     * @param employeeId: id truyền vào để lấy toàn bộ thông tin nhân viên
     * @Author NDDung (25/07/2022)
     */
    async getEmployee(employeeId) {
      let currentWindow = this;
      try {
        this.$refs["inputCode"].focusInput();
        await EmployeeApi.getEmployeeById(employeeId).then(
          (res) => {
            currentWindow.employee = res.data;
            setTimeout(function () {
              for (let item of currentWindow.departments) {
                if (currentWindow.employee.DepartmentId == item.DepartmentId) {
                  currentWindow.employee.DepartmentName = item.DepartmentName;
                }
              }
              currentWindow.employeeTemp = JSON.stringify(
                currentWindow.employee
              );
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
      this.employee.IdentityDate= e;
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
            month = "0" + month;
          }
          if (newDate <= 9) {
            newDate = "0" + newDate;
          }
          return `${year}/${month}/${newDate}`;
        }
      } catch (error) {
        console.log(error);
        return "";
      }
    },

    /**
     * Phương thức chọn giới tính
     * @param value: kết quả của 
     * @Author NDDung (27/07/2022)
     */
    changeGender(value) {
      this.employee.Gender = value;
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
            currentWindow.departments = res.data;
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
     * Phương thức đóng mở dropdown
     * @Author NDDung (28/07/2022)
     */
    showDepartmentDropdown(isShow, value) {
      this.isShowDropdown = isShow;
      if (value != "") {
        this.employee.DepartmentId = value.DepartmentId;
        this.employee.DepartmentName = value.DepartmentName;
      }
    },

    /**
     * Phương thức kiểm tra mã có bị trùng không
     * @param :error lỗi nhận về từ api
     * @Author NDDung (28/07/2022)
     */
    checkSameCode(error) {
      if (error.response.data.errorMsg["EmployeeCodeIsExist"] == resourceVN.ValidateError_SameEmployeeCode) {
        this.showEmployeeDetailPopup(true);
        this.popupNotification = []
        this.popupNotification.push(Resource.messageEmployeeCodeExist(this.employee.EmployeeCode));
        this.popupType = "icon__warning";
        this.popupButtonType = "button__SameCode";
        this.inputRequire.push("inputCode");
        return false;
      }
      return true;
    },

    /**
     * Phương thức kiểm tra dữ liệu đã thay đổi hay chưa
     * @Author NDDung (29/07/2022)
     */
    checkDataChange() {
      if (JSON.stringify(this.employee) == this.employeeTemp) {
        this.$emit("showEmployeeDetailDialog", false);
        this.employee = {};
        this.btnCloseOnClick();
      } else {
        this.popupNotification = []
        this.popupNotification.push(resourceVN.DataChanged_DoYouWantToSave);
        this.popupType = "icon__info";
        this.popupButtonType = "button__info";
        this.showEmployeeDetailPopup(true);
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
      this.$refs["toastMesDetail"].showToast();
    },

    /**
     * Phương thức set các giá trị của department
     * @value department đã chọn
     * @Author NDDung (03/08/2022)
     */
    setDepartmentName(value) {
      if (value != "") {
        this.employee.DepartmentId = value.DepartmentId;
        this.employee.DepartmentName = value.DepartmentName;
      }
    },

    /**
     * Phương thức kiểm tra ngày có lớn hơn ngày hiện tại hay Không
     * @param date: ngày truyền vào để kiểm tra định dạng
     * @Author NDDung (15/08/2022)
     */
    checkDate(date) {
      try {
        if (date) {
          date = new Date(date);
          if (date >= new Date()) {
            return true;
          } else {
            return false;
          }
        }
      } catch (error) {
        console.log(error);
        return "";
      }
    },

    /**
     * Phương thức xử lý sự kiên key down
     * @param e: event của keyup
     * @Author NDDung (20/08/2022)
     */
    hanldeOnKeyDown(e){
        if (e.ctrlKey && e.key.toLowerCase() === "s" && e.shiftKey) {
          e.preventDefault();
          this.btnSaveOnClick(this.saveMode.saveAndAdd);
        }
        if (e.ctrlKey && e.key.toLowerCase() === "s" && !e.shiftKey) {
          e.preventDefault();
          this.btnSaveOnClick(this.saveMode.saveDefault);
        }  
    },

    /**
     * Phương thức xử lý sự kiên key up
     * @param e: event của keyup
     * @Author NDDung (20/08/2022)
     */
    hanldeOnKeyUp(e){
        if (e.key === "Escape") {
          this.checkDataChange();
        }
    },

    /**
     * Phương thức show error input
     * @Author NDDung (20/08/2022)
     */
    showErrorInput(){

    },
    
    /**
     * validateEmail
     * @param value giá trị của email
     * @Author NDDung (21/08/2022)
     */
    validateEmail(value) {
      if (value == "" || value == undefined) {
        return true;
      }
      const regex = /^\w+([\\.-]?\w+)*@\w+([\\.-]?\w+)*(\.\w{2,3})+$/;
      const matches = regex.test(value);
      return matches;
    },
  },
  props: {
    // biến bool hiển thị việc ẩn hiện dialog detail
    isShow: {
      type: Boolean,
    },
    // id của nhân viên đang được chọn
    employeeSelectedId: {
      type: String,
    },
    // lưu mode của dialog là thêm hay sửa
    formMode: {
      type: [Object, Number],
      required: true,
    },
  },
  watch: {
    employeeSelectedId: function (value) {
      this.getAllDepartment();
      if (value) {
        this.getEmployee(this.employeeSelectedId);
      } else {
        this.getNewCode();
        this.employee.Gender = 0;
      }
    },
    employeeCode: function (value) {
      this.employee.employeeCode = value
    }
  },
  data() {
    return {
      // object lưu 1 nhân viên được truyền vào từ employeeList
      employee: {},
      //Biến lưu danh sách phòng ban
      departments: [],
      //Biến lưu mã nhân viên hiện đang chọn
      currentEmployeeId: null,
      // biến kiểu bool lưu việc đóng mở popup
      isShowPopup: false,
      // biến lưu text cho pop up thông báo
      popupNotification: [],
      //  biến kiểu bool lưu việc đóng mở dropdown
      isShowDropdown: false,
      // array chứa các input require chưa nhập
      inputRequire: [],
      // json chứa thông tin của nhân viên để kiểm tra dữ liệu đã thay đổi Chưa
      employeeTemp: {},
      // kiểu của popup (ví dụ success, error)
      popupType: null,
      // kiểu nút của popup
      popupButtonType: null,
      // Nội dung của toast message
      toastContent: "",
      // Kiểu toast message success, warning, info, error
      toastType: "",
      // Thời gian tồn tại toast message
      toastDuration: 0,
      // Kiểu của nút cất dữ liệu
      saveMode: {
        saveDefault: 0, // Cất
        saveAndAdd: 1, // Cất và thêm
      },
    };
  },
};
</script>

<style scoped>
@import url("../../../css/base/dialog.css");
</style>
