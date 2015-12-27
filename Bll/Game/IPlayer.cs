using FoolGame.Bll.Card;

namespace FoolGame.Bll.Game
{
    public enum GameRole
    {
        Attacker,
        Defender
    }

    public interface IPlayer
    {
        ICardCollection CardCollection { get; }
        int CardLimit { get; }
        GameRole GameRole { get; set; }
        void AddCard(ICard card);
        void RemoveCard(ICard card);
    }
}
