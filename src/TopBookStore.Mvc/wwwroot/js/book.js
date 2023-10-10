let dataTable;

$(document).ready(() => {
    dataTable = $('#tblDataBook').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/admin/book/getAllBooks"
        },
        columns: [
            { "data": "title" },
            { "data": "isbn13" },
            { "data": "price" },
            { "data": "discountPercent" },
            { "data": "numberOfPages" },
            {
                "data": "categories",
                "render": (data) => {
                    let categories = '';
                    data.forEach(category => {
                        categories += category.name + ', ';
                    });
                    categories = categories.slice(0, -2);
                    return categories;
                }
            },
            { "data": "author.fullName" },
            { "data": "publisher.name" },
            {
                "data": "bookId",
                "render": (data) => {
                    return `
                        <div class="text-center">
                            <a class="btn btn-success text-white"
                                href="/admin/book/upsert/${data}">
                                <span class="fas fa-edit"></span>&nbsp;Sửa
                            </a>
                            <a class="btn btn-danger text-white"
                                onclick=Delete("/admin/book/deleteBook/${data}")>
                                <span class="fas fa-trash-alt"></span>&nbsp;Xóa
                            </a>
                        </div>
                        `;
                }
            }
        ]
    });
});