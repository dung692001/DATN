import BaseAPIConfig from '@/apis/BaseAPIConfig.js';

class PositionsAPI {
    controller = 'Positions';

    getTest() {
        return BaseAPIConfig.get(`https://catfact.ninja/fact`);
    }
    /**
     * Lấy toàn bộ chức danh
     * @param {}
     * @returns
     * author: Nguyễn Đăng Dũng (05/01/2023)
     */
    async getAllPositions() {
        return await BaseAPIConfig.get(`${this.controller}`);
    }
}
export default new PositionsAPI();
