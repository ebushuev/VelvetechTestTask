namespace TodoApiDto.Services.Data
{
    public class ServiceResult
    {
        /// <summary>
        ///
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        ///
        /// </summary>
        public bool IsNotFound { get; set; } = false;
    }

    /// <summary>
    ///
    /// </summary>
    public class ServiceResult<T> : ServiceResult
    {
        /// <summary>
        ///
        /// </summary>
        public T Result { get; set; }
    }
}