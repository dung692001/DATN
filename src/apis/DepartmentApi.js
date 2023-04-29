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
}
export default new DepartmentAPI();
