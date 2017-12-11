define(['knockout'], function (ko) {
    return function (params) {
        var title = ko.observable("Component History");


        var posts = ko.observableArray([]);
        var visitedPosts = ko.observableArray([]);
        var currentView = ko.observable('postlist');
        var visitedPostsView = ko.observable('visitedpostlist');

        $.getJSON("api/searches/", data => {
            posts(data.items);
            console.log(data.items);
        });

        var millisecondsToWait = 500;
        setTimeout(function () {
            // Whatever you want to do after the wait
            $.getJSON("api/visited/", data => {
                visitedPosts(data.items);
                console.log(data.items);
            });
        }, millisecondsToWait);


        return {
            title,
            posts,
            currentView,
            visitedPosts,
            visitedPostsView
        };
    }
});