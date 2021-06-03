namespace CalixtosStore.Email
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var consumer = new Consumer();
            consumer.Execute();
        }
    }
}