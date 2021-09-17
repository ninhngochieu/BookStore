namespace BookStore.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int SubTotal { get; set; }


        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}