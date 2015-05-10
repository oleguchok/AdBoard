function DecideToMakeAStar(id) {
    if (id == 'empty-star') {
        document.getElementById(id).className = "glyphicon glyphicon-star"
    }
    if (id == 'fill-star') {
        document.getElementById(id).className = "glyphicon glyphicon-star-empty"
    }
    return false;
}