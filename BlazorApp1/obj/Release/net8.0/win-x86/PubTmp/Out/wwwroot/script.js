/* Get the documentElement (<html>) to display the page in fullscreen */
var elem = document.documentElement;


/* View in fullscreen */
function openFullscreen(openFullscreenButton) {

    openFullscreenButton.classList.add("no-display");



    /* If fullscreen mode is available, show the element in fullscreen */
    if (elem.requestFullscreen) {
        elem.requestFullscreen();
    } else if (elem.webkitRequestFullscreen) { /* Safari */
        elem.webkitRequestFullscreen();
    } else if (elem.msRequestFullscreen) { /* IE11 */
        elem.msRequestFullscreen();
    }



    document.addEventListener("fullscreenchange", function () {

        if (document.fullscreenElement == null)

            openFullscreenButton.classList.remove("no-display");
    });



}