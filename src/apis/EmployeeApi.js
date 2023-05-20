import BaseAPIConfig from '@/apis/BaseAPIConfig.js';

class EmployeeAPI {
    controller = 'Employee';

    getTest() {
        return BaseAPIConfig.get(`https://catfact.ninja/fact`);
    }

    /**
     * Lấy toàn bộ nhân viên
     * @param {}
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async getAllEmployee() {
        return await BaseAPIConfig.get(`${this.controller}`);
    }

    /**
     * Lấy nhân viên theo số bản ghi 1 trang
     * @param {*} recordNumber Mã hoặc tên của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async loadDataByFilter(recordNumber, currentPage, employeeFilter) {
        return await BaseAPIConfig.get(`${this.controller}/filter?pageSize=${recordNumber}&pageNumber=${currentPage}&employeeFilter=${employeeFilter}`);
    }

    /**
     * Lấy mã nhân viên mới
     * @param {}
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async getNewEmployeeCode() {
        return await BaseAPIConfig.get(`${this.controller}/NewEmployeeCode`);
    }

    /**
     * Lấy nhân viên theo id
     * @param {*} employeeId id nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async getEmployeeById(employeeId) {
        return await BaseAPIConfig.get(`${this.controller}/${employeeId}`);
    }

    /**
     * Lưu nhân viên khi ấn nút thêm
     * @param {*} employee thông tin của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async saveEmployeeByAdd(employee) {
        return await BaseAPIConfig.post(`${this.controller}`, employee);
    }

    /**
     * Lưu nhân viên khi ấn nút sửa
     * @param {*} employeeId Mã của nhân viên
     * @param {*} employee thông tin của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async saveEmployeeByEdit(employeeId, employee) {
        return await BaseAPIConfig.put(`${this.controller}/${employeeId}`, employee);
    }

    /**
     * Xóa nhân viên theo id
     * @param {*} employeeId Mã của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async deleteEmployee(employeeId) {
        return await BaseAPIConfig.delete(`${this.controller}/${employeeId}`);
    }

    /**
     * Lấy nhân viên theo số bản ghi 1 trang
     * @param {*} recordNumber Mã hoặc tên của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async getUserToken(email, password) {
        return await BaseAPIConfig.get(`${this.controller}/getUserToken?username=${email}&password=${password}`);
    }

    /**
     * Export excel
     * @returns
     * author: Nguyễn Đăng Dũng (15/08/2022)
     */
    async exportEmployee() {
        return await window.open('https://localhost:7152/api/v1/Employees/Export', 'Download');
    }

    async changePassword(employeeId, password, newPassword) {
        return await BaseAPIConfig.put(`${this.controller}/changePassword/${employeeId}?oldPassword=${password}&newPassword=${newPassword}`);
    }
}
export default new EmployeeAPI();
