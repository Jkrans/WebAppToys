﻿var dataTable;

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
            { data: "name", width: "55%" },
            { data: "user_Id", visible: false },

            {
                data: "id", width: "45%",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="./Listings/Upsert?id=${data}"
                            class ="btn btn-success text-white" style="cursor:pointer; width:100px; margin-right: 5px;"> <i class="far fa-edit"></i> Edit</a>
                            <a href="./Listings/details?id=${data}"
                            class ="btn btn-primary text-white" style="cursor:pointer; width:100px; margin-right: 5px;"> View Details</a>
                            <a onClick=Delete('/api/listing/'+${data})
                            class ="btn btn-danger text-white" style="cursor:pointer; width:100px;"> <i class="far fa-trash-alt"></i> Delete</a>
                    </div>`;
                }
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%",
        "order": [[0, "asc"]],
        "createdRow": function (row, data, dataIndex) {
            $(row).addClass('align-middle');
        }
    });
};



function Delete(url) {
    swal({
        title: "Are you sure you want to delete this item?",
        text: "You will not be able to restore this data!",
        icon: "warning",
        buttons: [true, "Confirm Delete"], // multiple buttons e.g. cancel and ok buttons
        dangerMode: [true] // sets the default focus on the cancel button, so you don't accidentally delete something, etc.
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else
                        toastr.error(data.message);


                }
            })
        }
    })


}


