using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TodoApiDTO.DAL.Models
{
    public class TodoItem
    {
        public long Id {
            get; set;
        }
        public string Name {
            get; set;
        }
        public bool IsComplete {
            get; set;
        }
        public string Secret {
            get; set;
        }

        [Timestamp]
        public byte[] Timestamp {
            get; set;
        }
    }
}
