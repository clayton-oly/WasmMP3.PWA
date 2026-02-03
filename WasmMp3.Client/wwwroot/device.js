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
