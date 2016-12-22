var d3 = require('d3');
var print = require("./print");
import barChart from "./single-item-bar-chart"
import printStackedBarChart from "./stacked-bar-chart"
import { printTable } from "./raw-data"
import { pieChart } from './pie-chart'

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
        $.post(
            "/Home/Analyse",
            {
                gitUrl: "https://github.com/jrhiston/git-analyser.git"
            },
            (data, status, xhr) => {
                console.log(data);

                var dsv = d3.dsvFormat(",");

                var data = dsv.parse(data[6].result);

                if (!data) {
                    return;
                }

                var getData = setData(data);
                console.log("Loaded data: ");
                console.log(data);
                callback(getData);
            });

        //d3.csv("../data/org-metrics.csv", function (data) {
        //    if (!data) {
        //        return;
        //    }

        //    var getData = setData(data);
        //    console.log("Loaded data: ");
        //    console.log(data);
        //    callback(getData);
        //});
    }

    function generateVisuals(getData) {
        print.bubbleChart(bubbleChartSelector, getData, numRevisionsSelector);
        pieChart(pieChartSelector, getData, numRevisionsSelector, 400, 25);

        //print.printStraightLineCircles(canvasSelector, getData, numRevisionsSelector);
        // barChart('.number-of-revs', getData, numRevisionsSelector, 15);
        // barChart('.number-of-authors', getData, numOfAuthorsSelector, 15);

        printStackedBarChart('.stacked-bar-chart', getData, [numRevisionsSelector,numOfAuthorsSelector]);
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