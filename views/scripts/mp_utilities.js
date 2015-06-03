function getQueryStringByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function getObjectIndexOfArray(key, value, items) {
    var return_value = -1;
    for (var i = 0; i < items.length; i++) {
        if ((key in items[i]) && items[i][key] == value) {
            return_value= i;
            break;
        }
    }
    return return_value;
}