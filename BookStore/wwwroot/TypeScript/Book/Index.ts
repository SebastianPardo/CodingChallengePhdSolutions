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
        this.Utilities.manageRequest({
            url: 'Book/GetAll', type: 'GET', callback: response => {
                debugger
                var colCount = 4;
                var rowNumber = 0;
                for (let item of response) {
                    if (colCount == 4) {
                        this.bookList.append(this.BookCards.GetRow(rowNumber));
                    } else if (colCount == 0) {
                        colCount = 4;
                        rowNumber++;
                    } else {
                        colCount--;
                    }
                    $("#bookRow" + rowNumber).append(this.BookCards.GetCard(item));
                }
            }
        })
    }
}