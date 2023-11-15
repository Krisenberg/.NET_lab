function getRelativeCoordinates(event, canvas) {
    const rect = canvas.getBoundingClientRect();
    const x = event.clientX - rect.left;
    const y = event.clientY - rect.top;
    return { x, y };
}

window.addEventListener("load", function () {

    var elements = document.getElementsByClassName("drawingCanvas");

    for (var i = 0; i < elements.length; i++) {
        setupCanvas(elements[i]);
    }

});

function setupCanvas(canvas) {

    var ctx = canvas.getContext("2d");

    canvas.addEventListener("mousemove", function (e) {

        const coords = getRelativeCoordinates(e, canvas);

        ctx.clearRect(0, 0, canvas.width, canvas.height);
        ctx.beginPath();
        ctx.moveTo(0, 0);
        ctx.lineTo(coords.x, coords.y);
        ctx.moveTo(0, canvas.height);
        ctx.lineTo(coords.x, coords.y);
        ctx.moveTo(canvas.width, 0);
        ctx.lineTo(coords.x, coords.y);
        ctx.moveTo(canvas.width, canvas.height);
        ctx.lineTo(coords.x, coords.y);
        ctx.strokeStyle = "red";
        ctx.lineWidth = 2;
        ctx.stroke();
    })

    canvas.addEventListener('mouseleave', function () {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    });
};