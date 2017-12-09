define(['knockout'], function (ko) {
    return function (params) {
        var title = ko.observable("Component 1");

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

        var lemmas = ["Joe", "Peter", "Dolor", "Sit", "Amet", "Consectetur", "Adipiscing"];
        var chageWords = function () {
            words([
                { text: lemmas[0], weight: 13 },
                { text: lemmas[1], weight: 10.5 },
                { text: lemmas[2], weight: 9.4 },
                { text: lemmas[3], weight: 8 },
                { text: lemmas[4], weight: 6.2 },
                { text: lemmas[5], weight: 5 },
                { text: lemmas[6], weight: 5 },
                /* ... */
            ]);
        }


        return {
            title,
            words,
            chageWords
        };
    }
});