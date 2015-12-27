using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FoolGame.Bll.Card
{
    class CardCollection : ICardCollection
    {
        public ObservableCollection<ICard> Cards { get; set; }

        public CardCollection()
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

        public void RemoveCardAt(int i)
        {
            Cards.RemoveAt(i);
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var card in Cards)
            {
                result.Append(card);
            }
            return result.ToString();
        }
    }
}
