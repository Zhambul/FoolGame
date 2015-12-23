using FoolGame.Bll.CardFabric;

namespace FoolGame.Bll.Card
{
    interface IDeck 
    {
        CardSuit TrumpSuit { get; }
        ICard TrumpCard { get; set; }
        ICardCollection CardCollection { get; }
        ICardFabric CardFabric { get; }
        int CardLimit { get; }
        ICard GetNextCard();
    }
}
