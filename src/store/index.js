import { createStore } from 'vuex';

export default createStore({
    state: {
        isLoggedIn: false,
        role: '',
        accountName: ''
    }
});
