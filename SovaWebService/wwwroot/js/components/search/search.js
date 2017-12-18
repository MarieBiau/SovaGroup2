define(['jquery', 'knockout'], function ($,ko) {
    return function (params) {

        var title = ko.observable("Show posts");
        var posts = ko.observableArray([]);
        var answers = ko.observableArray([]);
        var currentPost = ko.observable();
        var nextLink = ko.observable();
        var prevLink = ko.observable();
        var searchText = ko.observable(params.searchText || '');
        
       
        var currentView = ko.observable('postlist');
        var linkedPostsView = ko.observable('linkedPosts');

        var addMark = ko.observable();
        var removeMark = ko.observable();




        addMark = (data) => {


            $.getJSON(data.link,
                postData => {
                    var post = {
                        title: postData.title,
                        score: postData.score,
                        creationDate: postData.creation_date,
                        body: postData.body

                    }
                    
                    $.getJSON(postData.addMark);
                    alert("Reload Page addMark saved!");

                });
        };

        removeMark = (data) => {
            $.getJSON(data.link,
                postData => {
                    var post = {
                        title: postData.title,
                        score: postData.score,
                        creationDate: postData.creation_date,
                        body: postData.body

                    }

                    $.getJSON(postData.addMark);
                    alert("Reload Page removeMark saved!");
                });

        };



        
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


        var showPost = (data) => {
            $.getJSON(data.link, postData => {
                var post = {
                    title: postData.title,
                    score: postData.score,
                    creationDate: postData.creation_date,
                    body: postData.body
                    
                }
                
                $.getJSON(postData.comments, cms => {

                    post.comments = ko.observableArray(cms);

                    $.getJSON(postData.answers, ans => {

                        $.getJSON(postData.linkedPosts, linkPosts => {

                            post.linkedPosts = ko.observableArray(linkPosts);
                        });
                        $.getJSON(postData.showTags, stags => {

                            post.showTags = ko.observableArray(stags);
                            currentPost(post);

                        });

                        ans.forEach(e => {
                            $.getJSON(e.comments, comments => {
                                e.comments = comments;
                            });
                        });


                        post.answers = ko.observableArray(ans);

                    });

                });

            });
            title("Post");
            currentView('postview');
            linkedPostsView('linkedPosts');

        };

        

        $.getJSON("api/bestmatch/", dataAnswers => {

            answers(data.items);

        });

        var home = () => {
            title("Show posts");
            currentView('postlist');
        };

        $.getJSON("api/bestmatch/search/" + searchText(), data => {
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
            home,
            searchText,
            addMark,
            removeMark
         

        };
        

    }
});