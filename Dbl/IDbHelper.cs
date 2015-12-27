using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoolGame.Bll.Game;

namespace FoolGame.Dbl
{
    interface IDbHelper
    {
        void Save(IGame game);
        Game Load(IGameCallback gameCallback, IDeckChanged deckChanged);
    }
}
