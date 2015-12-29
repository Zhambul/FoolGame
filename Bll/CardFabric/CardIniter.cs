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
        private readonly ICardAppearanceStrategy _closedCard;

        public CardIniter()
        {
            _closedCard = new CardClosed();
        }

        public ICardAppearanceStrategy GetClosedAppearance()
        {
            return _closedCard;
        }

    }
}
