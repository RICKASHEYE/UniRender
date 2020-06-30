window.onload = new function () {
    console.log("loaded the first instance of subright engine!");
    initWebGLContainer();
}

var gl = null;

function initWebGLContainer() {
    const canvas = $("glCanvas");
    //Initialise the GL context
    gl = canvas.getContext("webgl");

    //Only continue if WebGL is avaliable and working
    if (gl === null) {
        alert("Unable to initialize WebGL. Your browser or machine may not support it!");
        return;
    }

    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.clear(gl.COLOR_BUFFER_BIT);
}

function drawCube() {

}