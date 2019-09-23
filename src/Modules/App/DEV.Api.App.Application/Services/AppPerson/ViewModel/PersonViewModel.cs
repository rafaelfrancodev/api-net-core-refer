using System;

namespace DEV.Api.App.Application.Services.AppPerson.ViewModel
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirthday { get; set; }
        public bool Active { get; set; }
        public int? IdUser { get; set; }
    }
}
