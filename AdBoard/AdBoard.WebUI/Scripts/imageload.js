function readURL(input, s) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(s)
                .attr('src', e.target.result)
        };

        reader.readAsDataURL(input.files[0]);
    }
}