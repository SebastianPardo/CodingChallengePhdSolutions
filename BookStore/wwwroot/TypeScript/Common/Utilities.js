var Utilities = /** @class */ (function () {
    function Utilities() {
        this.manageRequest = function (options) {
            $.ajax({
                url: 'https://localhost:44327/' + options.url,
                data: options.data,
                type: options.type,
                success: function (response, status, jqXhr) {
                    if (options.callback != undefined) {
                        options.callback(response);
                    }
                },
                error: function (jqXhr, status, error) {
                    if (options.errorMessage === undefined || options.errorMessage === null || options.errorMessage === '') {
                        swal.fire('Error', 'Could NOT get satisfactory answer due to error : ' + error, 'error');
                    }
                    else {
                        swal.fire('Error', options.errorMessage, 'error');
                    }
                }
            });
        };
        this.CreateCookie = function (name, value, days) {
            var expires = "";
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toUTCString();
            }
            document.cookie = name + "=" + value + expires + "; path=/";
        };
        this.ReadCookie = function (name) {
            var nameEQ = name + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ')
                    c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0)
                    return c.substring(nameEQ.length, c.length);
            }
            return null;
        };
    }
    return Utilities;
}());
//# sourceMappingURL=Utilities.js.map