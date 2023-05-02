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
}
export default new OrganizationAPI();
