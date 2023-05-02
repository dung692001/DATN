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
    height: 50px;
    width: 40px;
}
</style>
