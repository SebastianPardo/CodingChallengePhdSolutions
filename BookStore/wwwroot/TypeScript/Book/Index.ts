$(document).ready(() => {
    new IndexBook();
});

class IndexBook {
    private bookList: JQuery = $('#BookList');

    private BookCards;
    private Utilities;
    constructor() {
        this.BookCards = new BookCard();
        this.Utilities = new Utilities();
        var session = this.Utilities.ReadCookie("bookStoreSession");
        if (session != null) {
            this.Utilities.manageRequest({
                url: 'Order/GetByNumber?number=' + session, type: 'GET', callback: response => {
                    $('#itemsCart').html('<div id="itemsCart">' + response.bookByOrder.length + '</div>')
                }
            })
        }
        this.Utilities.manageRequest({
            url: 'Book/GetAll', type: 'GET', callback: response => {
                var colCount = 4;
                var rowNumber = 0;
                for (let item of response) {
                    debugger
                    if (colCount == 4) {
                        this.bookList.append(this.BookCards.GetRow(rowNumber));
                        colCount--;
                    } else if (colCount == 0) {
                        colCount = 4;
                        rowNumber++;
                    } else {
                        colCount--;
                    }
                    $("#bookRow" + rowNumber).append(this.BookCards.GetCard(item, session));
                    this.BookCards.AddClickBtnAdd(item.id);
                }
            }
        })
    }
}