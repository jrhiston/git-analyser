var d3 = require("d3")

// Example column data:
//var columns = [
//    {
//        head: 'Movie title', cl: 'title',
//        html: function (row) { return r.title; }
//    },
//    ...
//];

export function printTable(domElement, columns, rows) {
    var table = d3
        .select(domElement)
        .append('table')
        .attr('class', 'table table-condensed table-striped');

    table.append('thead')
        .append('tr')
        .selectAll('th')
        .data(columns)
        .enter()
        .append('th')
        .attr('class', d => d.cl)
        .text(d => d.head);

    table.append('tbody')
        .selectAll('tr')
        .data(rows)
        .enter()
        .append('tr')
        .selectAll('td')
        .data(function (row, i) {
            // evaluate column objects against the current row
            return columns.map(function (c) {
                var cell = {};
                d3.keys(c).forEach(function (k) {
                    cell[k] = typeof c[k] == 'function' ? c[k](row, i) : c[k];
                });
                return cell;
            });
        }).enter()
        .append('td')
        .html(d => d.html)
        .attr('class', d => d.cl);
}

function getString(d) {
    return d["entity"]
        + ','
        + d["n-authors"]
        + ','
        + d["n-revs"];
}