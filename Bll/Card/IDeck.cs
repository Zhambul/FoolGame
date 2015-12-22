using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoolGame.Bll.CardFabric;

namespace FoolGame.Bll.Card
{
    interface IDeck 
    {
        CardSuit TrumpSuit { get; }
        ICard TrumpCard { get; }
        ICardSet CardSet { get; }
        ICardFabric CardFabric { get; }
        int CardLimit { get; }
        ICard GetNextCard();
    }
}
