using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FoolGame.Properties;

namespace FoolGame.Bll.Card
{
    class CardClosedState : ICardAppearanceState
    {
        public ImageSource GetAppearance(ICard card)
        {
            return Util.ConvertToImageSource(Resources.Back);
        }
    }
}
