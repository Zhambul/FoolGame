using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoolGame.Bll.Card;

namespace FoolGame.Bll.Game
{
    interface IPlayer
    {
        ICardSet CardSet { get; }
        int CardLimit { get; }
        void AddCard(ICard card);
        void RemoveCard(ICard card);
        void OnMoveFinished();
        void OnMoveStarted();
    }
}
