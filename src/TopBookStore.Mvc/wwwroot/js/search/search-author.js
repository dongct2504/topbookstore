$(() => {
    $('#authorName').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/admin/author/searchAuthors",
                data: {
                    "term": request.term
                },
                success: function (data) {
                    response(data);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#authorId').val(ui.item.id);
        }
    });
});