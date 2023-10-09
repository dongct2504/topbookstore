let dataTable;

$(document).ready(() => {
    dataTable = $('#tblDataBook').DataTable({
        ajax: {
            type: 'GET',
            url: '/Admin/Book/GetAllBooks'
        },
        columns: [
            { data: 'title', width: '15%' },
            { data: 'isbn13', width: '10%' },
            { data: 'price', width: '10%' },
            { data: 'discountPercent', width: '10%' },
            { data: 'numberOfPages', width: '10%' },
            {
                data: 'categories',
                render: (data) => {
                    let categories = '';
                    data.forEach(category => {
                        categories += category.name + ', ';
                    });
                    categories = categories.slice(0, -2);
                    return categories;
                },
                width: '10%'
            },
            { data: 'author.fullName', width: '10%' },
            { data: 'publisher.name', width: '10%' },
            {
                data: 'bookId',
                render: (data) => {
                    return `
                        <div class="text-center">
                            <a class="btn btn-success text-white"
                                href="/Admin/Book/Upsert/${data}">
                                <span class="fas fa-edit"></span>&nbsp;Sửa
                            </a>
                            <a class="btn btn-danger text-white"
                                onclick=Delete("/Admin/Book/DeleteBook/${data}")>
                                <span class="fas fa-trash-alt"></span>&nbsp;Xóa
                            </a>
                        </div>
                        `;
                },
                width: '15%'
            }
        ]
    });
});