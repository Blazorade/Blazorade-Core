

export function getDelayedTimeString(args) {
    console.log("getDelayedTimeString", args);
    getDelayedTimeStringByCallback(args.successCallback);
}

export function getDelayedTimeStringByCallback(callback) {
    console.log("getDelayedTimeStringByCallback", callback);
    setTimeout(() => {
        callback.target.invokeMethodAsync(callback.methodName, new Date().toLocaleTimeString());
    }, 100);
}