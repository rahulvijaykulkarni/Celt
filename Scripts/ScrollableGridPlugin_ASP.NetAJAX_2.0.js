(function ($) {
    $.fn.Scrollable = function (options) {
        var defaults = {
            ScrollHeight: 300,
            Width: 0,
            IsInUpdatePanel: false
        };
        var options = $.extend(defaults, options);
        return this.each(function () {
            var grid = $(this).get(0);
            var gridId = grid.id;
            MakeScrollable(grid, options);
            if (options.IsInUpdatePanel) {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        MakeScrollable($("#" + gridId).get(0), options);
                    });
                }
            }
        });
    };
})(jQuery);

function MakeScrollable(grid, options) {
    var gridId = grid.id;
    var gridHeight = grid.offsetHeight;
    var headerCellWidths = new Array();
    for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
        headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
    }
    grid.parentNode.appendChild(document.createElement("div"));
    var parentDiv = grid.parentNode;

    var table = document.createElement("table");
    for (i = 0; i < grid.attributes.length; i++) {
        if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
            table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
        }
    }
    table.style.cssText = grid.style.cssText;
    table.appendChild(document.createElement("tbody"));
    table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
    var cells = table.getElementsByTagName("TH");

    var gridRow = grid.getElementsByTagName("TR")[0];
    for (var i = 0; i < cells.length; i++) {
        var width;
        if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
            width = headerCellWidths[i];
        }
        else {
            width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
        }
        cells[i].style.width = parseInt(width) + "px";
        //gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
        $("tr", $(grid)).each(function () {
            $("td", this).eq(i).css("width", width);
        });
    }
    parentDiv.removeChild(grid);

    var dummyHeader = document.createElement("div");
    dummyHeader.id = "header" + gridId;
    dummyHeader.appendChild(table);
    parentDiv.appendChild(dummyHeader);
    var scrollableDiv = document.createElement("div");


    dummyHeader = document.getElementById(dummyHeader.id);
    dummyHeader.style.width = "9999999999999px";
    var table_width = dummyHeader.getElementsByTagName("table")[0].offsetWidth;
    dummyHeader.style.width = (table_width + 17) + "px";
    scrollableDiv.style.cssText = "overflow:auto;height:" + options.ScrollHeight + "px;width:" + (table_width + 17) + "px";
    scrollableDiv.appendChild(grid);
    parentDiv.appendChild(scrollableDiv);
    if (options.Width > 0) {
        parentDiv.style.cssText = "overflow:auto;width:" + options.Width + "px";
    }
};