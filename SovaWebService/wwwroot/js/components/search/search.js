define(['jquery', 'knockout'], function ($,ko) {
    return function (params) {

        //create logic to observe input field and search button click


        var title = ko.observable("Show Posts");
        var posts = ko.observableArray([]);
        var nextLink = ko.observable();
        var prevLink = ko.observable();

        var currentView = ko.observable('postlist');


        var next = () => {
            $.getJSON(nextLink(), data => {
                posts(data.items);
                nextLink(data.next);
                prevLink(data.prev);
            });
        };
        var canNext = ko.computed(() => {
            return nextLink() !== null;
        });

        var prev = () => {
            $.getJSON(prevLink(), data => {
                posts(data.items);
                nextLink(data.next);
                prevLink(data.prev);
            });
        };

        var canPrev = ko.computed(() => {
            return prevLink() !== null;
        });

        var currentPost = ko.observable();

        var showPost = (data) => {
            $.getJSON(data.link, postData => {
                var post = {
                    title: postData.title,
                    score: postData.score,
                    creationDate: postData.creationDate,
                    body: postData.body
                }

                $.getJSON(postData.answers, ans => {
                    post.answers = ko.observableArray(ans);
                    currentPost(post);
                });
            });
            title("Post");
            currentView('postview');
        };

        var home = () => {
            title("Show Posts");
            currentView('postlist');
        };

        $.getJSON("api/questions/", data => {
            posts(data.items);
            nextLink(data.next);
            prevLink(data.prev);
            console.log(data.items);
        });


        return {
            title,
            posts,
            next,
            canNext,
            prev,
            canPrev,
            currentView,
            showPost,
            currentPost,
            home
        };
        

    }
});