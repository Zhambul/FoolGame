using System.Windows.Media;
using FoolGame.Bll.Card;

namespace FoolGame.Bll.CardFabric
{
    interface ICardIniter
    {
        ImageSource GetBackImageSource();
        ImageSource GetFrontImageSource(CardSuit cardSuit, CardValue cardValue);
    }
}
