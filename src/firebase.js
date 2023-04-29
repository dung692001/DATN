// Import the functions you need from the SDKs you need
import { initializeApp } from 'firebase/app';
import { getFirestore, collection } from 'firebase/firestore';
import 'firebase/storage';
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
export const firebaseApp = initializeApp({
    apiKey: 'AIzaSyBREHMjG2NHXOfqncnTxPuLKKaEueE3ak0',
    authDomain: 'datn-f83eb.firebaseapp.com',
    projectId: 'datn-f83eb',
    storageBucket: 'datn-f83eb.appspot.com',
    messagingSenderId: '513254004965',
    appId: '1:513254004965:web:062cf2cd4b46c23462dc0c',
    measurementId: 'G-22EXGXTVJS'
});

// Initialize Firebase
// used for the firestore refs
const db = getFirestore(firebaseApp);

// here we can export reusable database references
export const todosRef = collection(db, 'todos');
