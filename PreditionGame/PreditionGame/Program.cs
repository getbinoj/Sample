using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreditionGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get the Level
            string _strLevel = "";
            int _level = 1;
            while (!int.TryParse(_strLevel, out _level))
            {
                Console.WriteLine("Please enter Level");
                _strLevel = Console.ReadLine();
            }
            //Initialize the ball and containers
            BallGame _game = new BallGame();
            int _containCount=_game.InitializeGame(_level);
            //Get the predictions 
            Console.WriteLine("Please enter your predition of container that is empty from 1 - {0}", _containCount);
            int _selectedContainer = 1;
            while (!int.TryParse(Console.ReadLine(), out _selectedContainer))
            {
                Console.WriteLine("Please enter a number !!");
            }
            //Play game
            int _emptyContainer=_game.Play();
            // Comapre the input with result
            if (_selectedContainer == _emptyContainer)
                Console.WriteLine("Your prediction is correct!!");
            else
                Console.WriteLine("Your prediction is wrong!! The empty container is {0}",_emptyContainer);
            //Display message
            //Console.WriteLine(_emptyContainer);
            Console.ReadLine();

        }
    }
}
