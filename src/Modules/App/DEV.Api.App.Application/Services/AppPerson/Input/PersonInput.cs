using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Api.App.Application.Services.AppPerson.Input
{
    public class PersonInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirthday { get; set; }
        public bool Active { get; set; }
        public int? IdUser { get; set; }
    }
}
