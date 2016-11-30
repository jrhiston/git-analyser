var d3 = require('d3')

export default function printStackedBarChart(selector, getData, dataSelectors) {
    const h = 400
    var data = getData()

    //type(data, data.columns)

    var svg = d3.select(selector)
        .append("svg")
        .attr('height', h)

    var itemWidth = 15;

    var stack = d3.stack()
        .keys(data.columns.slice(1));

    var z = d3.scaleOrdinal()
        .range(["#ff7733", "#019acb"])
        .domain(data.columns.slice(1));

    function update() {
        var w = parseInt(d3.select(selector).style('width'))
        svg.attr('width', w)

        var dataPerPixel = Math.floor(w / itemWidth)

        var dataResampled = data.filter((d, i) => i < dataPerPixel)

        var seriesData = stack(dataResampled)

        var series = svg.selectAll('.serie').data(seriesData)

        var newSeries = series
            .enter()
            .append('g')
            .attr('class', 'serie')
            .attr('fill', d => z(d.key))
        
        updateRectangles(newSeries
            .selectAll('rect')
            .data(d => d))

        var rects = series
            .selectAll('rect')
            .data(d => d)
        
        updateRectangles(rects)

        series.exit().remove()

        rects.exit().remove()
    }

    function updateRectangles(rectSelector){
        rectSelector.enter()
            .append('rect')
            .attr('x', (item, index) => index * itemWidth + 10)
            .attr('y', d => h - (d[1] * 4))
            .attr('height', d => (d[1] - d[0]) * 4)
            .attr("class", "bar__stacked")
            .on("mouseover", handleMouseOver)
            .on("mouseout", handleMouseOut)
    }

    // Create Event Handlers for mouse
    function handleMouseOver(d, i) {  // Add interactivity
        // Use D3 to select element, change color and size
        //d3.select(this).attr("class", "bar__selected")

        svg.append("text")
            .attr("id", "t2" + d.key + "-" + i)
            .attr("x", 10)
            .attr("y", 20)
            .attr("class", "bar__text")
            .text(JSON.stringify(d.data))
    }

    function handleMouseOut(d, i) {
        // Use D3 to select element, change color back to normal
        d3.select(this).attr("class", "bar__stacked")

        // Select text by id and then remove
        d3.select("#t" + d.key + "-" + i).remove()  // Remove text location
        d3.select("#t2" + d.key + "-" + i).remove()
    }

    d3.select(window).on("resize", update)

    update()
}