using FoolGame.Bll.Card;

namespace FoolGame.Bll.Game
{
    class Player : IPlayer
    {
        public ICardSet CardSet { get; private set; }
        public int CardLimit { get; private set; }
        public GameRole GameRole { get; set; }

        public void AddCard(ICard card)
        {
            CardSet.AddCard(card);
        }

        public void RemoveCard(ICard card)
        {
            CardSet.RemoveCard(card);
        }
        public Player(CardSet cardSet)
        {
            CardSet = cardSet;
            CardLimit = 6;
        }
    }
}
