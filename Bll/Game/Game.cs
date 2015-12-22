using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoolGame.Bll.Card;

namespace FoolGame.Bll.Game
{
    class Game : IGame
    {
        public IPlayer UserPlayer { get; private set; }
        public IPlayer CompPlayer { get; private set; }

        private readonly IDeck _deck;
        public Game(IPlayer userPlayer, IPlayer compPlayer, IDeck deck)
        {
            UserPlayer = userPlayer;
            CompPlayer = compPlayer;
            _deck = deck;
        }


        public void OnGameStarted()
        {
            GiveCards();
        }

        private void GiveCards(IPlayer player, bool userPlayer)
        {
            while (player.CardLimit != player.CardSet.Count && _deck.CardSet.Count != 0)
            {
                var card = _deck.GetNextCard();
                if (userPlayer)
                {
                    card.VisibilityState = CardVisibilityState.Visible;
                }
                player.AddCard(card);
            }
        }

        public void OnMovesEnded()
        {
            GiveCards();
        }

        private void GiveCards()
        {
            GiveCards(UserPlayer, true);
            GiveCards(CompPlayer, false);
        }

        public void OnPlayerMove()
        {
            OnComputerMove();
        }

        public void OnComputerMove()
        {

        }
    }
}
