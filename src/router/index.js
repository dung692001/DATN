import { createRouter, createWebHashHistory } from 'vue-router';
import jwt from 'jsonwebtoken';

import store from '../store/index';

import AppLayout from '@/layout/AppLayout.vue';
import EmployeeList from '@/components/pages/employee/EmployeeList.vue';
import DepartmentList from '@/components/pages/department/DepartmentList.vue';
import OrganizationList from '@/components/pages/organization/OrganizationList.vue';
import PositiosList from '@/components/pages/positions/PositionsList.vue';
import DemoPage from '@/views/uikit/Employee.vue';
import Login from '@/views/pages/auth/Login.vue';
import ChangePassword from '@/views/pages/auth/ChangePassword.vue';
import EmployeeProfile from '@/components/pages/employee/EmployeeProfile.vue';
const routes = [
    {
        path: '/',
        name: '/',
        redirect: '/layout'
    },
    {
        path: '/auth/login',
        name: 'login',
        component: Login
    },
    {
        path: '/auth/changePassword',
        name: 'changPassword',
        component: ChangePassword,
        meta: { requiresAuth: true, permission: 'admin, employee' }
    },
    {
        path: '/layout',
        component: AppLayout,
        beforeEnter: requireAdmin,
        meta: { requiresAuth: true },
        children: [
            {
                path: '/src/components/pages/employee/employeeList',
                name: 'employeeList',
                component: EmployeeList,
                meta: { requiresAuth: true, permission: 'admin' }
            },
            {
                path: '/src/components/pages/department/departmentList',
                name: 'departmentList',
                component: DepartmentList,
                meta: { requiresAuth: true, permission: 'admin' }
            },
            {
                path: '/src/components/pages/organization/organizationList',
                name: 'organizationList',
                component: OrganizationList,
                meta: { requiresAuth: true, permission: 'admin' }
            },
            {
                path: '/src/components/pages/positions/positionsList',
                name: 'positionsList',
                component: PositiosList,
                meta: { requiresAuth: true, permission: 'admin' }
            },
            {
                path: '/uikit/employee',
                name: 'employee',
                component: DemoPage,
                meta: { requiresAuth: true, permission: 'employee' }
            },
            {
                path: '/src/components/pages/employee/employeeProfile',
                name: 'employeeProfile',
                component: EmployeeProfile,
                meta: { requiresAuth: true, permission: 'admin' }
            }
        ]
    }
];

const router = createRouter({
    history: createWebHashHistory(import.meta.env.BASE_URL),
    routes,
    linkActiveClass: 'active'
});

router.beforeEach((to, from, next) => {
    console.log(from);
    if (to.matched.some((record) => record.meta.requiresAuth)) {
        if (!isLoggedIn() || !store.state.isLoggedIn) {
            next({
                path: '/auth/login',
                query: { redirect: to.fullPath }
            });
        } else {
            const permit = to.meta.permission;
            if (permit && permit.indexOf(store.state.role) >= 0) {
                next();
            }
        }
    } else {
        next();
    }
});

// Hàm check login với token
function isLoggedIn() {
    const token = localStorage.getItem('token');
    store.state.isLoggedIn = true;

    // Kiểm tra xem token đã được lưu trong sessionStorage chưa
    if (token) {
        // Giải mã token để kiểm tra tính hợp lệ của nó
        const decodedToken = jwt.decode(token);
        store.state.role = jwt.decode(token).role;

        // Kiểm tra xem token có hết hạn hay không
        const expirationDate = new Date(decodedToken.exp * 1000);
        if (expirationDate <= new Date()) {
            // Nếu token đã hết hạn, xóa nó khỏi sessionStorage và trả về false
            localStorage.removeItem('token');
            return false;
        } else {
            // Nếu token hợp lệ, trả về true
            return true;
        }
    } else {
        // Nếu token không tồn tại trong sessionStorage, trả về false
        return false;
    }
}

// Hàm check role
function requireAdmin(to, from, next) {
    const token = localStorage.getItem('token');

    if (!isLoggedIn(token)) {
        next('/auth/login');
    } else {
        //const decodedToken = jwt.decode(token);
        // if (decodedToken.role !== 'admin') {
        //     store.dispatch('showToast', 'Tài khoản không có quyền truy cập!');
        //     next(from.path);
        // } else {
        //     next();
        // }
        next();
    }
}

export default router;
