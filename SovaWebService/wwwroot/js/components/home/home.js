define(['knockout'], function (ko) {
    return function (params) {
        var title = ko.observable("Component Home");
        var word1 = "bootstrap";
        var word2 = "sql";
        var wordbtn = [];

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


        //TODO
        //change fake text to return most used tags...
        //make buttons (array) for change of name
        //expand changeword function to take more words 
        //make inout field, where user can write own words
        //give cloud words links to posts search
        //call controller for newest posts




        return {
            title,
            words,
            changeWords
        };
    }
});