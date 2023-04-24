<script>
export default {
    name: 'BaseMenuItem',
    props: {
        // icon của menu
        menuIcon: {
            type: String,
            required: true
        },
        // nội dung của menu item
        menuTitle: {
            type: String,
            required: true
        },
        // link truyền vào để dùng router link
        menuLink: {
            type: String,
            required: true
        },
        // biến ẩn hiện của class margin-0
        isOpen: {
            type: Boolean
        }
    }
};
</script>

<template>
    <router-link :to="menuLink" class="menu__item">
        <div class="menu__item__icon" :class="{ 'margin-0': !isOpen }">
            <div class="icon" :class="menuIcon"></div>
        </div>
        <div class="menu__item__title" :tooltip-title="menuTitle" :class="{ isShowBlock: isOpen }">
            {{ menuTitle }}
        </div>
    </router-link>
</template>

<style scoped></style>
