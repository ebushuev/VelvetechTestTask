namespace TodoApiDTO.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Сервис с базовыми специфичными операциями.
    /// </summary>
    public class BaseControllerService
    {
        /// <summary>
        /// Возвращает ActionResult от типа объекта результата.
        /// </summary>
        /// <param name="result">Объект результата.</param>
        /// <typeparam name="T">Тип объекта результата.</typeparam>
        public ActionResult<T> GetActionResult<T>(T result)
        {
            return new ActionResult<T>(result);
        }
    }
}