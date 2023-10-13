$(() => {
    $('#publisherName').autocomplete({
        source: (request, response) => {
            $.ajax({
                url: "/admin/publisher/searchPublishers",
                dataType: "json",
                data: {
                    "term": request.term
                },
                success: function (data) {
                    response(data);
                }
            });
        },
        minLength: 1,
        select: (event, ui) => {
            $('#publisherId').val(ui.item.id);
        }
    });
});