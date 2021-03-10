const responseCode = {
    /// <summary>
    /// успішно отримали відповідь
    /// </summary>
    success: 0,

    /// <summary>
    /// запит не було відправлено на сервер, через відсутність зв'язку
    /// </summary>
    notSent: 1,

    /// <summary>
    /// таймаут відповіді від сервера
    /// </summary>
    timeout: 2,

    /// <summary>
    /// від сервера прийшла помилка (Exception) на запит
    /// </summary>
    error: 3
};

export default responseCode;