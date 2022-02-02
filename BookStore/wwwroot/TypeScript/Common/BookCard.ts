class BookCard {
    private card = ""
    constructor() {
        this.card = '<div class="col s6 m3">' +
            '<div class="card">' +
            '<div class="card-image">' +
            '<img style="width: 70%;height: 70%;border-radius: 50%;margin-left: auto;margin-right: auto;" src = "#IMAGE">' +
            '<a class="btn-floating btn-small halfway-fab waves-effect waves-light green"> <i class="material-icons"> add </i></a>' +
            '</div>' +
            '<div class="card-content">' +
            '<span class="card-title">#TITTLE</span>' +
            '<p>#AUTHOR - #DATE</p>' +
            '<p>#DESCRIPTION</p>' +
            '<span class="card-title">#PRICE</span>' +
            '</div>' +
            '</div>' +
            '</div>'        
    }

    GetCard = (book) => {
        var card = this.card.replace('#TITTLE', book.tittle)
        card = card.replace('#AUTHOR', book.author.name)
        card = card.replace('#DATE', book.releaseDate)
        card = card.replace('#DESCRIPTION', book.description)
        card = card.replace('#PRICE', book.price)
        return card
    }

    GetRow = (number) => {
        return '<div id="bookRow' + number + '" class="row"></div>'
    }
    
    
}