define(['knockout', 'd3'], function (ko, d3) {
    return function(params) {
        var title = ko.observable("Component Home");
        //var word1 = "";
        //var word2 = "sql";
        //var wordbtn = ko.observableArray([]);
        //var tagobj = ko.observable();
        var word = ko.observable("test");
        var graph = ko.observable({});
        var graphword = ko.observable();


        //association graph
        var makeAssociation = function()
        {
            $.getJSON("api/graph/"+graphword(),
                data => {
                    graph = ko.observable({}); 

                    graph(JSON.parse(data));


                    var width = 960,
                        height = 500;

                    var svg = d3.select("body").append("svg")
                        .attr("width", width)
                        .attr("height", height);


                    var force = d3.layout.force()
                        .gravity(0.05)
                        .distance(100)
                        .charge(-200)
                        .size([width, height]);

                    //var color = d3.scaleOrdinal(d3.schemeCategory20);
                    var color = d3.scale.category20c();

                    //d3.json("graph.json", function(error, json) {
                    (function() {
                        //if (error) throw error;

                        force
                            .nodes(graph().nodes)
                            .links(graph().links)
                            .start();

                        var link = svg.selectAll(".link")
                            .data(graph().links)
                            .enter().append("line")
                            .attr("class", "link")
                            .attr("stroke-width", function(d) { return Math.sqrt(d.value); });

                        var node = svg.selectAll(".node")
                            .data(graph().nodes)
                            .enter().append("g")
                            .attr("class", "node")
                            .call(force.drag);
                        node.append("circle")
                            .attr("r", function(d) { return 5; })
                            .style("fill", "red");

                        node.append("text")
                            .attr("dx", function(d) { return -(d.name.length * 3) })
                            .attr("dy", ".65em")
                            .text(function(d) { return d.name });

                        force.on("tick",
                            function() {
                                link.attr("x1", function(d) { return d.source.x; })
                                    .attr("y1", function(d) { return d.source.y; })
                                    .attr("x2", function(d) { return d.target.x; })
                                    .attr("y2", function(d) { return d.target.y; });

                                node.attr("transform", function(d) { return "translate(" + d.x + "," + d.y + ")"; });
                            });
                    })();
                });
        }
        

        //tags for wordcloud
        var words = ko.observableArray([]);
        $.getJSON("api/tags/10",
            data => {

                for (var i = 0; i < data.length; i++) {

                    var wordobj = { text: data[i].name, weight: data[i].occurrences };

                    words.push(wordobj);

                    //console.log("JSON Data: " + data[i].name + " " + data[i].occurences);
                }
                //console.log(word());

            });

        console.log(words());

        //to make word clouds with tags when pressing a button
        var showtags = function() {

            $.getJSON("api/tags/10",
                data => {
                    var obj = [];
                    for (var i = 0; i < data.length; i++) {

                        var wordobj = { text: data[i].name, weight: data[i].occurrences };

                        obj.push(wordobj);

                        //console.log("JSON Data: " + data[i].name + " " + data[i].occurences);
                    }
                    words(obj);
                });
        }


        var changeWords = function() {
            //words = ko.observableArray([]);
            $.getJSON("api/BestMatchList/" + word(),
                data => {

                    var obj = [];

                    for (var i = 0; i < data.length; i++) {

                        var wordobj = { text: data[i].lemma, weight: data[i].weight };
                        obj.push(wordobj);

                        //console.log("JSON Data: " + data[i].lemma + " " + data[i].weight);
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
            graph,
            graphword,
            makeAssociation,
            changeWords
        };
    }
});