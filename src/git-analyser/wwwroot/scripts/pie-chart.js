var d3 = require('d3');

export function pieChart(
    selector,
    getData,
    dataItemSelector,
    height,
    numberItemsToShow) {
    var width = parseInt(d3.select(selector).style('width')),
        radius = Math.min(width, height) / 2;

    var color = d3.scaleOrdinal()
        .range(["#2a97bb", "#cc66ff", "#a64dff", "#99ddff"]);

    var arc = d3.arc()
        .outerRadius(radius - 10)
        .innerRadius(0);

    var pie = d3.pie()
        .sort(null)
        .value(d => d[dataItemSelector]);

    var svg = d3.select(selector).append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

    var g = svg.selectAll(".arc")
        .data(pie(getData().slice(0, numberItemsToShow)))
        .enter()
        .append("g")
        .attr("class", "arc")
        .on("mouseover", handleMouseOver)
        .on("mouseout", handleMouseOut);

    g.append("path")
        .attr("d", arc)
        .style("fill", d => color(d.data['entity']))
        .style("stroke", "#ccc")
        .style("stroke-width", 2);

    function type(d) {
        d[dataItemSelector] = +d[dataItemSelector];
        return d;
    }
    function handleMouseOver(d, i) { 

        svg.append("text")
            .attr("id", "t2" + d.key + "-" + i)
            .attr("x", -(width/2))
            .attr("y", -(height/2) + 20)
            .attr("class", "bar__text")
            .text(d.data.entity.slice(d.data.entity.lastIndexOf("/") + 1) 
                    + ", " 
                    + d.data[dataItemSelector])
    }

    function handleMouseOut(d, i) {
        d3.select("#t2" + d.key + "-" + i).remove()
    }
}