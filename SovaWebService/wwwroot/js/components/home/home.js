define(['knockout'], function (ko) {
    return function (params) {
        var title = ko.observable("Component Home");
        var word1 = "bootstrap";
        var word2 = "sql";

        var words = ko.observableArray([]);

        $.getJSON("api/BestMatchList/" + word1, data => {

            for (var i = 0; i < data.length; i++) {


                var wordobj = { text: data[i].lemma, weight: data[i].weight };
                words.push(wordobj);

                console.log("JSON Data: " + data[i].lemma + " " + data[i].weight);
            }
        });
       

        var changeWords = function () {
            $.getJSON("api/BestMatchList/" + word2, data => {
                
                var obj = [];

                for (var i = 0; i < data.length; i++) {


                    var wordobj = { text: data[i].lemma, weight: data[i].weight };
                    obj.push(wordobj);


                    console.log("JSON Data: " + data[i].lemma + " " + data[i].weight);
                }
                words(obj);

            });

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
<<<<<<< HEAD
            changeWords
=======
            chageWords,
            posts,
            currentView,
            showPost,
            currentPost,
            home
>>>>>>> 741e3225b55e13314459f17a7e91b8d1e85ba9a9
        };
    }
});