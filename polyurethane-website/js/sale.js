cartjs.initialize({
    emailOrdersTo  : 'your-shop@mail.com',
    language       : 'ukrainian', // Также доступны 'english' и 'ukrainian'.
    currency       : 'грн.',

    // Дополнительные опции, можно их не указывать.
    requireName    : true,      // Спрашивать имя покупателя.
    requirePhone   : true,      // Спрашивать телефон покупателя.
    requireEmail   : false,     // Спрашивать почту покупателя.
    requireAddress : true,     // Спрашивать адрес покупателя.
})