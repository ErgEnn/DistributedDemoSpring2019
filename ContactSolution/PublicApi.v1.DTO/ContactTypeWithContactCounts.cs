namespace PublicApi.v1.DTO
{
    public class ContactTypeWithContactCounts
    {
        public int Id { get; set; }
        public string ContactTypeValue { get; set; }

        public int ContactCount { get; set; }
    }
}