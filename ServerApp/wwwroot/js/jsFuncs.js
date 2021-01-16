

export function getDelayedTimeString(args) {
    setTimeout(() => {
        args.successCallback.target.invokeMethodAsync(args.successCallback.methodName, new Date().toLocaleTimeString());
    }, 500);
}