$(document).ready(() => {
    new IndexOrder();
});

class IndexOrder {
    private orderList: JQuery = $('#orderList');

    private Utilities;
    constructor() {
        this.Utilities = new Utilities();
        var session = this.Utilities.ReadCookie("bookStoreSession");
        this.fillTable(session);
    }
    fillTable = (session) => {
        //this.orderList.html('<tbody id="orderList"></tbody>');
        this.Utilities.manageRequest({
            url: 'Order/GetByNumber?number=' + session, type: 'GET', callback: response => {
                for (let item of response.bookByOrder) {
                    debugger
                    this.orderList.append('<tr>'
                        + '<td>' + item.book.tittle + '</td>'
                        + '<td>' + item.quatity + '</td>'
                        + '<td>' + item.book.price + '</td>'
                        + '<td>' + (+item.quantity * +item.price) + ' </td>'
                        + '</tr>');
                }
            }
        });
    }
}