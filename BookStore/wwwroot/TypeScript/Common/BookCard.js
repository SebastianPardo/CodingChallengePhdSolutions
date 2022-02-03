var BookCard = /** @class */ (function () {
    function BookCard() {
        var _this = this;
        this.card = "";
        this.GetCard = function (book) {
            var card = _this.card.replace('#TITTLE', book.tittle);
            card = card.replace('#AUTHOR', book.author.name);
            card = card.replace('#DATE', book.releaseDate);
            card = card.replace('#DESCRIPTION', book.description);
            card = card.replace('#PRICE', book.price);
            card = card.replace('#IMAGE', book.coverImage);
            card = card.replace('#BOOKID', book.id);
            return card;
        };
        this.GetRow = function (number) {
            return '<div id="bookRow' + number + '" class="row"></div>';
        };
        this.AddClickBtnAdd = function (bookId) {
            $('#btnAdd' + bookId).click(function (e) {
                var session = _this.Utilities.ReadCookie("bookStoreSession");
                _this.Utilities.manageRequest({
                    url: 'Order/Create', type: 'POST',
                    data: {
                        Id: 0,
                        IsPreorder: true,
                        IdBook: bookId,
                        Quantity: 0,
                        Order: {
                            Id: 0,
                            Number: session == null ? 0 : session,
                            DateOrder: new Date(),
                        },
                    },
                    callback: function (response) {
                        var obj = JSON.parse(response);
                        _this.Utilities.CreateCookie("bookStoreSession", obj.Number, 7);
                        swal.fire('Good!', 'Added to cart.', 'success');
                        $('#itemsCart').text(+$('#itemsCart').text() + 1);
                    }
                });
            });
        };
        this.Utilities = new Utilities();
        this.card = '<div class="col s6 m3">' +
            '<div class="card">' +
            '<div class="card-image">' +
            '<img style="width: 70%;height: 50%;border-radius: 50%;margin-left: auto;margin-right: auto;" src = "#IMAGE">' +
            '<a class="btn-floating btn-small halfway-fab waves-effect waves-light green" id="btnAdd#BOOKID"> <i class="material-icons"> add </i></a>' +
            '</div>' +
            '<div class="card-content">' +
            '<span class="card-title">#TITTLE</span>' +
            '<p>#AUTHOR - #DATE</p>' +
            '<p>#DESCRIPTION</p>' +
            '<span class="card-title">#PRICE</span>' +
            '</div>' +
            '</div>' +
            '</div>';
    }
    return BookCard;
}());
//# sourceMappingURL=BookCard.js.map