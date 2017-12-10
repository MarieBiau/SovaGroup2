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


        //TODO
        //change fake text to return most used tags...
        //call controller for newews posts




        return {
            title,
            words,
            chageWords
        };
    }
});