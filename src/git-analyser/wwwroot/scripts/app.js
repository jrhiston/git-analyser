var d3 = require('d3');
var print = require("./print");
import barChart from "./single-item-bar-chart"
import printStackedBarChart from "./stacked-bar-chart"
import { printTable } from "./raw-data"
import { pieChart } from './pie-chart'

(function () {
    var entitySelector = 'entity';
    var numOfAuthorsSelector = 'n-authors';
    var numRevisionsSelector = 'n-revs';

    function initialise(callback) {
        $.post(
            "/Home/Analyse",
            {
                gitUrl: "https://github.com/jrhiston/git-analyser.git"
            },
            (data, status, xhr) => {
                console.log(data);

                if (!data) {
                    return;
                }

                printSummary(data[0]);
                printSection(data[1], "org-metrics", "n-revs", callback);
                printSection(data[2], "coupling", "coupled", callback);
                printSection(data[3], "age", "age-months", callback);
                printSection(data[4], "abs-churn", "added", callback);
                printSection(data[5], "author-churn", "added", callback);
                printSection(data[6], "entity-churn", "added", callback);
                printSection(data[7], "entity-ownership", "added", callback);
                printSection(data[8], "entity-effort", "author-revs", callback);
            });
    }

    const printSection = (data, prefix, colForPieChart, callback) => {

        var dsv = d3.dsvFormat(",");
        var orgMetrics = dsv.parse(data.result);

        var cols = orgMetrics.columns.map((col) => {
            return {
                head: col, 
                cl: col,
                html: d => d[col]
            }
        });

        console.log("Loaded data: ");
        console.log(orgMetrics);
        callback(orgMetrics, prefix, cols, colForPieChart);
    }

    const printSummary = (data) => {
        var dsv = d3.dsvFormat(",");
        var summary = dsv.parse(data.result);
        
        var cols = summary.columns.map((col) => {
            return {
                head: col, 
                cl: col,
                html: d => d[col]
            }
        });

        printTable(
            `.summary`, 
            cols, 
            summary);
    }

    function generateVisuals(data, prefix, cols, colForPieChart) {
        printStackedBarChart(
            `.${prefix}__stacked-bar-chart`, 
            () => data);

        pieChart(`.${prefix}__pieChart`, 
            () => data, 
            colForPieChart, 
            400, 
            25);

        printTable(
            `.${prefix}__number-of-revs-data`, 
            cols, 
            data.slice(0, 15));
    }

    initialise(generateVisuals);
})();