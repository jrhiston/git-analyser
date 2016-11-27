var d3 = require('d3')

export default function printStackedBarChart(selector, getData, dataSelectors) {
    const h = 400
    var data = getData()

    type(data, data.columns)

    var svg = d3.select(selector)
        .append("svg")
        .attr('height', h)

    var itemWidth = 40;

    var stack = d3.stack()
        .keys(data.columns.slice(1));

    var z = d3.scaleOrdinal()
        .range(["#98abc5", "#8a89a6"])
        .domain(data.columns.slice(1));

    function update() {
        var w = parseInt(d3.select(selector).style('width'))
        svg.attr('width', w)

        var dataPerPixel = Math.floor(w / itemWidth)
        var dataResampled = data.filter((d, i) => i < dataPerPixel)

        var series = stack(dataResampled)

        var dataSelection = svg.selectAll('.serie').data(series)

        //     g.selectAll(".serie")
        // .data(stack.keys(data.columns.slice(1))(data))
        // .enter().append("g")
        //   .attr("class", "serie")
        //   .attr("fill", function(d) { return z(d.key); })
        // .selectAll("rect")
        // .data(function(d) { return d; })
        // .enter().append("rect")
        //   .attr("x", function(d) { return x(d.data.State); })
        //   .attr("y", function(d) { return y(d[1]); })
        //   .attr("height", function(d) { return y(d[0]) - y(d[1]); })
        //   .attr("width", x.bandwidth());

        dataSelection
            .enter().append('g')
                .attr('class', 'serie')
                .attr('fill', d => z(d.key))
            .selectAll('rect')
            .data(d => d)
            .enter()
            .append('rect')
            .attr('x', (item, index) => index * itemWidth + 10)
            .attr('y', (d) => {
                return h - d[1]
            })
            .attr('height', d => d[1] - d[0])

        // dataSelection
        //     .enter()
        //     .append("rect")
        //     .attr("x", (item, index) => index * itemWidth + 10)
        //     .attr("y", (d) => h - d[dataItemSelector] * 4)
        //     .attr("height", (d) => d[dataItemSelector] * 4)
        //     .attr("class", "bar__rect")
        //.on("mouseover", handleMouseOver)
        //.on("mouseout", handleMouseOut)

        dataSelection.exit().remove()
    }

    function type(d, columns) {
        for (var t = 0; t < d.length; t++) {
            for (var i = 1; i < columns.length; ++i) {
                d[t][columns[i]] = +d[t][columns[i]];
            }
        }
    }

    // Create Event Handlers for mouse
    function handleMouseOver(d, i) {  // Add interactivity

        // Use D3 to select element, change color and size
        d3.select(this).attr("class", "bar__selected");

        // Specify where to put label of text
        svg.append("text")
            .attr("id", "t" + d[dataItemSelector] + "-" + i)
            .attr("x", i * (itemWidth) + 15)
            .attr("y", h - (d[dataItemSelector] * 4 + 10))
            .attr("class", "bar__text")
            .text(d[dataItemSelector])

        svg.append("text")
            .attr("id", "t2" + d[dataItemSelector] + "-" + i)
            .attr("x", 10)
            .attr("y", 20)
            .attr("class", "bar__text")
            .text(d["entity"])
    }

    function handleMouseOut(d, i) {
        // Use D3 to select element, change color back to normal
        d3.select(this).attr("class", "bar__rect");

        // Select text by id and then remove
        d3.select("#t" + d[dataItemSelector] + "-" + i).remove();  // Remove text location
        d3.select("#t2" + d[dataItemSelector] + "-" + i).remove();
    }

    d3.select(window).on("resize", update)

    update()
}