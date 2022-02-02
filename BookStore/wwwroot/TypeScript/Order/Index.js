$(document).ready(function () {
    new IndexOrder();
});
var IndexOrder = /** @class */ (function () {
    function IndexOrder() {
        var _this = this;
        this.orderList = $('#orderList');
        this.fillTable = function (session) {
            //this.orderList.html('<tbody id="orderList"></tbody>');
            _this.Utilities.manageRequest({
                url: 'Order/GetByNumber?number=' + session, type: 'GET', callback: function (response) {
                    for (var _i = 0, _a = response.bookByOrder; _i < _a.length; _i++) {
                        var item = _a[_i];
                        debugger;
                        _this.orderList.append('<tr>'
                            + '<td>' + item.book.tittle + '</td>'
                            + '<td>' + item.quatity + '</td>'
                            + '<td>' + item.book.price + '</td>'
                            + '<td>' + (+item.quantity * +item.price) + ' </td>'
                            + '</tr>');
                    }
                }
            });
        };
        this.Utilities = new Utilities();
        var session = this.Utilities.ReadCookie("bookStoreSession");
        this.fillTable(session);
    }
    return IndexOrder;
}());
//# sourceMappingURL=Index.js.map