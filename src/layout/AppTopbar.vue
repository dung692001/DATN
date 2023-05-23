<script setup>
import { ref, computed, onMounted, onBeforeUnmount } from 'vue';
import { useLayout } from '@/layout/composables/layout';
import { useRouter } from 'vue-router';

const { layoutConfig, onMenuToggle } = useLayout();

const menu = ref(null);
const overlayMenuItems = ref([
    {
        label: 'ChangePassword',
        icon: 'pi pi-refresh',
        command: () => {
            router.push({ name: 'changPassword', params: {} });
        }
    },
    {
        label: 'Update',
        icon: 'pi pi-refresh'
    },
    {
        label: 'Delete',
        icon: 'pi pi-trash'
    },
    {
        separator: true
    },
    {
        label: 'Log Out',
        icon: 'pi pi-fw pi-sign-out',
        command: () => {
            if (localStorage.getItem('token')) {
                localStorage.removeItem('token');
            }
            if (sessionStorage.getItem('token')) {
                sessionStorage.removeItem('token');
            }
            router.push({ name: 'login', params: {} });
        }
    }
]);

const toggleMenu = (event) => {
    menu.value.toggle(event);
};

const outsideClickListener = ref(null);
const topbarMenuActive = ref(false);
const router = useRouter();

onMounted(() => {
    bindOutsideClickListener();
});

onBeforeUnmount(() => {
    unbindOutsideClickListener();
});

const logoUrl = computed(() => {
    return `layout/images/${layoutConfig.darkTheme.value ? 'logo-white' : 'logo-dark'}.svg`;
});

const onTopBarMenuButton = () => {
    topbarMenuActive.value = !topbarMenuActive.value;
};
const onSettingsClick = () => {
    topbarMenuActive.value = false;
    router.push('/documentation');
};
const topbarMenuClasses = computed(() => {
    return {
        'layout-topbar-menu-mobile-active': topbarMenuActive.value
    };
});

const bindOutsideClickListener = () => {
    if (!outsideClickListener.value) {
        outsideClickListener.value = (event) => {
            if (isOutsideClicked(event)) {
                topbarMenuActive.value = false;
            }
        };
        document.addEventListener('click', outsideClickListener.value);
    }
};
const unbindOutsideClickListener = () => {
    if (outsideClickListener.value) {
        document.removeEventListener('click', outsideClickListener);
        outsideClickListener.value = null;
    }
};
const isOutsideClicked = (event) => {
    if (!topbarMenuActive.value) return;

    const sidebarEl = document.querySelector('.layout-topbar-menu');
    const topbarEl = document.querySelector('.layout-topbar-menu-button');

    return !(sidebarEl.isSameNode(event.target) || sidebarEl.contains(event.target) || topbarEl.isSameNode(event.target) || topbarEl.contains(event.target));
};
</script>

<script>
import EmployeeApi from '@/apis/EmployeeApi';
export default {
    mounted() {
        this.dataLoadImg = '';
        this.getEmployee(this.$store.state.employeeId);
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
                        this.dataLoadImg = 'images/' + this.$store.state.employeeInfo.Avatar;
                        this.EmployeeName = this.$store.state.employeeInfo.EmployeeName;
                        this.UserName = this.$store.state.employeeInfo.OfficeEmail;
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
            dataLoadImg: '',
            EmployeeName: '',
            UserName: ''
        };
    }
};
</script>

<template>
    <div class="layout-topbar">
        <router-link to="/" class="layout-topbar-logo">
            <img :src="logoUrl" alt="logo" />
            <span>SAKAI</span>
        </router-link>

        <button class="p-link layout-menu-button layout-topbar-button" @click="onMenuToggle()">
            <i class="pi pi-bars"></i>
        </button>

        <button class="p-link layout-topbar-menu-button layout-topbar-button" @click="onTopBarMenuButton()">
            <i class="pi pi-ellipsis-v"></i>
        </button>

        <div class="layout-topbar-menu" :class="topbarMenuClasses">
            <button @click="onTopBarMenuButton()" class="p-link layout-topbar-button">
                <i class="pi pi-calendar"></i>
                <span>Calendar</span>
            </button>
            <button @click="toggleMenu" class="p-link layout-topbar-button">
                <Menu ref="menu" :model="overlayMenuItems" :popup="true" :class="'infor-menu-width'">
                    <template #start>
                        <button @click="profileClick" class="w-full p-link flex align-items-center p-2 pl-3 text-color hover:surface-200 border-noround flex-cutstom">
                            <BaseDownload :urlLink="dataLoadImg"> </BaseDownload>
                            <div class="flex flex-column align style-custom">
                                <span class="font-bold">{{ EmployeeName }}</span>
                                <span class="text-sm">{{ UserName }}</span>
                            </div>
                        </button>
                    </template>
                </Menu>
                <i class="pi pi-user"></i>
                <span>Profile</span>
            </button>
            <button @click="onSettingsClick()" class="p-link layout-topbar-button">
                <i class="pi pi-cog"></i>
                <span>Settings</span>
            </button>
        </div>
    </div>
</template>

<style lang="scss">
.infor-menu-width {
    width: 250px !important;
    .flex-cutstom {
        display: flex;
        flex-direction: column !important;
    }
    .style-custom {
        padding: 10px;
    }
}
</style>
