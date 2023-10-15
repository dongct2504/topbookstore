let dataTable;

$(document).ready(() => {
    dataTable = $('#datatable-book').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/admin/book/getAllBooks"
        },
        "columns": [
            { "data": "title", "width": "20%" },
            { "data": "price", "width": "10%" },
            { "data": "numberOfPages", "width": "10%" },
            { "data": "author.fullName", "width": "20%" },
            {
                "data": "categories",
                "render": (data) => {
                    let categories = '';
                    data.forEach(category => {
                        categories += category.name + ', ';
                    });
                    categories = categories.slice(0, -2);
                    return categories;
                },
                "width": "25%"
            },
            {
                "data": "bookId",
                "render": (data) => {
                    return `
                        <div class="text-center">
                            <a class="btn btn-success text-white mb-2 mt-2"
                                href="/admin/book/upsert/${data}">
                                <span class="fas fa-edit"></span>&nbsp;Sửa
                            </a>
                            <a class="btn btn-danger text-white mb-2 mt-2"
                                onclick=Delete("/admin/book/deleteBook/${data}")>
                                <span class="fas fa-trash-alt"></span>&nbsp;Xóa
                            </a>
                        </div>
                        `;
                },
                "width": "15%"
            }
        ]
    });
});