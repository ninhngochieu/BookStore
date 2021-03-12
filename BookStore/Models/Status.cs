using System.Collections.Generic;

namespace BookStore.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string StatusName { get; set; }

        public IList<Invoice> MyProperty { get; set; }
    }
}