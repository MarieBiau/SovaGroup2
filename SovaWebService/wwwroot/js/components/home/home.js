define(['knockout'], function (ko) {
    return function (params) {
        var title = ko.observable("Component Home");

        var word = ko.observable();

        var submitword = function () {
            word = word();
            changeWords();
        }

        

        //tags for wordcloud
        var words = ko.observableArray([]);
        $.getJSON("api/tags/10", data => {

            for (var i = 0; i < data.length; i++) {

                var wordobj = { text: data[i].name, weight: data[i].occurrences };

                words.push(wordobj);

                console.log("JSON Data: " + data[i].name + " " + data[i].occurences);
            }
            console.log(word());

        });

        //to make word clouds with tags when pressing a button
        var showtags = function () {

            $.getJSON("api/tags/10", data => {
                var obj = [];
                for (var i = 0; i < data.length; i++) {

                    var wordobj = { text: data[i].name, weight: data[i].occurrences };

                    obj.push(wordobj);

                    console.log("JSON Data: " + data[i].name + " " + data[i].occurences);
                }
                words(obj);
            });
        }



        var changeWords = function () {
            //words = ko.observableArray([]);
            $.getJSON("api/BestMatchList/" + word, data => {

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
            title("Show posts");
            currentView('postlist');
        };

        $.getJSON("api/newestposts/", data => {
            posts(data.items);
            console.log(data.items);
        });


        return {
            title,
            words,
            showtags,
            word,
            submitword,
            changeWords,
            posts,
            currentView,
            showPost,
            currentPost,
            home
        };


    }
});