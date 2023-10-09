let dataTable;

$(document).ready(() => {
    dataTable = $('#tblDataCategory').DataTable({
        ajax: {
            "type": "GET",
            "url": "/Admin/Category/GetAllCategories"
        },
        columns: [
            { "data": "name"},
            {
                "data": "categoryId",
                "render": (data) => {
                    return `
                        <div class="text-center">
                            <a class="btn btn-success text-white"
                                href="/Admin/Category/Upsert/${data}">
                                <span class="fas fa-edit"></span>&nbsp;Sửa
                            </a>
                            <a class="btn btn-danger text-white"
                                onclick=Delete("/Admin/Category/DeleteCategory/${data}")>
                                <span class="fas fa-trash-alt"></span>&nbsp;Xóa
                            </a>
                        </div>
                        `;
                }
            }
        ]
    });
});

function Delete(url) {
    swal({
        "title": "Bạn có chắc muốn xóa!",
        "text": "Khi xóa sẽ xóa vĩnh viễn và sẽ không thể hồi phục!",
        "icon": "warning",
        "buttons": true,
        "dangerMode": true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                "type": "DELETE",
                "url": url,
                success: (data) => {
                    if (data.success) { // if success is then then get the data
                        toastr.success(data.message)
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    });
};