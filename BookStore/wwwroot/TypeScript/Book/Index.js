$(document).ready(function () {
    new IndexBook();
});
var IndexBook = /** @class */ (function () {
    function IndexBook() {
        var _this = this;
        this.bookList = $('#BookList');
        this.BookCards = new BookCard();
        this.Utilities = new Utilities();
        var session = this.Utilities.ReadCookie("bookStoreSession");
        if (session != null) {
            this.Utilities.manageRequest({
                url: 'Order/GetByNumber?number=' + session, type: 'GET', callback: function (response) {
                    $('#itemsCart').html('<div id="itemsCart">' + response.bookByOrder.length + '</div>');
                }
            });
        }
        this.Utilities.manageRequest({
            url: 'Book/GetAll', type: 'GET', callback: function (response) {
                var colCount = 4;
                var rowNumber = 0;
                for (var _i = 0, response_1 = response; _i < response_1.length; _i++) {
                    var item = response_1[_i];
                    debugger;
                    if (colCount == 4) {
                        _this.bookList.append(_this.BookCards.GetRow(rowNumber));
                        colCount--;
                    }
                    else if (colCount == 0) {
                        colCount = 4;
                        rowNumber++;
                    }
                    else {
                        colCount--;
                    }
                    $("#bookRow" + rowNumber).append(_this.BookCards.GetCard(item, session));
                    _this.BookCards.AddClickBtnAdd(item.id);
                }
            }
        });
    }
    return IndexBook;
}());
//# sourceMappingURL=Index.js.map