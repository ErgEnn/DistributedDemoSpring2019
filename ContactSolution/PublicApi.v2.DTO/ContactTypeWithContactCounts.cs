﻿namespace PublicApi.v2.DTO
{
    public class ContactTypeWithContactCounts
    {
        public int Id { get; set; }
        public string ContactTypeValue { get; set; }

        public int ContactCount { get; set; }
    }
}