using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolGame.Bll.Card
{
    interface ICardSet
    {
        ObservableCollection<ICard> Cards { get; set; }
        int Count { get; }
        void AddCard(ICard card);
        void RemoveCard(ICard card);
        ICard GetCardAt(int i);
        ICard GetLastCard();
    }
}
