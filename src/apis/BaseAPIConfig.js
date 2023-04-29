import axios from 'axios';
let BaseAPIConfig = axios.create({ baseURL: 'https://localhost:7084/api/v1/' });
/**
 * Chú ý: Khi call api thậy thì mở comment ra, thay baseURL thành api Backend cần gọi
 */
// let BaseAPIConfig = axios.create({ baseURL: "http://localhost:56886/api/" });

BaseAPIConfig.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error && error.response && error.response.status == 401) {
            console.log('expired');
        }
        return Promise.reject(error);
    }
);
export default BaseAPIConfig;
