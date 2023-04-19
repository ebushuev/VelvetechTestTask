namespace TodoApiDto.StrongId
{
    public class TodoId
    {
        public long ObjectId { get; set; }

        public TodoId(long objectId)
        {
            ObjectId = objectId;
        }
    }
}