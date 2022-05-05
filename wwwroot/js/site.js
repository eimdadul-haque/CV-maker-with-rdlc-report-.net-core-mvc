

function photoUpload(event) {
    if (event.target.files.length > 0) {
        var src = URL.createObjectURL(event.target.files[0]);
        var obj = document.getElementById("image")
        obj.src = src;
    }
}