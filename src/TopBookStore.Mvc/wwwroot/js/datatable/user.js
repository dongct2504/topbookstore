let dataTable;

$(document).ready(() => {
    dataTable = $('#datatable-user').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/admin/user/getAllUsers"
        },
        "columns": [
            { "data": "lastName", "width": "15%" },
            { "data": "firstName", "width": "15%" },
            { "data": "email", "width": "20%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": "categoryId",
                "render": (data) => {
                    return `
                        <div class="text-center">
                            <a class="btn btn-success text-white"
                                href="/admin/category/upsert/${data}">
                                <span class="fas fa-edit"></span>&nbsp;Sửa
                            </a>
                            <a class="btn btn-danger text-white"
                                onclick=Delete("/admin/category/deleteCategory/${data}")>
                                <span class="fas fa-trash-alt"></span>&nbsp;Xóa
                            </a>
                        </div>
                        `;
                },
                "width": "20%"
            }
        ]
    });
});