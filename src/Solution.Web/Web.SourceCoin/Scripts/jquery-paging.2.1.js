function jqueryLoadData() {
    var self = this;
    self.table = null;

    self.pageIndex = 0;
    self.pageSize = 0;
    self.total = 0;

    self.loadListUrl = "";
    self.loadListUrl_v2 = "";
    self.containerId = "";
    self.containerId_v2 = "";
    self.className = "jqueryLoadList";
    self.isLoadListWhenInit = true;
};

jqueryLoadData.prototype.init = function (loadListUrl, pageSize, container) {
    var self = this;

    self.loadListUrl = loadListUrl;
    self.pageSize = parseInt(pageSize);
    if (container !== null) {
        self.containerId = container;
    }

    if (self.isLoadListWhenInit) {
        self.loadList(0);
    }
};

jqueryLoadData.prototype.init_v2 = function (loadListUrl, pageSize, container) {
    var self = this;

    self.loadListUrl_v2 = loadListUrl;
    self.pageSize = parseInt(pageSize);
    if (container !== null) {
        self.containerId_v2 = container;
    }

    if (self.isLoadListWhenInit) {
        self.loadList_v2(0);
    }
};

jqueryLoadData.prototype.loadList = function (pageIndex) {
    var self = this;

    if (pageIndex !== null) {
        self.pageIndex = pageIndex;
    }
    else {
        self.pageIndex = 0;
    }
    var data;
    if (typeof (self.getCustomData) === 'undefined') {
        data = { pageIndex: self.pageIndex, pageSize: self.pageSize };
    }
    else {
        data = self.getCustomData();
    }

    $.ajax({
        url: self.loadListUrl,
        data: JSON.stringify(data),
        type: 'POST',
        contentType: 'application/json',
        beforeSend: function () {

        },
        success: function (result) {
            self.displayData(result);
            self.loadPaging();
        },
        error: function (result) {
        }
    });
};

jqueryLoadData.prototype.loadList_v2 = function (pageIndex) {
    var self = this;

    if (typeof(pageIndex) !== 'undefined') {
        self.pageIndex = pageIndex;
    }
    else {
        self.pageIndex = 0;
    }
    var data;
    if (typeof (self.getCustomData) === 'undefined') {
        data = { pageIndex: self.pageIndex, pageSize: self.pageSize };
    }
    else {
        data = self.getCustomData();
    }

    $.ajax({
        url: self.loadListUrl_v2,
        data: JSON.stringify(data),
        type: 'POST',
        contentType: 'application/json',
        beforeSend: function () {

        },
        success: function (result) {
            self.displayData_v2(result);
            self.loadPaging_v2();
        },
        error: function (result) {
        }
    });
};

jqueryLoadData.prototype.loadPaging = function () {
    var self = this;

    var html = "";
    $("#" + self.containerId + " .pagination-list").html(html);
    var totalPage = Math.ceil(self.total / self.pageSize);

    if (totalPage > 1) {
        totalPage -= 1;
        var pageIndex = self.pageIndex;
        var prev = pageIndex - 2;
        var next = pageIndex + 3;
        if (prev <= 0) {
            next += prev * (-1);
        }
        if (next >= totalPage) {
            prev -= next - totalPage;
            next = totalPage;
        }
        var i = prev <= 0 ? 0 : prev;
        var j = next >= totalPage ? totalPage : next;
        html = "<nav><ul class='pagination'>";
        for (var k = i; k <= j; k++) {
            if (k !== pageIndex) {
                html += "<li class='page-item'><a class='page-link' href='javascript:void(0);' onclick='" + self.className + ".loadList(" + k + ");'>" + (k + 1) + "</a></li>";
            } else {
                html += "<li class='page-item'><a class='page-link' style='font-weight:bold;background:#4CAF50; color: #fff' href='javascript:void(0);'>" + (k + 1) + "</a></li>";
            }
        }
        html += "</ul></nav>";
        $("#" + self.containerId + " .pagination-list").html(html);
    }
};

jqueryLoadData.prototype.loadPaging_v2 = function () {
    var self = this;

    var html = "";
    $("#" + self.containerId_v2 + " .pagination-list").html(html);
    var totalPage = Math.ceil(self.total / self.pageSize);

    if (totalPage > 1) {
        totalPage -= 1;
        var pageIndex = self.pageIndex;
        var prev = pageIndex - 2;
        var next = pageIndex + 3;
        if (prev <= 0) {
            next += prev * (-1);
        }
        if (next >= totalPage) {
            prev -= next - totalPage;
            next = totalPage;
        }
        var i = prev <= 0 ? 0 : prev;
        var j = next >= totalPage ? totalPage : next;
        html = "<nav><ul class='pagination'>";
        for (var k = i; k <= j; k++) {
            if (k !== pageIndex) {
                html += "<li class='page-item'><a class='page-link' href='javascript:void(0);' onclick='" + self.className + ".loadList_v2(" + k + ");'>" + (k + 1) + "</a></li>";
            } else {
                html += "<li class='page-item'><a class='page-link' href='javascript:void(0);'>" + (k + 1) + "</a></li>";
            }
        }
        html += "</ul></nav>";
        $("#" + self.containerId_v2 + " .pagination-list").html(html);
    }
};

jqueryLoadData.prototype.drawTable = function (data) {
    var self = this;

    data.container = $("#" + self.containerId + " .table-list");
    self.table = data;
    self.table.draw(data);
};

jqueryLoadData.prototype.drawTable_v2 = function (data) {
    var self = this;

    data.container = $("#" + self.containerId_v2 + " .table-list");
    self.table = data;
    self.table.draw(data);
};

var jqueryLoadList = new jqueryLoadData();