<script setup>
import { useLayout } from '@/layout/composables/layout';
import { ref, computed } from 'vue';
import AppConfig from '@/layout/AppConfig.vue';

const { layoutConfig } = useLayout();
const email = ref('');
const password = ref('');
const checked = ref(false);

const logoUrl = computed(() => {
    return `layout/images/${layoutConfig.darkTheme.value ? 'logo-white' : 'logo-dark'}.svg`;
});
</script>

<script>
import EmployeeApi from '../../../apis/EmployeeApi.js';
import router from '@/router/index';
import jwt from 'jsonwebtoken';
export default {
    methods: {
        setPayloadToast(content, type, duration) {
            this.payloadToast = {
                toastContent: content,
                toastType: type,
                toastDuration: duration
            };
        },

        async signInOnClick(email, password) {
            var me = this;
            try {
                if (email && password) {
                    await EmployeeApi.getUserToken(email, password).then(
                        (res) => {
                            if (res.data) {
                                localStorage.setItem('token', res.data);
                                sessionStorage.setItem('token', res.data);
                                const token = localStorage.getItem('token');
                                const decodeValue = jwt.decode(token);
                                me.$store.state.role = decodeValue.role;
                                me.$store.state.employeeId = decodeValue.sub;
                                me.$store.state.isLoggedIn = true;
                                me.setPayloadToast('Đăng nhập thành công!', 'success', 2000);
                                me.$store.dispatch('showToast', me.payloadToast);
                                me.getEmployee(decodeValue.sub);
                                setTimeout(() => {
                                    if (me.$store.state.role == 'admin') {
                                        router.push({ name: 'employeeList', params: {} });
                                    } else if (me.$store.state.role == 'employee') {
                                        router.push({ name: 'employee', params: {} });
                                    }
                                }, 500);

                                if (!res.data) {
                                    me.$store.state.isLoggedIn = false;
                                }
                                me.$store.state.isLoggedIn = true;
                            } else {
                                me.setPayloadToast('Tài khoản hoặc mật khẩu sai, vui lòng thử lại!', 'error', 2000);
                                me.$store.dispatch('showToast', me.payloadToast);
                            }
                        },
                        (err) => {
                            console.log(err);
                        }
                    );
                }
            } catch (error) {
                ``;
                console.log(error);
            }
        },

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
    },
    data() {
        return {
            payloadToast: {}
        };
    }
};
</script>

<template>
    <div class="surface-ground flex align-items-center justify-content-center min-h-screen min-w-screen overflow-hidden">
        <div class="flex flex-column align-items-center justify-content-center">
            <img :src="logoUrl" alt="Sakai logo" class="mb-5 w-6rem flex-shrink-0" />
            <div style="border-radius: 56px; padding: 0.3rem; background: linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%)">
                <div class="w-full surface-card py-8 px-5 sm:px-8" style="border-radius: 53px">
                    <div class="text-center mb-5">
                        <img src="/demo/images/login/avatar.png" alt="Image" height="50" class="mb-3" />
                        <div class="text-900 text-3xl font-medium mb-3">Welcome!</div>
                        <span class="text-600 font-medium">Đăng nhập để tiếp tục</span>
                    </div>

                    <div>
                        <label for="email1" class="block text-900 text-xl font-medium mb-2">Tài Khoản</label>
                        <InputText id="email1" type="text" placeholder="Email address" class="w-full md:w-30rem mb-5" style="padding: 1rem" v-model="email" />

                        <label for="password1" class="block text-900 font-medium text-xl mb-2">Mật Khẩu</label>
                        <Password id="password1" v-model="password" placeholder="Password" :toggleMask="true" class="w-full mb-3" inputClass="w-full" inputStyle="padding:1rem"></Password>

                        <div class="flex align-items-center justify-content-between mb-5 gap-5">
                            <div class="flex align-items-center">
                                <Checkbox v-model="checked" id="rememberme1" binary class="mr-2"></Checkbox>
                                <label for="rememberme1">Lưu tài khoản</label>
                            </div>
                            <a class="font-medium no-underline ml-2 text-right cursor-pointer" style="color: var(--primary-color)">Quên mật khẩu?</a>
                        </div>
                        <Button label="Sign In" class="w-full p-3 text-xl" @click="signInOnClick(email, password)"></Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <AppConfig simple />
</template>

<style scoped>
.pi-eye {
    transform: scale(1.6);
    margin-right: 1rem;
}

.pi-eye-slash {
    transform: scale(1.6);
    margin-right: 1rem;
}
</style>
