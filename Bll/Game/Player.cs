using FoolGame.Bll.Card;

namespace FoolGame.Bll.Game
{
    class Player : IPlayer
    {
        public ICardCollection CardCollection { get; private set; }
        public int CardLimit { get; private set; }
        public GameRole GameRole { get; set; }
        public void AddCard(ICard card)
        {
            CardCollection.AddCard(card);
        }
        public void RemoveCard(ICard card)
        {
            CardCollection.RemoveCard(card);
        }
        public Player(CardCollection cardCollection)
        {
            CardCollection = cardCollection;
            CardLimit = 6;
        }
    }
}
