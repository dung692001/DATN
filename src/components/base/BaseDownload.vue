<script>
import { ref as storageRef } from 'firebase/storage';
import { uuidv4 } from '../../JS/uuid';
import { useFirebaseStorage, useStorageFileUrl } from 'vuefire';

export default {
    props: {
        urlLink: {
            type: String,
            required: true
        }
    },

    setup(props) {
        const storage = useFirebaseStorage();
        var linkImg = props.urlLink ? props.urlLink : 'images/' + uuidv4();
        const mountainFileRef = storageRef(storage, linkImg);

        const { url } = useStorageFileUrl(mountainFileRef);

        console.log(url);

        return {
            storage,
            url,
            linkImg,
            mountainFileRef
        };
    },

    watch: {
        urlLink(newVal) {
            this.linkImg = newVal;
            this.mountainFileRef = storageRef(this.storage, this.linkImg);
            this.url = useStorageFileUrl(this.mountainFileRef).url;
        }
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
