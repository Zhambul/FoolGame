using System;
using System.Drawing;
using System.Resources;
using System.Windows.Media;
using FoolGame.Bll.Card;
using FoolGame.Properties;

namespace FoolGame.Bll.CardFabric
{
    class CardIniter : ICardIniter
    {
        private readonly ICardAppearanceState _closedCard;

        public CardIniter()
        {
            _closedCard = new CardClosedState();
        }

        public ICardAppearanceState GetClosedAppearance()
        {
            return _closedCard;
        }

    }
}
