<div>
    <button id="showUserListings" class="btn btn-secondary">Show Only My Listings</button>
</div>

<script>
    using System.Security.Claims;

    var showOnlyUserListings = false;

    $(document).ready(function() {
        var userId = '@User.FindFirstValue(ClaimTypes.NameIdentifier)'; // get current user's ID
        var dataTable = $('#DT_load').DataTable({
            "ajax": {
                "url": "/api/listing",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { data: "name", width: "45%" },
                { data: "user_Id", width: "10%"},
                {
                    data: "id", width: "45%",
                    "render": function (data) {
                        return `<div class="text-center">
                                <a href="./Listings/Upsert?id=${data}"
                                class ="btn btn-success text-white" style="cursor:pointer; width=100px;"> <i class="far fa-edit"></i>Edit</a>
                                <a href="./Listings/details?id=${data}"
                                class ="btn btn-primary text-white" style="cursor:pointer; width=100px;">View Details</a>
                                <a onClick=Delete('/api/listing/'+${data})
                                class ="btn btn-danger text-white" style="cursor:pointer; width=100px;"> <i class="far fa-trash-alt"></i>Delete</a>
                        </div>`;
                    }
                },
                 // hide User_Id column
            ],
            "language": {
                "emptyTable": "no data found."
            },
            "width": "100%"
        });

        $('#showUserListings').on('click', function() {
            showOnlyUserListings = !showOnlyUserListings;
            if (showOnlyUserListings) {
                dataTable.columns(2).search(userId).draw(); // search User_Id column for current user's ID
                $(this).text('Show All Listings'); // update button text
            } else {
                dataTable.columns(2).search('').draw(); // clear search and show all listings
                $(this).text('Show Only My Listings'); // update button text
            }
        });
    });
</script>
