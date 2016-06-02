namespace LoadingEntitiesAndNavigationProperties.Recipe14
{
    public  class Invoice
    {
        public int InvoiceId { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}