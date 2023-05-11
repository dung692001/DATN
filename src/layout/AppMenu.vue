<script setup>
import { ref } from 'vue';
import store from '../store/index';
import AppMenuItem from './AppMenuItem.vue';
const userRole = ['employee', 'admin'];
const model = ref([
    {
        label: 'Quản lí',
        role: userRole[1]
    },
    {
        label: 'Người dùng',
        role: userRole[0]
    },
    {
        label: '',
        items: [{ label: 'DS Nhân viên', icon: 'pi pi-fw pi-user', to: '/src/components/pages/employee/employeeList' }],
        role: userRole[1]
    },
    {
        label: '',
        items: [{ label: 'Phòng ban', icon: 'pi pi-fw pi-user', to: '/src/components/pages/department/departmentList' }],
        role: userRole[1]
    },
    {
        label: '',
        items: [{ label: 'Dự án', icon: 'pi pi-fw pi-user', to: '/src/components/pages/organization/organizationList' }],
        role: userRole[1]
    },
    {
        label: '',
        items: [{ label: 'Chức danh', icon: 'pi pi-fw pi-user', to: '/src/components/pages/positions/positionsList' }],
        role: userRole[1]
    },
    {
        label: '',
        items: [{ label: 'Hồ sơ nhân viên', icon: 'pi pi-fw pi-user', to: '/uikit/employee' }],
        role: userRole[0]
    }
]);
</script>
<script>
export default {
    name: 'AppMenu',
    components: {},

    //GĐ 2: created (setup)
    created() {
        this.getRole();
    },
    methods: {
        getRole() {
            this.currentRole = store.state.role;
        }
    },
    data() {
        return {
            // quyền người dùng hiện tại
            currentRole: ''
        };
    }
};
</script>

<template>
    <ul class="layout-menu">
        <template v-for="(item, i) in model" :key="item">
            <app-menu-item v-if="!item.separator && item.role == currentRole" :item="item" :index="i"></app-menu-item>
            <li v-if="item.separator" class="menu-separator"></li>
        </template>
    </ul>
</template>

<style lang="scss" scoped></style>
