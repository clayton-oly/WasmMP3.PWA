// Use as versões compatíveis para garantir que os objetos 'firebase' fiquem globais
importScripts('https://www.gstatic.com/firebasejs/10.8.0/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/10.8.0/firebase-messaging-compat.js');

const firebaseConfig = {
    apiKey: "AIzaSyCqlTqaaT7tjQrRy4E23mgp0wYcLaAFXa0",
    authDomain: "mp3-pwa.firebaseapp.com",
    projectId: "mp3-pwa",
    storageBucket: "mp3-pwa.firebasestorage.app",
    messagingSenderId: "459762706933",
    appId: "1:459762706933:web:a85325e54e9461155c98bc"
};

// Inicializa o Firebase dentro do Worker
firebase.initializeApp(firebaseConfig);

try {
    const messaging = firebase.messaging();
} catch (e) {
    console.error("Erro ao inicializar messaging no worker:", e);
}
