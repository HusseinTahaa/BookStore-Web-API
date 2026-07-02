namespace BookStoreAPI.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
      : base(message)
        {
        }


        public NotFoundException(string resource, int id) : base($"{resource} with id {id} is not found!")
        {

        }
    }
}