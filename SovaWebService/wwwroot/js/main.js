require.config({
    baseUrl: "js",
    paths: {
        jquery: "../lib/jquery/dist/jquery.min",
        knockout: "../lib/knockout/dist/knockout",
        text: "../lib/text/text",
        jqcloud: '../lib/jqcloud2/dist/jqcloud.min',
        bootstrap: '../lib/bootstrap/dist/js/bootstrap.min',
        d3: "https://d3js.org/d3.v3.min"
    },
    shim: {
        jqcloud: {
            deps: ['jquery']
        },
        bootstrap: {
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

require(['knockout','bootstrap'], function (ko) {

    var vm = (function () {
        var links = [
            { name: 'Home', comp: 'home' },
            { name: 'History', comp: 'history' },
            { name: 'Personal', comp: 'personal' }
        ];
        var currentComp = ko.observable('home');
        var currentParams = ko.observable({});
        var searchText = ko.observable("");

        var submitSearch = function () {
            currentParams({ searchText: searchText() });
            currentComp("search");
        }

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
            currentParams,
            change,
            isActive,
            submitSearch,
            searchText

        };
    })();

    ko.applyBindings(vm);
});