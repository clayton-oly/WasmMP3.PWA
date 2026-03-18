//device storage

window.deviceStorage = {
    get: function (key) {
        return localStorage.getItem(key);
    },
    set: function (key, value) {
        localStorage.setItem(key, value);
    },
    remove: function (key) {
        localStorage.removeItem(key);
    }
};


//device access

window.device = {
    isOnline: () => navigator.onLine,

    vibrate: (ms) => {
        if (navigator.vibrate) navigator.vibrate(ms);
    },

    onOnline: (dotNetObjRef) => {
        window.addEventListener("online", () => {
            dotNetObjRef.invokeMethodAsync("OnOnlineChanged", true);
        });
        window.addEventListener("offline", () => {
            dotNetObjRef.invokeMethodAsync("OnOnlineChanged", false);
        });
    }
};



//Clipboard (copiar/colar)
window.clipboard = {
    copyText: function (text) {
        try {
            navigator.clipboard.writeText(text);
            return true;
        } catch (err) {
            console.error("Erro ao copiar: ", err);
            return false;
        }
    },
    readText: function () {
        try {
            const text = navigator.clipboard.readText();
            return text;
        } catch (err) {
            console.error("Erro ao ler: ", err);
            return null;
        }
    }
};


// pegar nivel da bateria

window.battery = {
    getLevel: async function () {
        if (!navigator.getBattery) return -1;

        const battery = await navigator.getBattery();
        return battery.level * 100; // Retorna 0 a 100
    }
};

//GPS

window.getGeolocation = () => {
    return new Promise((resolve, reject) => {
        if (!navigator.geolocation) {
            reject("Geolocalização não suportada.");
        }
        navigator.geolocation.getCurrentPosition(
            (position) => {
                resolve({
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude
                });
            },
            (error) => {
                reject(error.message);
            }
        );
    });
};


// Camera
window.camera = {
    startVideo: async (videoElementId, useFrontCamera) => {
        const video = document.getElementById(videoElementId);

        // Define se usa 'user' (frontal) ou 'environment' (traseira)
        const facingMode = useFrontCamera ? 'user' : 'environment';

        const constraints = {
            video: {
                facingMode: { ideal: facingMode }
            }
        };

        if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
            const stream = await navigator.mediaDevices.getUserMedia(constraints);
            window.currentStream = stream; // Armazena o stream para parar depois
            video.srcObject = stream;
            video.play();
        }
    },

    takePicture: (videoElementId, canvasElementId) => {
        const video = document.getElementById(videoElementId);
        const canvas = document.getElementById(canvasElementId);
        const context = canvas.getContext('2d');
        canvas.width = video.videoWidth;
        canvas.height = video.videoHeight;
        context.drawImage(video, 0, 0, video.videoWidth, video.videoHeight);
        return canvas.toDataURL('image/png');
    },

    stopVideo: (videoElementId) => {
        const video = document.getElementById(videoElementId);
        if (window.currentStream) {
            window.currentStream.getTracks().forEach(track => track.stop());
            video.srcObject = null;
            window.currentStream = null;
        }
    }
};

//push notification
window.push = {
    getFCMToken: async (vapidKey) => {

        try {
            //aqui vai o seu firebaseConfig
            const firebaseConfig = {
                apiKey: "AIzaSyCqlTqaaT7tjQrRy4E23mgp0wYcLaAFXa0",
                authDomain: "mp3-pwa.firebaseapp.com",
                projectId: "mp3-pwa",
                storageBucket: "mp3-pwa.firebasestorage.app",
                messagingSenderId: "459762706933",
                appId: "1:459762706933:web:a85325e54e9461155c98bc"
            };

            // Verifica se já existe um app inicializado, se não, inicializa
            if (firebase.apps.length === 0) {
                firebase.initializeApp(firebaseConfig);
            }

            const messaging = firebase.messaging();
            const permission = await Notification.requestPermission();

            if (permission === 'granted') {
                return await messaging.getToken({ vapidKey: vapidKey });
            }
            return null;
        } catch (error) {
            console.error("Erro no Firebase JS:", error);
            return null;
        }
    }
};
