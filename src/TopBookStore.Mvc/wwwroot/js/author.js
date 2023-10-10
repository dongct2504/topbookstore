let dataTable;

$(document).ready(() => {
    dataTable = $('#tblDataAuthor').DataTable({
        ajax: {
            type: 'GET',
            url: '/admin/author/getAllAuthors'
        },
        columns: [
            { data: 'firstName' },
            { data: 'lastName' },
            { data: 'phoneNumber' },
            {
                data: 'books',
                render: (data) => {
                    let books = '';
                    data.forEach(book => {
                        books += book.title + ', ';
                    });
                    books = books.slice(0, -2);

                    return books;
                }
            },
            {
                data: 'authorId',
                render: (data) => {
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
                }
            }
        ]
    });
});

function Delete(url) {
    Swal.fire({
        title: 'Bạn có chắc muốn xóa?',
        text: 'Khi xóa sẽ xóa vĩnh viễn và sẽ không thể hồi phục.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Xóa',
        cancelButtonText: 'Hủy'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: (data) => {
                    if (data.success) { // if success is then then get the data
                        Swal.fire({
                            title: 'Đã xóa!',
                            text: data.message,
                            icon: 'success'
                        });
                        dataTable.ajax.reload();
                    } else {
                        Swal.fire({
                            title: 'Oops...',
                            text: data.message,
                            icon: 'error'
                        });
                    }
                }
            })
        }
    });
}