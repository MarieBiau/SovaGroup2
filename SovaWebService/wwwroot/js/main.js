
require.config({

    baseUrl: "js",

    paths: {
        jquery: '../lib/jQuery/dist/jquery.min',
        knockout: '../lib/knockout/dist/knockout',
        text: '../lib/text/text',
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
    //home view/page
    ko.components.register("home",
        {
            viewModel: { require: "components/home/homeComp" },
            template: { require: "text!components/home/homecomp_view.html" }
        });
    //history view
    ko.components.register("favorites",
        {
            viewModel: { require: "components/favorites/favoriteComp" },
            template: { require: "text!components/favorites/favoritecomp_view.html" }
        });
    //favorites view
    ko.components.register("history",
        {
            viewModel: { require: "components/history/historyComp" },
            template: { require: "text!components/history/historycomp_view.html" }
        });
});


require(['jquery', 'knockout'], ($, ko) => {

    var vm = (function () {
        var title = ko.observable("Show Posts");
        var posts = ko.observableArray([]);
        var nextLink = ko.observable();
        var prevLink = ko.observable();

        var currentView = ko.observable('postlist');
        var curentView = ko.observable('postlist');
        var switchComponent = function () {
            if (curentView() === "postlist") {
                curentView("home");
                console.log("I have changed");
            } else {
                curentView("postlist");
            }

        }

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

        var currentPost = ko.observable();

        var showPost = (data) => {
            $.getJSON(data.link, postData => {
                var post = {
                    //title: postData.body,
                    score: postData.score,
                    creationDate: postData.creationDate,
                    body: postData.body
                }

                $.getJSON(postData.answers, ans => {
                    post.answers = ko.observableArray(ans);
                    currentPost(post);
                });
            });
            title("Post");
            currentView('postview');
        };

        var home = () => {
            title("Show Posts");
            currentView('postlist');
        };

        $.getJSON("api/questions", data => {
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
            curentView,
            showPost,
            currentPost,
            switchComponent,
            home
        };
    })();

    ko.applyBindings(vm);
});

