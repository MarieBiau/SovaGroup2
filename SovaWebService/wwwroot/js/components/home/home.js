define(['knockout'], function (ko) {
    return function (params) {
        var title = ko.observable("Component Home");

        var words = ko.observableArray([
            { text: "Lorem", weight: 13 },
            { text: "Ipsum", weight: 10.5 },
            { text: "Dolor", weight: 9.4 },
            { text: "Sit", weight: 8 },
            { text: "Amet", weight: 6.2 },
            { text: "Consectetur", weight: 5 },
            { text: "Adipiscing", weight: 5 },
            /* ... */
        ]);

        var chageWords = function () {
            words([
                { text: "Joe", weight: 13 },
                { text: "Peter", weight: 10.5 },
                { text: "Dolor", weight: 9.4 },
                { text: "Sit", weight: 8 },
                { text: "Amet", weight: 6.2 },
                { text: "Consectetur", weight: 5 },
                { text: "Adipiscing", weight: 5 },
                /* ... */
            ]);
        }

        var posts = ko.observableArray([]);
        var currentView = ko.observable('postlist');

        $.getJSON("api/newestposts/", data => {
            posts(data.items);
            console.log(data.items);
        });


        return {
            title,
            words,
            chageWords,
            posts,
            currentView
        };
    }
});