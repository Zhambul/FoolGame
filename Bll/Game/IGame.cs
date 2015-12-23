using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoolGame.Bll.Card;

namespace FoolGame.Bll.Game
{
    interface IGame
    {
        IPlayer UserPlayer { get; }
        IPlayer CompPlayer { get; }
        CardSet TableCards { get; set; }
        void OnGameStarted();
        void OnMovesEnded(bool switchRoles);
        void OnPlayerMove(ICard card);
        void OnComputerMove(ICard card);
        void CheckWin();
        int Pass();
        void GetCardsFromTableToUser();
    }
}
