using System;
namespace BookStore.Models
{
    public class Book
    {
        public Book()
        {
        }
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate{ get; set; }
        public int Amount{ get; set; }
        public string Image{ get; set; }
        //Navigation Properties
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
