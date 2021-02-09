

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

export function getTimeStringOrError(args) {
    if (!args.data.error) {
        args.successCallback.target.invokeMethodAsync(args.successCallback.methodName, new Date().toLocaleTimeString());
    }
    else {
        args.failureCallback.target.invokeMethodAsync(args.failureCallback.methodName);
    }
}

export function devNull(args) {
    console.log("devNull", args);

}