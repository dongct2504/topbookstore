let dataTable;

$(document).ready(() => {
    dataTable = $('#tblDataAuthor').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/admin/author/getAllAuthors",
        },
        "columns": [
            { "data": "firstName", "width": "25%" },
            { "data": "lastName", "width": "25%" },
            { "data": "phoneNumber", "width": "20%" },
            {
                "data": "authorId",
                "render": (data) => {
                    return `
                        <div class="text-center">
                            <a class="btn btn-success text-white"
                                href="/admin/author/upsert/${data}">
                                <span class="fas fa-edit"></span>&nbsp;Sửa
                            </a>
                            <a class="btn btn-danger text-white"
                                onclick=Delete("/admin/author/deleteAuthor/${data}")>
                                <span class="fas fa-trash-alt"></span>&nbsp;Xóa
                            </a>
                        </div>
                        `
                },
                "width": "30%"
            }
        ]
    });
});