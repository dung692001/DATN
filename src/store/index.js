import { createStore } from 'vuex';
import { toast } from '../JS/toastMes';
export default createStore({
    state: {
        isLoggedIn: false,
        role: '',
        accountName: '',
        employeeId: '',
        toastContent: '',
        toastType: '',
        toastDuration: 0,
        employeeInfo: {}
    },
    mutations: {},
    actions: {
        showToast(context, payload) {
            toast({
                message: payload.toastContent,
                type: payload.toastType,
                duration: payload.toastDuration
            });
        }
    }
});
