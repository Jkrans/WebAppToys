
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
            { data: "name", width: "75%" },
            {
                data: "id", width: "25%",
                "render": function (data) {
                    return `<div class="text-center">
                            
                            <a href="./Listings/details?id=${data}"
                            class ="btn btn-primary text-white" style="cursor:pointer; width=100px;">View Details</a>
                            
                    </div>`;
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
};


