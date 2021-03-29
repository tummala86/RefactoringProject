namespace PinnacleSample
{
    public interface ICustomerRepository
    {
        Customer GetByName(string name);
    }
}