var d3 = require('d3')

export default function printBarChart(selector, getData, dataItemSelector, numToShow) {
    const h = 400
    var data = getData()

    var svg = d3.select(selector)
        .append("svg")
        .attr('height', h)
    
    var itemWidth = 40;

    function update() {
        var w = parseInt(d3.select(selector).style('width'))
        svg.attr('width', w)

        var dataPerPixel = Math.floor(w / itemWidth)
        var dataResampled = data.filter((d, i) => i < dataPerPixel)

        var dataSelection = svg.selectAll('rect')
            .data(dataResampled)

        dataSelection
            .enter()
            .append("rect")
            .attr("x", (item, index) => index * itemWidth + 10)
            .attr("y", (d) => h - (d[dataItemSelector] * 4))
            .attr("height", (d) => d[dataItemSelector] * 4)
            .attr("class", "bar__rect")
            .on("mouseover", handleMouseOver)
            .on("mouseout", handleMouseOut)

        dataSelection.exit().remove()
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