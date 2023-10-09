let dataTable;

$(document).ready(() => {
    dataTable = $('#tblDataBook').DataTable({
        ajax: {
            type: 'GET',
            url: '/Admin/Book/GetAllBooks'
        },
        columns: [
            { data: 'title', width: '9%' },
            { data: 'description', width: '9%' },
            { data: 'isbn13', width: '9%' },
            { data: 'price', width: '9%' },
            // { data: 'discountPercent', width: '9%' },
            // { data: 'numberOfPages', width: '9%' },
            // { data: 'publisher.name', width: '9%' },
            // { data: 'publicationDate', width: '9%' },
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
                }, width: '20%'
            }
        ]
    });
});