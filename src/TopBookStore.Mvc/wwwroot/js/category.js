$(document).ready(() => {
    const dataTable = $('#tblDataCategory').DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAllCategories"
        },
        "columns": [
            { "data": "name", "width": "60%" },
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
                                href="/Admin/Category/upsert/${data}">
                                <span class="fas fa-trash-alt"></span>&nbsp;Xóa
                            </a>
                        </div>
                        `;
                }, "width": "40%"
            }
        ]
    });
});