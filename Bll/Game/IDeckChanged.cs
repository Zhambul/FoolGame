using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoolGame.Bll.Card;

namespace FoolGame.Bll.Game
{
    interface IDeckChanged
    {
        void OnDeckChanged(int countOfCardsInDeck);
        void OnTrumpCardSelected(ICard trumpCard);
    }
}
