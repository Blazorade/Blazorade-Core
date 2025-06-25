
export function getClientTimezoneOffset() {
    return new Date().getTimezoneOffset();
}

export function scrollElementIntoView(element) {
    console.debug("Scrolling element into view:", element);

    if (element) {

        element.scrollIntoView({ behavior: "smooth", block: "start" });
    }
}

export function scrollElementIntoViewById(elementId) {
    console.debug("Scrolling element into view by ID:", elementId);

    var elem = document.getElementById(elementId);
    scrollElementIntoView(elem);
}
