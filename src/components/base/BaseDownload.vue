<script>
import { ref as storageRef } from 'firebase/storage';

import { useFirebaseStorage, useStorageFileUrl } from 'vuefire';

export default {
    props: {
        linkImg: {
            type: String,

            required: true
        }
    },

    setup(props) {
        const storage = useFirebaseStorage();

        const mountainFileRef = storageRef(storage, props.linkImg);

        const { url } = useStorageFileUrl(mountainFileRef);

        console.log(url);

        return {
            url
        };
    }
};
</script>
<template>
    <img class="posterLink" :src="url" />
</template>

<style lang="css">
.posterLink {
    height: 55px;
    width: 40px;
    text-align: center;
    line-height: 32px;
    border-radius: 10%;
    margin: 0;
    padding: 0;
    font-size: 12px;
    color: #1f1f20;
}
</style>
