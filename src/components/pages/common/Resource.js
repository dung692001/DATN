export const resourceVN = {
    ValidateError_EmployeeCodeNotEmpty: 'Mã không được để trống.',
    ValidateError_EmployeeFullNameNotEmpty: 'Tên không được để trống.',
    ValidateError_DepartmentCodeNotEmpty: 'Đơn vị không được để trống.',
    DataChanged_DoYouWantToSave: 'Dữ liệu đã bị thay đổi. Bạn có muốn cất không?',
    ValiedateError_EmployeeCodeIsExist: 'Mã đã tồn tại vui lòng nhập mã khác!',
    DeleteMultiEmployee: 'Bạn có thật sự muốn xóa những nhân viên đã chọn không?',
    DateMustLessThenDateTimeNow: 'Ngày phải nhỏ hơn ngày hiện tại',
    ValidateError_SameEmployeeCode: 'Mã nhân viên không được phép trùng',
    ValidateError_Email: 'Email không đúng định dạng.'
};

export class Resource {
    /**
     * Message không được để trống
     * @param text Nội dung của label muốn validate
     * @Author NDDung (21/08/2022)
     */
    static messageMustType(text) {
        return `${text} không được để trống.`;
    }

    /**
     * Cnfirm Delete bản ghi
     * @param text Nội dung của label muốn validate
     * @Author NDDung (21/08/2022)
     */
    static messageDelete(text) {
        return `Bạn có thực sự muốn xóa Nhân viên < ${text} > không?`;
    }

    /**
     * Message báo thông tin không đúng định dạng
     * @param text Nội dung của label muốn validate
     * @Author NDDung (21/08/2022)
     */
    static messageNotValid(text) {
        return `${text} không đúng định dạng!`;
    }

    /**
     * Thông báo thành công
     * @param text Nội dung của label muốn validate
     * @Author NDDung (21/08/2022)
     */
    static messageSuccess(text) {
        return `${text} thành công!`;
    }

    /**
     * Thông báo mã nhân viên đã tồn tại
     * @param text Nội dung của label muốn validate
     * @Author NDDung (21/08/2022)
     */
    static messageEmployeeCodeExist(text) {
        return `Mã nhân viên < ${text} > đã tồn tại trong hệ thống, vui lòng kiểm tra lại.`;
    }
}

export const myApi = {
    DeleteMultiEmployee: `https://localhost:7152/api/v1/Employees/DeleteMultiEmployee`
};
