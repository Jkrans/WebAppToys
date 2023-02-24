
var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/listing",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: "name", width: "40%" },
            { data: "description", width: "60%" }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
};


