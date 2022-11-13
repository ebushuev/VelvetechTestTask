
namespace TodoApiRepository.Models
{
    public class TodoItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Todo item name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Todo item readiness flag
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Secret field
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Concurency token
        /// </summary>
        public byte[] Rowversion { get; set; }
    }
}
