import BaseAPIConfig from '@/apis/BaseAPIConfig.js';

class DepartmentAPI {
    controller = 'Department';

    getTest() {
        return BaseAPIConfig.get(`https://catfact.ninja/fact`);
    }
    /**
     * Lấy toàn bộ phòng ban
     * @param {}
     * @returns
     * author: Nguyễn Đăng Dũng (27/07/2022)
     */
    async getAllDepartment() {
        return await BaseAPIConfig.get(`${this.controller}`);
    }

    /**
     * Lấy nhân viên theo id
     * @param {*} departmentId id nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async getDepartmentById(departmentId) {
        return await BaseAPIConfig.get(`${this.controller}/${departmentId}`);
    }

    /**
     * Lưu nhân viên khi ấn nút thêm
     * @param {*} department thông tin của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async saveDepartmentByAdd(department) {
        return await BaseAPIConfig.post(`${this.controller}`, department);
    }

    /**
     * Lưu nhân viên khi ấn nút sửa
     * @param {*} departmentId Mã của nhân viên
     * @param {*} department thông tin của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async saveDepartmentByEdit(departmentId, department) {
        return await BaseAPIConfig.put(`${this.controller}/${departmentId}`, department);
    }

    /**
     * Xóa nhân viên theo id
     * @param {*} departmentId Mã của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (07/05/2022)
     */
    async deleteDepartment(departmentId) {
        return await BaseAPIConfig.delete(`${this.controller}/${departmentId}`);
    }

    /**
     * Lấy nhân viên theo số bản ghi 1 trang
     * @param {*} recordNumber Mã hoặc tên của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async loadDataByFilter(recordNumber, currentPage, textFilter) {
        return await BaseAPIConfig.get(`${this.controller}/filter?pageSize=${recordNumber}&pageNumber=${currentPage}&textFilter=${textFilter}`);
    }
}
export default new DepartmentAPI();
