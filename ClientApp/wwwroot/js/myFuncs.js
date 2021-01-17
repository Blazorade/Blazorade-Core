

function getDelayedData(args) {
    console.log("getDelayedData", args);
    setTimeout(() => {
        console.log("timeout elapsed");
        args.successCallback.target.invokeMethodAsync(args.successCallback.methodName, args.data.data);
    }, 500);
}