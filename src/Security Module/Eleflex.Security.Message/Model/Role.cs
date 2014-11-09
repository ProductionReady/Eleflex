﻿using System;
using System.Collections.Generic;

namespace Eleflex.Security.Message
{
    public partial class Role
    {
        public System.Guid RoleKey { get; set; }
        public bool Inactive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExtraData { get; set; }
    }
}