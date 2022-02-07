$(document).ready(() => {
    new IndexOrder();
});

class IndexOrder {
    private orderList: JQuery = $('#orderList');
    private btnCheckOut: JQuery = $('#btnCheckOut');

    private Utilities;
    constructor() {
        this.Utilities = new Utilities();

        this.btnCheckOut.attr('disabled', 'true');
        var session = this.Utilities.ReadCookie("bookStoreSession");
        if (session != null && session != 'null') {
            this.fillTable(session);
            this.btnCheckOut.removeAttr('disabled')
        }

        this.btnCheckOut.click(e => {
            this.Utilities.manageRequest({
                url: 'Order/CheckOut', type: 'POST',
                data: {
                    number: session
                },
                callback: response => {
                    var message = '';
                    var obj = JSON.parse(response);
                    this.Utilities.CreateCookie("bookStoreSession", null, 1);
                    for (let item of obj.BookByOrder) {
                        if (item.Quatity == 0) {
                            message = message + 'Sorry, we can process this book ' + item.Book.Name + 'because we have only' + item.Book.Quantity;
                        }
                    }
                    swal.fire('Good!', message == '' ? 'Successful checkout.' : message, 'success');
                    this.btnCheckOut.attr('disabled', 'true');
                    this.orderList.empty();
                }
            });
        });
    }
    fillTable = (session) => {
        this.Utilities.manageRequest({
            url: 'Order/GetByNumber?number=' + session, type: 'GET', callback: response => {
                for (let item of response.bookByOrder) {
                    this.orderList.append('<tr>'
                        + '<td>' + item.book.tittle + '</td>'
                        + '<td> <div class="input-field col s4"><input value="' + item.quatity + '" placeholder = "Placeholder" id = "book' + item.book.id +'" type = "number" class= "validate" style="width:auto"></div></td>'
                        + '<td>' + item.book.price + '</td>'
                        + '<td>' + (+item.quatity * +item.book.price) + ' </td>'
                        + '</tr>');
                    this.UpdateQuatityChanges(item.book.id);
                }
            }
        });
    }

    UpdateQuatityChanges(bookId) {
        $('#book' + bookId).focusout(e => {
            var session = this.Utilities.ReadCookie("bookStoreSession");
            this.Utilities.manageRequest({
                url: 'Order/Update', type: 'PUT',
                data: {
                    Id: 0,
                    IsPreorder: true,
                    IdBook: bookId,
                    IdOrder: session,
                    Quatity: $('#book' + bookId).val(),
                },
                callback: response => {
                    this.orderList.empty();
                    this.fillTable(session);
                }
            });
        });
    }
}