import BaseAPIConfig from '@/apis/BaseAPIConfig.js';

class OrganizationAPI {
    controller = 'Organization';

    getTest() {
        return BaseAPIConfig.get(`https://catfact.ninja/fact`);
    }
    /**
     * Lấy toàn bộ chức danh
     * @param {}
     * @returns
     * author: Nguyễn Đăng Dũng (05/01/2023)
     */
    async getAllOrganization() {
        return await BaseAPIConfig.get(`${this.controller}`);
    }

    /**
     * Lấy nhân viên theo id
     * @param {*} organizationId id nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async getOrganizationById(organizationId) {
        return await BaseAPIConfig.get(`${this.controller}/${organizationId}`);
    }

    /**
     * Lưu nhân viên khi ấn nút thêm
     * @param {*} organization thông tin của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async saveOrganizationByAdd(organization) {
        return await BaseAPIConfig.post(`${this.controller}`, organization);
    }

    /**
     * Lưu nhân viên khi ấn nút sửa
     * @param {*} organizationId Mã của nhân viên
     * @param {*} organization thông tin của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async saveOrganizationByEdit(organizationId, organization) {
        return await BaseAPIConfig.put(`${this.controller}/${organizationId}`, organization);
    }

    /**
     * Xóa nhân viên theo id
     * @param {*} organizationId Mã của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (07/05/2022)
     */
    async deleteOrganization(organizationId) {
        return await BaseAPIConfig.delete(`${this.controller}/${organizationId}`);
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
export default new OrganizationAPI();
