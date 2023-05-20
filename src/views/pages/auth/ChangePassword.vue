<script setup>
import { useLayout } from '@/layout/composables/layout';
import { ref, computed } from 'vue';
import AppConfig from '@/layout/AppConfig.vue';

const { layoutConfig } = useLayout();
const password = ref('');
const newPassword = ref('');
const confirmPassword = ref('');

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

        async signInOnClick(password, newPassword, confirmPassword) {
            var me = this;
            try {
                if (newPassword && password) {
                    if (newPassword == confirmPassword) {
                        const token = localStorage.getItem('token');
                        me.$store.state.employeeId = jwt.decode(token).sub;
                        const employeeId = me.$store.state.employeeId;
                        await EmployeeApi.changePassword(employeeId, password, newPassword).then(
                            (res) => {
                                if (res.data == true) {
                                    me.setPayloadToast('Đổi mật khẩu thành công!', 'success', 2000);
                                    me.$store.dispatch('showToast', me.payloadToast);
                                    setTimeout(() => {
                                        if (localStorage.getItem('token')) {
                                            localStorage.removeItem('token');
                                        }
                                        if (sessionStorage.getItem('token')) {
                                            sessionStorage.removeItem('token');
                                        }
                                        router.push({ name: 'login', params: {} });
                                    }, 2000);
                                } else {
                                    me.setPayloadToast('Mật khẩu cũ không đúng vui lòng thử lại!', 'error', 2000);
                                    me.$store.dispatch('showToast', me.payloadToast);
                                }
                            },
                            (err) => {
                                console.log(err);
                            }
                        );
                    } else {
                        me.setPayloadToast('Mật khẩu xác nhận không trùng với mật khẩu mới!', 'error', 2000);
                        me.$store.dispatch('showToast', me.payloadToast);
                    }
                }
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
                        <div class="text-900 text-3xl font-medium mb-3">Change Password!</div>
                        <!-- <span class="text-600 font-medium">Sign in to continue</span> -->
                    </div>

                    <div>
                        <label for="password1" class="block text-900 font-medium text-xl mb-2">Mật khẩu cũ</label>
                        <Password id="password1" v-model="password" placeholder="Old Password" :toggleMask="true" class="w-full mb-3" inputClass="w-full" inputStyle="padding:1rem"></Password>

                        <label for="password2" class="block text-900 font-medium text-xl mb-2">Mật khẩu mới</label>
                        <Password id="password2" v-model="newPassword" placeholder="New Password" :toggleMask="true" class="w-full mb-3" inputClass="w-full" inputStyle="padding:1rem"></Password>

                        <label for="password3" class="block text-900 font-medium text-xl mb-2">Xác nhận mật khẩu</label>
                        <Password id="password3" v-model="confirmPassword" placeholder="Confirm New Password" :toggleMask="true" class="w-full mb-3" inputClass="w-full" inputStyle="padding:1rem"></Password>
                        <Button label="Sign In" class="w-full p-3 text-xl" @click="signInOnClick(password, newPassword, confirmPassword)"></Button>
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
