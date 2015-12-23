using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolGame.Bll.Game
{
    interface IGameCallback
    {
        void OnNotSuitableCard();
        void OnPassButtonVisible();
        void OnGetCardsButtonVisible();
        void OnRoleSwith(bool isAttacking);
        void OnGetCardsButtonHidden();
    }
}
