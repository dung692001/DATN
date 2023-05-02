<script>
import { uuidv4 } from '../../JS/uuid';
import { useFileDialog } from '@vueuse/core';
import { ref as storageRef } from 'firebase/storage';
import { useFirebaseStorage, useStorageFile } from 'vuefire';
import { watch } from 'vue';
export default {
    emits: ['url-bind'],
    setup(props, { emit }) {
        const storage = useFirebaseStorage();
        var linkImg = props.urlLink ? props.urlLink : 'images/' + uuidv4();
        var mountainFileRef = storageRef(storage, linkImg);
        const { url, uploadProgress, uploadError, uploadTask, upload } = useStorageFile(mountainFileRef);

        const { files, open, reset } = useFileDialog();

        watch(files, async (newFiles) => {
            const file = newFiles[0];
            if (file) {
                await upload(file);

                emit('url-bind', linkImg);
            }
        });

        function uploadPicture() {
            const data = files.value?.item(0);
            if (data) {
                upload(data);
            }

            emit('url-bind', linkImg);
        }

        return {
            storage,
            url,
            uploadProgress,
            uploadError,
            uploadTask,
            uploadPicture,
            open,
            reset,
            files,
            linkImg,
            mountainFileRef
        };
    },

    watch: {
        urlLink(newVal) {
            this.linkImg = 'images/' + newVal;
            this.mountainFileRef = storageRef(this.storage, this.linkImg);
            this.url = useStorageFile(this.mountainFileRef).url;
        },
        isReset() {
            this.mountainFileRef = storageRef(this.storage, this.linkImg);
            this.url = useStorageFile(this.mountainFileRef).url;
        }
    },
    props: {
        typeImage: {
            type: String,
            default: '*/*'
        },
        urlLink: {
            type: String,
            required: false,
            default: null
        },
        idUpload: {
            type: String,
            default: ''
        },
        isReset: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            file: '',
            srcLink: '',
            filename: '',
            urlCheck: ''
        };
    }
};
</script>

<template>
    <div class="upload-container">
        <label class="upload-main" :for="idUpload" @click="open({ accept: 'image/*', multiple: false })">
            <div class="icon-add" v-if="!url">
                <i class="fas fa-plus fa-lg"></i>
            </div>
            <img :src="url" alt="Ảnh" class="upload-image" v-if="url" />
        </label>
    </div>
</template>

<style lang="scss">
.upload-container {
    display: flex;
    height: 120px;
    width: 80px;
    align-items: center;
    justify-content: center;
    border: 2px solid;
    border-radius: 4px;
    cursor: pointer;
    &:hover {
        opacity: 0.8;
    }
    .upload-main {
        cursor: pointer;
        display: flex;
        height: 120px;
        width: 80px;
        align-items: center;
        justify-content: center;
        margin: unset;
    }
    .icon-add {
        transform: scale(3);
    }
    .upload-image {
        width: 80px;
        height: 120px;
        object-fit: cover;
    }
}
</style>
