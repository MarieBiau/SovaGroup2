define(['knockout'], function (ko) {
    return function (params) {
        var title = ko.observable("Component Personal");


        var posts = ko.observableArray([]);
        var currentView = ko.observable('postlist');

        //annotation binding
        var annotation = ko.observable();
        var save = true;
        var marked = ko.observable(true);
        var bool = null;
        var saveAnnotation = ko.observable();


        var checkIfMarked = function() {
            if (marked()===true) {
                return true;
            } else {
                return false;
            }

        }
        bool=checkIfMarked();
        console.log(checkIfMarked());

        saveAnnotation = function () {
            if (save === true) {

                var annotation = this.annotation();

                console.log(annotation);
               
            }
        }


        console.log(annotation());

        $.getJSON("api/marks/", data => {
            posts(data.items);
            console.log(data.items);

            });


        return {
            title,
            posts,
            currentView,
            annotation,
            marked,
            checkIfMarked,
            bool,
            saveAnnotation
        };

    }
});