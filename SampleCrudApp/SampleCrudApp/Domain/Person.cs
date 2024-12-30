namespace SampleCrudApp.Domain
{
    public class Person
    {
        public Person(string firstName, string lastName, string nationalCode, DateTime birthDay)
        {
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            BirthDay = birthDay;
        }

        public int Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string NationalCode { get; private set; }

        public DateTime BirthDay { get; private set; }

        internal void Update(string firstName, string lastName, string nationalCode)
        {
            FirstName = firstName;
            LastName = lastName;   
            NationalCode = nationalCode;
        }
    }
}
