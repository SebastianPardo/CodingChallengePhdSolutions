$(document).ready(function () {
    new IndexBook();
});
var IndexBook = /** @class */ (function () {
    function IndexBook() {
        var _this = this;
        this.bookList = $('#BookList');
        this.BookCards = new BookCard();
        this.Utilities = new Utilities();
        this.Utilities.manageRequest({
            url: 'Book/GetAll', type: 'GET', callback: function (response) {
                debugger;
                var colCount = 4;
                var rowNumber = 0;
                for (var _i = 0, response_1 = response; _i < response_1.length; _i++) {
                    var item = response_1[_i];
                    if (colCount == 4) {
                        _this.bookList.append(_this.BookCards.GetRow(rowNumber));
                    }
                    else if (colCount == 0) {
                        colCount = 4;
                        rowNumber++;
                    }
                    else {
                        colCount--;
                    }
                    $("#bookRow" + rowNumber).append(_this.BookCards.GetCard(item));
                }
            }
        });
    }
    return IndexBook;
}());
//# sourceMappingURL=Index.js.map