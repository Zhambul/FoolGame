using System.Collections.ObjectModel;
using System.Linq;

namespace FoolGame.Bll.Card
{
    class CardSet : ICardSet
    {
        public ObservableCollection<ICard> Cards { get; set; }

        public CardSet()
        {
            Cards = new ObservableCollection<ICard>();
        }

        public int Count { get { return Cards.Count; } }

        public void AddCard(ICard card)
        {
            Cards.Add(card);
        }

        public void RemoveCard(ICard card)
        {
            Cards.Remove(card);
        }

        public ICard GetCardAt(int i)
        {
            return Cards.ElementAt(i);
        }

        public ICard GetLastCard()
        {
            return Cards.Last();
        }
    }
}
