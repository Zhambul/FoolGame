using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoolGame.Bll.Card;

namespace FoolGame.Bll.Game
{
    class Player : IPlayer
    {
        public ICardSet CardSet { get; private set; }
        public int CardLimit { get; private set; }

        public void AddCard(ICard card)
        {
            CardSet.AddCard(card);
        }

        public void RemoveCard(ICard card)
        {
            CardSet.RemoveCard(card);
        }

        public void OnMoveFinished()
        {
            throw new NotImplementedException();
        }

        public void OnMoveStarted()
        {
            throw new NotImplementedException();
        }

        public Player(CardSet cardSet)
        {
            CardSet = cardSet;
            CardLimit = 6;
        }
    }
}
