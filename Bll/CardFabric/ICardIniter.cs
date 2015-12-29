using System.Windows.Media;
using FoolGame.Bll.Card;

namespace FoolGame.Bll.CardFabric
{
    interface ICardIniter
    {
        ICardAppearanceStrategy GetClosedAppearance();
    }
}
