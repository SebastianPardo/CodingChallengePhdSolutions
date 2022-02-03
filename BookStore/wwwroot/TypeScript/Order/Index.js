$(document).ready(function () {
    new IndexOrder();
});
var IndexOrder = /** @class */ (function () {
    function IndexOrder() {
        var _this = this;
        this.orderList = $('#orderList');
        this.btnCheckOut = $('#btnCheckOut');
        this.fillTable = function (session) {
            _this.Utilities.manageRequest({
                url: 'Order/GetByNumber?number=' + session, type: 'GET', callback: function (response) {
                    for (var _i = 0, _a = response.bookByOrder; _i < _a.length; _i++) {
                        var item = _a[_i];
                        _this.orderList.append('<tr>'
                            + '<td>' + item.book.tittle + '</td>'
                            + '<td scope="row"> <div class="input-field col s6"><input value="' + item.quatity + '" placeholder = "Placeholder" id = "book' + item.book.id + '" type = "number" class= "validate" ></div></td>'
                            + '<td>' + item.book.price + '</td>'
                            + '<td>' + (+item.quatity * +item.book.price) + ' </td>'
                            + '</tr>');
                        _this.UpdateQuatityChanges(item.book.id);
                    }
                }
            });
        };
        this.Utilities = new Utilities();
        this.btnCheckOut.attr('disabled', 'true');
        var session = this.Utilities.ReadCookie("bookStoreSession");
        if (session != null && session != 'null') {
            this.fillTable(session);
            this.btnCheckOut.removeAttr('disabled');
        }
        this.btnCheckOut.click(function (e) {
            _this.Utilities.manageRequest({
                url: 'Order/CheckOut', type: 'POST',
                data: {
                    number: session
                },
                callback: function (response) {
                    var message = '';
                    var obj = JSON.parse(response);
                    _this.Utilities.CreateCookie("bookStoreSession", null, 1);
                    for (var _i = 0, _a = obj.BookByOrder; _i < _a.length; _i++) {
                        var item = _a[_i];
                        if (item.Quatity == 0) {
                            message = message + 'Sorry, we can process this book ' + item.Book.Name + 'because we have only' + item.Book.Quantity;
                        }
                    }
                    swal.fire('Good!', message == '' ? 'Successful checkout.' : message, 'success');
                    _this.btnCheckOut.attr('disabled', 'true');
                    _this.orderList.empty();
                }
            });
        });
    }
    IndexOrder.prototype.UpdateQuatityChanges = function (bookId) {
        var _this = this;
        $('#book' + bookId).focusout(function (e) {
            var session = _this.Utilities.ReadCookie("bookStoreSession");
            _this.Utilities.manageRequest({
                url: 'Order/Update', type: 'PUT',
                data: {
                    Id: 0,
                    IsPreorder: true,
                    IdBook: bookId,
                    IdOrder: session,
                    Quatity: $('#book' + bookId).val(),
                },
                callback: function (response) {
                    _this.orderList.empty();
                    _this.fillTable(session);
                }
            });
        });
    };
    return IndexOrder;
}());
//# sourceMappingURL=Index.js.map