using Emejzon.Services;

namespace Emejzon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insert email: ");
            string? email = Console.ReadLine();
            Console.WriteLine("Insert password: ");
            string? password = Console.ReadLine();
            PasswordManager.PasswordVerify += (email, success) => Console.WriteLine
            ($"Login for {email} {(success ? "succeded" : "failed")}");

            if(PasswordManager.VerifyPassword(email, password)){
                Console.ReadKey();
            }
            else{
                Console.WriteLine("Email or password incorrect");
                Console.ReadKey();
            }


            
        }
    }
}
