define(['knockout'], function (ko) {
    return function (params) {
        var title = ko.observable("Component Personal");


        var posts = ko.observableArray([]);
        var currentView = ko.observable('postlist');

        $.getJSON("api/marks/", data => {
            posts(data.items);
            console.log(data.items);

            });

        return {
            title,
            posts,
            currentView
        };
    }
});