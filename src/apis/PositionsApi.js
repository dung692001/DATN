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
    /**
     * Lấy nhân viên theo id
     * @param {*} positionsId id nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async getPositionsById(positionsId) {
        return await BaseAPIConfig.get(`${this.controller}/${positionsId}`);
    }

    /**
     * Lưu nhân viên khi ấn nút thêm
     * @param {*} positions thông tin của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async savePositionsByAdd(positions) {
        return await BaseAPIConfig.post(`${this.controller}`, positions);
    }

    /**
     * Lưu nhân viên khi ấn nút sửa
     * @param {*} positionsId Mã của nhân viên
     * @param {*} positions thông tin của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (26/07/2022)
     */
    async savePositionsByEdit(positionsId, positions) {
        return await BaseAPIConfig.put(`${this.controller}/${positionsId}`, positions);
    }

    /**
     * Xóa nhân viên theo id
     * @param {*} positionsId Mã của nhân viên
     * @returns
     * author: Nguyễn Đăng Dũng (07/05/2022)
     */
    async deletePositions(positionsId) {
        return await BaseAPIConfig.delete(`${this.controller}/${positionsId}`);
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
export default new PositionsAPI();
