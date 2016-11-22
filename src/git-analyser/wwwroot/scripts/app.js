var d3 = require('d3');
var print = require("./print");
import barChart from "./bar-chart"
import { printTable } from "./raw-data"

(function () {
    var bubbleChartSelector = '.bubbleChart';
    var pieChartSelector = '.pieChart';
    var entitySelector = 'entity';
    var numOfAuthorsSelector = 'n-authors';
    var numRevisionsSelector = 'n-revs';

    function setData(data) {
        return function getData() {
            return data;
        };
    }

    function initialise(callback) {
        d3.csv("../data/org-metrics.csv", function (data) {
            if (!data) {
                return;
            }

            var getData = setData(data);
            console.log("Loaded data: ");
            console.log(data);
            callback(getData);
        });
    }

    function generateVisuals(getData) {
        print.bubbleChart(bubbleChartSelector, getData, numRevisionsSelector);
        print.pieChart(pieChartSelector, getData, numRevisionsSelector);

        //print.printStraightLineCircles(canvasSelector, getData, numRevisionsSelector);
        barChart('.number-of-revs', getData, numRevisionsSelector, 15);
        // print.printRawData(".number-of-revs-data", getData)

        var rawData = getData().slice(0, 15);

        var cols = [
            {
                head: "entity", 
                cl: "title",
                html: d => d.entity
            },
            {
                head: "n-authors",
                cl: "n-authors",
                html: d => d["n-authors"]
            },
            {
                head: "n-revs",
                cl: "n-revs",
                html: d => d["n-revs"]
            }
        ];

        printTable(".number-of-revs-data", cols, rawData);
    }

    initialise(generateVisuals);
})();