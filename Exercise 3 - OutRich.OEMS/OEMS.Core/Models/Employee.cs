using System;

namespace OEMS.Core.Models
{
    public class Employee : BaseModel
    {      
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }
        
        public void UpdateFrom(string firstName, string lastName, DateTime birthDate) 
        {            
			this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            
        }    
    }
}
