using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoolGame.Bll.Card;
using FoolGame.Bll.CardFabric;
using FoolGame.Bll.Game;
using FoolGame.Bll.Vk;

namespace FoolGame.Dbl
{
    class DbHelper : IDbHelper
    {
        public void Save(IGame game)
        {
            File.WriteAllText(GetFilePath(),game.ToString());
        }

        public String GetFilePath()
        {
            String debugPath = AppDomain.CurrentDomain.BaseDirectory;
            String projectPath = debugPath.Substring(
                0, debugPath.IndexOf("\\bin\\Debug", StringComparison.Ordinal));
            return  projectPath + "\\save.txt";
        }
        public Game Load(IGameCallback gameCallback, IDeckChanged deckChanged)
        {
            Game game;
            using (var reader = new StreamReader(GetFilePath()))
            {
                while ((reader.ReadLine()) != null)
                {
                      
                }
                game = new Game( new Player(new CardCollection()),new Player(new CardCollection()), 
                    new Deck(new CardCollection(), new CardFabric(new CardIniter()), deckChanged),new CardCollection()
                    ,gameCallback,new VkHelper());
            }
            return game;
        }
    }
}
