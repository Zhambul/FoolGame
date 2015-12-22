using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoolGame.Bll.Card;

namespace FoolGame.Bll.CardFabric
{
    interface ICardFabric
    {
        ICard GetCard();
    }
}
