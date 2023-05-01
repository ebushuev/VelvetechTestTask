using System;

namespace TodoApiDTO.DataAccessLayer.Entity
{
    public abstract class EntityBase
    {
        public long Id { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public void SetActive()
        {
            this.Active = true;
        }

        public void InitCreate()
        {
            CreatedDate = DateTime.UtcNow.AddHours(5);
            SetActive();
        }

        public void InitChange()
        {
            LastModifiedDate = DateTime.UtcNow.AddHours(5);
            SetActive();
        }
    }
}
