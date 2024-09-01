﻿using ASAP.Domain.Entities.Common;

namespace ASAP.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
