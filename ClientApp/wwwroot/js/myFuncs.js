
function getDelayedData(args) {
    setTimeout(() => {
        args.successCallback.target
            .invokeMethodAsync(
                args.successCallback.methodName,
                args.data.data
            );
    }, 500);
}
