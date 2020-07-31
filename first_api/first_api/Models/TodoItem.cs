using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace first_api.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }

        internal object ItemToDTO(TodoItem x)
        {
            throw new NotImplementedException();
        }
    }
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
