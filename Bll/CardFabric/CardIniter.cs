using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FoolGame.Bll.Card;
using FoolGame.Properties;

namespace FoolGame.Bll.CardFabric
{
    class CardIniter : ICardIniter
    {
        public ImageSource GetBackImageSource()
        {
            return Util.ConvertToImageSource(Resources.Back);
        }

        public ImageSource GetFrontImageSource(CardSuit cardSuit, CardValue cardValue)
        {
            ResourceManager rm = Resources.ResourceManager;
            String sad = GetStringNameForCard(cardSuit, cardValue);
            Bitmap myImage = (Bitmap)rm.GetObject(sad);
            return Util.ConvertToImageSource(myImage);
        }

        private String GetStringNameForCard(CardSuit cardSuit, CardValue cardValue)
        {
            return String.Format("{0}_{1}", cardSuit, cardValue);
        }
    }
}
