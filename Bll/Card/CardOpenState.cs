using System;
using System.Drawing;
using System.Resources;
using System.Windows.Media;
using FoolGame.Properties;

namespace FoolGame.Bll.Card
{
    class CardOpenState : ICardAppearanceState
    {
        public ImageSource GetAppearance(ICard card)
        {
            ResourceManager rm = Resources.ResourceManager;
            String sad = GetStringNameForCard(card.Suit, card.Value);
            Bitmap myImage = (Bitmap)rm.GetObject(sad);
            return Util.ConvertToImageSource(myImage);
        }

        private String GetStringNameForCard(CardSuit cardSuit, CardValue cardValue)
        {
            return String.Format("{0}_{1}", cardSuit, cardValue);
        }
    }
}
