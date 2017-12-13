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

        var currentPost = ko.observable();

        var showPost = (data) => {
            $.getJSON(data.link, postData => {
                var post = {
                    title: postData.title,
                    score: postData.score,
                    creationDate: postData.creationDate,
                    body: postData.body

                }


                $.getJSON(postData.comments, cms => {

                    post.comments = ko.observableArray(cms);
                    console.log(post.comments);

                    $.getJSON(postData.answers, ans => {

                        post.answers = ko.observableArray(ans);
                        console.log(post.answers);
                        currentPost(post);
                    });

                });



            });
            title("Post");
            currentView('postview');
        };

        var home = () => {
            title("Show Posts");
            currentView('postlist');
        };

        $.getJSON("api/newestposts/", data => {
            posts(data.items);
            console.log(data.items);
        });


        return {
            title,
            words,
            chageWords,
            posts,
            currentView,
            showPost,
            currentPost,
            home
        };
    }
});