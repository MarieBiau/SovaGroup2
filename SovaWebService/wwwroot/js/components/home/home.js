define(['knockout'], function (ko) {
    return function (params) {
        var title = ko.observable("Component Home");
        var word1 = "";
        var word2 = "sql";
        var wordbtn = ko.observableArray([]);
        var tagobj = ko.observable();
        var word = ko.observable("test");

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
        var showtags = function() {

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
        
        
        //TODO
        //give cloud words links to posts search
        //call controller for newest posts




        return {
            title,
            words,
            showtags,
            word,
            submitword,
            //tagobj,
            changeWords
        };
    }
});