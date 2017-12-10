require.config({
    baseUrl: "js",
    paths: {
        jquery: "../lib/jquery/dist/jquery.min",
        knockout: "../lib/knockout/dist/knockout",
        text: "../lib/text/text",    
        jqcloud: '../lib/jqcloud2/dist/jqcloud.min'
    },
    shim: {
        jqcloud: {
            deps: ['jquery']
        }
    }
});

require(['knockout', 'jquery', 'jqcloud'], function (ko, $) {
    ko.bindingHandlers.cloud = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called when the binding is first applied to an element
            // Set up any initial state, event handlers, etc. here
            var words = allBindings.get('cloud').words;
            if (words && ko.isObservable(words)) {
                words.subscribe(function () {
                    $(element).jQCloud('update', ko.unwrap(words));
                });
            }
        },
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called once when the binding is first applied to an element,
            // and again whenever any observables/computeds that are accessed change
            // Update the DOM element based on the supplied values here.
            var words = ko.unwrap(allBindings.get('cloud').words) || [];
            $(element).jQCloud(words);
        }
    };
});


require(['knockout'], function (ko) {
    ko.components.register("history",
        {
            viewModel: { require: "components/history/history" },
            template: { require: "text!components/history/history_view.html" }
        });
    ko.components.register("home",
        {
            viewModel: { require: "components/home/home" },
            template: { require: "text!components/home/home_view.html" }
        });
    ko.components.register("personal",
        {
            viewModel: { require: "components/personal/personal" },
            template: { require: "text!components/personal/personal_view.html" }
        });
    ko.components.register("search",
        {
            viewModel: { require: "components/search/search" },
            template: { require: "text!components/search/search_view.html" }
        });
});

require(['knockout'], function (ko) {

    var vm = (function () {
        var links = [
            { name: 'Home', comp: 'home' },
            { name: 'History', comp: 'history' },
            { name: 'Personal', comp: 'personal' },
            { name: 'Search', comp: 'search' }
        ];
        var currentComp = ko.observable('home');

        var isActive = function (menu) {
            if (menu.comp === currentComp()) {
                return 'active';
            }
            return '';
        }

        var change = function (menu) {
            currentComp(menu.comp);
        }

        return {
            links,
            currentComp,
            change,
            isActive
        };
    })();

    ko.applyBindings(vm);
});




//require(['jquery', 'knockout'], ($, ko) => {

//    var vm = (function () {
//        var title = ko.observable("Show Posts");
//        var posts = ko.observableArray([]);
//        var nextLink = ko.observable();
//        var prevLink = ko.observable();

//        var currentView = ko.observable('postlist');


//        var next = () => {
//            $.getJSON(nextLink(), data => {
//                posts(data.items);
//                nextLink(data.next);
//                prevLink(data.prev);
//            });
//        };
//        var canNext = ko.computed(() => {
//            return nextLink() !== null;
//        });

//        var prev = () => {
//            $.getJSON(prevLink(), data => {
//                posts(data.items);
//                nextLink(data.next);
//                prevLink(data.prev);
//            });
//        };

//        var canPrev = ko.computed(() => {
//            return prevLink() !== null;
//        });

//        var currentPost = ko.observable();

//        var showPost = (data) => {
//            $.getJSON(data.link, postData => {
//                var post = {
//                    title: postData.title,
//                    score: postData.score,
//                    creationDate: postData.creationDate,
//                    body: postData.body
//                }

//                $.getJSON(postData.answers, ans => {
//                    post.answers = ko.observableArray(ans);
//                    currentPost(post);
//                });
//            });
//            title("Post");
//            currentView('postview');
//        };

//        var home = () => {
//            title("Show Posts");
//            currentView('postlist');
//        };

//        $.getJSON("api/questions/", data => {
//            posts(data.items);
//            nextLink(data.next);
//            prevLink(data.prev);
//            console.log(data.items);
//        });


//        return {
//            title,
//            posts,
//            next,
//            canNext,
//            prev,
//            canPrev,
//            currentView,
//            showPost,
//            currentPost,
//            home
//        };
//    })();

//    ko.applyBindings(vm);
//});

