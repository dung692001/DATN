<script setup></script>
<script>
//import { mapMutations } from 'vuex';
import '@fortawesome/fontawesome-free/css/all.css';
import jwt from 'jsonwebtoken';
import router from '@/router/index';
import EmployeeApi from '@/apis/EmployeeApi';
export default {
    name: 'App',
    components: {},
    created() {
        if (localStorage.getItem('token')) {
            const token = localStorage.getItem('token');
            const decodedToken = jwt.decode(token);
            this.$store.state.role = decodedToken.role;
            this.$store.state.isLoggedIn = true;
            this.$store.state.employeeId = decodedToken.sub;
            this.getEmployee(decodedToken.sub);
            if (this.$store.state.role == 'admin') {
                router.push({ name: 'employeeList', params: {} });
            } else if (this.$store.state.role == 'employee') {
                router.push({ name: 'employee', params: {} });
            }
        }
    },

    methods: {
        /**
         * Phương thức get employee từ employeeId
         * @param employeeId: id truyền vào để lấy toàn bộ thông tin nhân viên
         * @Author NDDung (25/07/2022)
         */
        async getEmployee(employeeId) {
            let me = this;
            try {
                await EmployeeApi.getEmployeeById(employeeId).then(
                    (res) => {
                        me.$store.state.employeeInfo = {
                            EmployeeName: res.data.EmployeeName,
                            Avatar: res.data.Avatar,
                            OfficeEmail: res.data.OfficeEmail
                        };
                    },
                    (err) => {
                        console.log(err);
                    }
                );
            } catch (error) {
                console.log(error);
            }
        }
    }
};
</script>
<template>
    <router-view />
    <div id="toast"></div>
</template>

<style>
@import url(./css/employees.css);
</style>
