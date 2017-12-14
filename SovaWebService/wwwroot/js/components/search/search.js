define(['jquery', 'knockout'], function ($,ko) {
    return function (params) {

        //create logic to observe input field and search button click


        var title = ko.observable("Show Posts");
        var posts = ko.observableArray([]);
        var nextLink = ko.observable();
        var prevLink = ko.observable();
        var searchField = ko.observable();
       
        var currentView = ko.observable('postlist');
        var linkedPostsView = ko.observable('linkedPosts');


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
                

                $.getJSON(postData.comments, cms => {

                    post.comments = ko.observableArray(cms);
                    console.log(post.comments);
                    //currentPost(post);

                    $.getJSON(postData.answers, ans => {

                        post.answers = ko.observableArray(ans);
                        console.log(post.answers);
                        //currentPost(post);

                        $.getJSON(postData.linkedPosts, linkPosts => {

                            post.linkedPosts = ko.observableArray(linkPosts);
                            console.log(post.linkedPosts);
                            //currentPost(post);

                            $.getJSON(postData.showTags, stags => {

                                post.showTags = ko.observableArray(stags);
                                console.log(post.showTags);
                                currentPost(post);
                            });

                        });


                    });

                });

               

            });
            title("Post");
            currentView('postview');
            linkedPostsView('linkedPosts');

        };

        var home = () => {
            title("Show Posts");
            currentView('postlist');
        };

        $.getJSON("api/bestmatch/search/NET", data => {
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