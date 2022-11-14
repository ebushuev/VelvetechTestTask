using System;

namespace TodoApi.Domain.PartialModels
{
    public class BaseModel
    {
        public long Id { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateChange { get; set; }
    }
}
