using System.Collections.ObjectModel;

namespace FoolGame.Bll.Card
{
    interface ICardCollection
    {
        ObservableCollection<ICard> Cards { get; set; }
        int Count { get; }
        void AddCard(ICard card);
        void RemoveCard(ICard card);
        ICard GetCardAt(int i);
        ICard GetLastCard();
    }
}
