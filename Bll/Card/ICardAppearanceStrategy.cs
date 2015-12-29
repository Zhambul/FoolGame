using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FoolGame.Bll.Card
{
    public interface ICardAppearanceStrategy
    {
        ImageSource GetAppearance(ICard card);
    }
}
