using FoolGame.Bll.Card;

namespace FoolGame.Bll.Game
{
    interface IDeckChanged
    {
        void OnDeckChanged(int countOfCardsInDeck);
        void OnTrumpCardSelected(ICard trumpCard);
    }
}
