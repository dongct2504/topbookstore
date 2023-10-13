let dataTable;

$(document).ready(() => {
    dataTable = $('#tblDataBook').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/admin/book/getAllBooks"
        },
        "columns": [
            { "data": "title", "width": "20%" },
            { "data": "price", "width": "10%" },
            { "data": "numberOfPages", "width": "5%" },
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
                "width": "30%"
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

function Delete(url) {
    Swal.fire({
        title: "Bạn có chắc muốn xóa?",
        text: "Khi xóa sẽ xóa vĩnh viễn và sẽ không thể hồi phục.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Xóa",
        cancelButtonText: "Hủy"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: (data) => {
                    if (data.success) { // if success is then then get the data
                        Swal.fire({
                            title: "Đã xóa!",
                            text: data.message,
                            icon: "success"
                        });
                        dataTable.ajax.reload();
                    } else {
                        Swal.fire({
                            title: "Ây da!!!",
                            text: data.message,
                            icon: "error"
                        });
                    }
                }
            })
        }
    });
}