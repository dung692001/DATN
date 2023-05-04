<script setup></script>
<script>
//import { mapMutations } from 'vuex';
//import jwt from 'jsonwebtoken';
export default {
    name: 'App',
    components: {},
    created() {
        if (localStorage.getItem('token')) {
            const token = localStorage.getItem('token');
            //const decodedToken = jwt.decode(token);
            //this.$store.state.role = jwt.decode(token).role;
            this.$store.state.isLoggedIn = true;
            console.log(token);
        }
    }
};
</script>
<template>
    <router-view />
</template>

<style>
@import url(./css/employees.css);
</style>
