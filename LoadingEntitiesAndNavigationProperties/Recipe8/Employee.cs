namespace LoadingEntitiesAndNavigationProperties.Recipe8
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int DepartmetId { get; set; }
        public virtual Department Department { get; set; }
    }
}