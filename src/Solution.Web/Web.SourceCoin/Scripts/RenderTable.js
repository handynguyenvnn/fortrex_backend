function jqueryRenderTable(dataName) {
    self = this;

    this.name = "data";
    if (typeof (dataName) != 'undefined') {
        this.name = dataName;
    }

    this.tableRender = $('<div class="google-visualization-table" style="position: relative; max-width: 100%; max-height: 100%; width: 100%;">'
        + '<div style="position: relative; max-width: 100%; max-height: 100%; width: 100%;">'
        + '<table cellspacing="0" class="table table-over table-striped" style="width: 100%;"></table>'
        + '</div></div>');
    this.theaderRender = $('<thead></thead>');
    this.tbodyRender = $('<tbody></tbody>');

    this.trHeaderRender = $('<tr class="google-visualization-table-tr-head"></tr>');

    this.typeArrs = [];

    this.container = "";
    self.tableDataRenderSelectedRow = [];
}

function getTDTypeFromArray(arrHeaderType, value, index) {

    var td = $('<td class="google-visualization-table-td"></td>');

    switch (arrHeaderType[index]) {
        case 'number':
            td.addClass('google-visualization-table-td-number');
            td.append(value);
            break;
        case 'string':
            td.append(value);
            break;
        case 'boolean':
            td.addClass('google-visualization-table-td-bool');
            if (value) {
                td.append('✔');
            } else {
                td.append('✗');
            }
            break;
        default:
            td.append(value);
    }

    return td;
}

jqueryRenderTable.prototype.addColumn = function (type, label, width) {

    var th = $('<th class="google-visualization-table-th gradient"></th>');
    th.attr('data-name', this.name);
    th.append(label);

    if (typeof width !== 'undefined') {
        th.css('width', width);
    }

    this.trHeaderRender.append(th);

    this.typeArrs.push(type);
}


jqueryRenderTable.prototype.addRow = function (rows, colspan) {

    var tr = $('<tr></tr>');
    for (i = 0; i < rows.length; i++) {
        var td = getTDTypeFromArray(this.typeArrs, rows[i], i);
        if (colspan != null) {
            td.attr("colspan", colspan);
        }
        tr.append(td);
    }

    this.tbodyRender.append(tr);

}

jqueryRenderTable.prototype.draw = function (data) {

    this.container.html('');
    this.typeArrs = [];

    data.tableRender.find('table').append(data.theaderRender.append(data.trHeaderRender));

    data.tableRender.find('table').append(data.tbodyRender.find('tr').attr("onclick", this.name + ".setSelection(this,event)"));
    this.container.append(data.tableRender);
    this.container.find('table > tbody > tr:odd').addClass("oddTableRow");
}

jqueryRenderTable.prototype.getSelection = function () {

    return self.tableDataRenderSelectedRow;
}

jqueryRenderTable.prototype.setSelection = function (t, e) {
    if (t == null) {
        self.tableDataRenderSelectedRow = [];
    } else {

        var tr = $(t);
        var rowIndex = tr.index();

        if (e.ctrlKey) {
            var found = jQuery.inArray(rowIndex, self.tableDataRenderSelectedRow);
            if (!(found >= 0)) {
                self.tableDataRenderSelectedRow.push(rowIndex);
            }
            if (tr.hasClass("selectedTableRow")) {
                tr.removeClass("selectedTableRow");
                self.tableDataRenderSelectedRow = jQuery.grep(self.tableDataRenderSelectedRow, function (a) {
                    return a !== rowIndex;
                });

            } else {
                tr.addClass("selectedTableRow");
            }


        } else if (e.shiftKey) {
            if (!(self.tableDataRenderSelectedRow.length > 0)) {
                self.tableDataRenderSelectedRow.push(rowIndex);
                tr.addClass("selectedTableRow");
            } else {

                var lastValue = this.tableDataRenderSelectedRow[self.tableDataRenderSelectedRow.length - 1];
                var currentSelected = rowIndex;

                var minIndex = Math.min(lastValue, currentSelected);
                var maxIndex = Math.max(lastValue, currentSelected);

                self.tableDataRenderSelectedRow = [];
                tr.parent().find('tr').each(function () {
                    $(this).removeClass("selectedTableRow");
                });

                tr.parent().find('tr').each(function () {
                    if ($(this).index() >= minIndex && $(this).index() <= maxIndex) {
                        $(this).addClass("selectedTableRow");
                        self.tableDataRenderSelectedRow.push($(this).index());
                    }
                });
            }
            window.getSelection().removeAllRanges();
        } else {
            self.tableDataRenderSelectedRow = [];
            tr.parent().find('tr').each(function () {
                if ($(this).index() != rowIndex) {
                    $(this).removeClass("selectedTableRow");
                }
            });

            if (tr.hasClass("selectedTableRow")) {
                tr.removeClass("selectedTableRow");
            } else {
                tr.addClass("selectedTableRow");
                self.tableDataRenderSelectedRow.push(rowIndex);
            }
        }

        //console.log(self.tableDataRenderSelectedRow);
    }
}
