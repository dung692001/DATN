import BaseAPIConfig from '@/apis/BaseAPIConfig.js';

class AttendanceStatistAPI {
    controller = 'AttendanceStatist';

    getTest() {
        return BaseAPIConfig.get(`https://catfact.ninja/fact`);
    }

    /**
     * Lấy nhân viên theo số bản ghi 1 trang
     * @param {*} recordNumber Mã hoặc tên của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async getAttendanceStatistByWorkYear(employeeId, workYear) {
        return await BaseAPIConfig.get(`${this.controller}/getAttendanceStatistByWorkYear?EmployeeId=${employeeId}&WorkYear=${workYear}`);
    }
}
export default new AttendanceStatistAPI();
