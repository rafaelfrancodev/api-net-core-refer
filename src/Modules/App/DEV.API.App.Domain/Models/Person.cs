using DEV.API.App.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.API.App.Domain.Models
{
    public class Person : EntityAudited<int>
    {
        public string Name { get; set; }
        public DateTime DateBirthday { get; set; }
        public bool Active { get; set; }
        public int? IdUser { get; set; }
    }
}
