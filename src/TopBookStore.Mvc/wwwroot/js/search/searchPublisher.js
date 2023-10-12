$(() => {
    $('#publisherName').autocomplete({
        source: function(request, response) {
            $.ajax({
                url: "/admin/publisher/searchPublishers",
                dataType: "json",
                data: {
                    "term": request.term
                },
                success: function(data) {
                    response(data);
                }
            });
        },
        minLength: 1,
        select: function(event, ui) {
            $('#publisherId').val(ui.item.id);
        }
    });
});