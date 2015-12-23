using FoolGame.Bll.Card;

namespace FoolGame.Bll.Game
{
    enum GameRole
    {
        Attacker,
        Defender
    }
    interface IPlayer
    {
        ICardSet CardSet { get; }
        int CardLimit { get; }
        GameRole GameRole { get; set; }
        void AddCard(ICard card);
        void RemoveCard(ICard card);
    }
}
