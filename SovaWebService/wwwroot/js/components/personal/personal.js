define(['knockout'], function (ko) {
    return function (params) {
        var title = ko.observable("Component Personal");


        var posts = ko.observableArray([]);
        var currentView = ko.observable('postlist');
        var currentPost = ko.observable();
        //annotation binding

        var annotation = ko.observable();
        var saveAnnotation = ko.observable();
        var addMark = ko.observable();
        var removeMark = ko.observable();

        addMark = (data) => {
    
            $.getJSON("api/marks/" + data.id + "/addMark");
            console.log(data.id);
            alert("addMark saved!");

             
        };

        removeMark = (data) => {
            $.getJSON("api/marks/" + data.id + "/removeMark");
            console.log(data.id);
            alert("removeMark saved!");

        };


        
        saveAnnotation = (data) => {

            var json = '[{ "op": "replace", "path": "/annotation", "value": "' + data.annotationText() + '" }]';
            $.ajax({
                type: "PATCH",
                contentType: "application/json",
                url: "api/marks/" + data.id + "/updateAnnotation",
                data: json,
                success: success
            });


        };

        function success() {
            alert("Annotation saved!");
            //do some stuff
        }
        
        var showPost = (data) => {
            $.getJSON(data.link, postData => {
                var post = {
                    title: postData.title,
                    score: postData.score,
                    creationDate: postData.creation_date,
                    body: postData.body,
                    id: postData.id

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
         

        };

        var home = () => {
            title("Show posts");
            currentView('postlist');
        };

        $.getJSON("api/marks/", data => {
            var p = data.items.map(function (e) {
                e.annotationText = ko.observable("");
                return e;
            });
            posts(p);
            console.log(p);

        });

        
        return {
            currentPost,
            home,
            showPost,
            title,
            posts,
            currentView,
            annotation,
            saveAnnotation,
            addMark,
            removeMark
            

        };

    }
});