#pragma warning disable CS8604,CS8600

namespace refrigerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            int choice=0;
            do
            {
                choice = User.SelectAnAction(game);
                if(choice!=-1)
                   game.HandleChoice(choice);
            } while (choice != 100);
        }
    }  
}