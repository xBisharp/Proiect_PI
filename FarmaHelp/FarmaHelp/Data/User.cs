namespace FarmaHelp.Data
{
    public class User
    {

        public string Name { get; set; }

        public string PreName { get; set; }
        public string Email {get;set;}
        public string Password {get;set;}
        public string PhoneNumber {get;set;}

        public bool esteAdmin { get;set;}
        public string GetPhoneNumber()
        {
            return PhoneNumber;
        }

        public void SetPhoneNumber(string value) { 
        
            PhoneNumber = value;
        
        }

        public string GetPassword()
        {
            return Password;
        }
        public void SetPassword(string value)
        {

            Password = value;
        }

         

        public string GetEmail()
        {
            return Email;
        }
        public void SetEmail(string value)
        {
            Email = value;
        }


        public string GetName()
        {
            return Name;
        }

        public void SetName(string value)
        {
            Name = value;
        }
    }
}
