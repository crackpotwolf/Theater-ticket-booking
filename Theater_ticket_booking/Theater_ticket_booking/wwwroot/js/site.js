//Обрабатывает отправку форм
function sendForm(form, event, then, options) {
    event.preventDefault();

    onStart = options?.onStart;
    if (onStart != undefined)
        onStart();

    send(
        $(form).attr('action'),
        $(form).attr('method'),
        then,
        options?.data ?? $(form).serialize()
    );
}

//Обрабатывает отправку запросов на сервер
function send(action, method, then, data) {
    $.ajax({
        url: action,
        method: method,
        complete: (response) => {
            if (then != undefined)
                then(response);
        },
        data: typeof (data) == typeof ('') ? data : JSON.stringify(data),
        contentType: "application/json; charset=UTF-8",
    });
}

//Возвращает время в формате HH:mm
function OnlyTime(date) {
    var minutes = date.getMinutes().toString();
    if (minutes.length == 1)
        minutes = '0' + minutes;

    return `${date.getHours()}:${minutes}`;
}

// Парсит JWT токен
function ParseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
};

//Возвращает информацию о пользователе из localStorage
function GetCurrentUserInfo() {
    return JSON.parse(localStorage.getItem('user-info'));
}

//Добавляет текущие параметры запроса к ссылке
function OpenLink(tagA, event) {
    event.preventDefault();
    var uri = $(tagA)[0].href += new URL(window.location.href).search;
    window.open(uri, '_self');
}

//Преобразование формы в JSON
function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}

// Получение информации о пользователе с сервера
function GetUserInfoFromServer() {
    var token = $.cookie("Authorization");
    var userId = ParseJwt(token).Id;

    $.ajax({
        url: `/api/Users/${userId}`,
        async: false,
        complete: response => {
            localStorage.setItem("user-info", JSON.stringify(response.responseJSON));
        }
    });
}

// Сортировка по полю
Array.prototype.sortBy = function (p) {
    return this.slice(0).sort(function (a, b) {
        return (a[p] > b[p]) ? 1 : (a[p] < b[p]) ? -1 : 0;
    });
}

function get(dict, keys) {
    var spl = keys.split('>');
    var res = dict[spl[0]] ?? '';
    for (var i = 1; i < spl.length; i++) {
        res = res[spl[i]] ?? '';
    }
    return res;
}

//Log out
function LogOut() {
    $.removeCookie("Authorization");
    window.location = '/Login';
}