using System;
using System.Collections.Generic;

namespace TodoWebAPI
{
    public partial class User
    {
        public int Id { get; set; }
        public Guid GUserIdx { get; set; }
        public string? SUsername { get; set; }
        public string? SPasswordHash { get; set; }
    }
}
