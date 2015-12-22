using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolGame.Bll.Game
{
    interface IGame
    {
        IPlayer UserPlayer { get; }
        IPlayer CompPlayer { get; }
        void OnGameStarted();
        void OnMovesEnded();
        void OnPlayerMove();
        void OnComputerMove();
    }
}
