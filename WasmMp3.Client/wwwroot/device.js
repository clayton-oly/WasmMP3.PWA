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
                    latitude: position.coords.latitudes,
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
    startVideo: async (videoElementId) => {
        const video = document.getElementById(videoElementId);
        if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
            const stream = await navigator.mediaDevices.getUserMedia({ video: true });
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
    }
};
