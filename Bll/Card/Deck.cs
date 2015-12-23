using System;
using FoolGame.Bll.CardFabric;
using FoolGame.Bll.Game;

namespace FoolGame.Bll.Card
{
    class Deck : IDeck
    {
        private readonly IDeckChanged _gameWindow;
        public CardSuit TrumpSuit { get; private set; }
        public ICard TrumpCard { get; set; }
        public ICardSet CardSet { get; private set; }
        public ICardFabric CardFabric { get; private set; }
        public int CardLimit { get; private set; }

        public Deck(ICardSet cardSet, ICardFabric cardFabric, IDeckChanged gameWindow)
        {
            CardSet = cardSet;
            CardFabric = cardFabric;
            CardLimit = 36;
            _gameWindow = gameWindow;
            FullfillCardSet();
            TrumpSuit = GetRandomTrumpSuit();
            TrumpCard = GetRandomTrumpCard();
        }

        private ICard GetRandomTrumpCard()
        {
            while (true)
            {
                int randomIndex = new Random().Next(CardSet.Count - 1);
                var randomCard = CardSet.GetCardAt(randomIndex);
                if (randomCard.Suit == TrumpSuit)
                {
                    _gameWindow.OnTrumpCardSelected(randomCard);
                    CardSet.RemoveCard(randomCard);
                    return randomCard;
                }
            }
        }

        private CardSuit GetRandomTrumpSuit()
        {
            switch (new Random().Next(0,4))
            {
                case 0: return CardSuit.Club;
                case 1: return CardSuit.Diamond;
                case 2: return CardSuit.Heart;
                case 3: return CardSuit.Spade;
            }
            throw new Exception();
        }

        private void FullfillCardSet()
        {
            while (CardSet.Count != CardLimit)
            {
                CardSet.AddCard(CardFabric.GetCard());
            }
        }
     
        public ICard GetNextCard()
        {
            int index = new Random().Next(0,CardSet.Count+1);
            ICard card = CardSet.Cards[index];
            CardSet.RemoveCard(card);
            _gameWindow.OnDeckChanged(CardSet.Count);
            return card;
        }
    }
}
