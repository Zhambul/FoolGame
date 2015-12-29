using System;
using System.Collections.Generic;
using System.Windows.Media;
using FoolGame.Bll.Card;

namespace FoolGame.Bll.CardFabric
{
    class CardFabric : ICardFabric
    {
        private readonly ICardIniter _cardIniter;

        public CardCollection Cards { get; set; }
        private List<CardSuit> _suits;
        private List<CardValue> _values;
        private Random random;
        public CardFabric(ICardIniter cardIniter)
        {
            _cardIniter = cardIniter;
            Cards = new CardCollection();
            _suits = new List<CardSuit>();
            _values = new List<CardValue>();
            random = new Random();
            InitCards();
        }

        private void InitCards()
        {
            InitSuits();
            InitValues();
            foreach (var cardSuit in _suits)
            {
                foreach (var cardValue in _values)
                {
                    ICardAppearanceStrategy cardAppearance = new CardClosed();
//                    ImageSource backImage = _cardIniter.GetBackImageSource();
//                    ImageSource frontImage = _cardIniter.GetFrontImageSource(cardSuit, cardValue);

                    Cards.AddCard(new Card.Card()
                    {
                        Suit = cardSuit,
                        Value = cardValue,
                        CardAppearance = cardAppearance
                    });
                }
            }
        }

        private void InitValues()
        {
            _values.Add(CardValue.Six);
            _values.Add(CardValue.Seven);
            _values.Add(CardValue.Eight);
            _values.Add(CardValue.Nine);
            _values.Add(CardValue.Ten);
            _values.Add(CardValue.Jack);
            _values.Add(CardValue.Queen);
            _values.Add(CardValue.King);
            _values.Add(CardValue.Ace);
        }

        private void InitSuits()
        {
            _suits.Add(CardSuit.Club);
            _suits.Add(CardSuit.Diamond);
            _suits.Add(CardSuit.Heart);
            _suits.Add(CardSuit.Spade);
        }

        public ICard GetCard()
        {
            int generatedIndex = random.Next(Cards.Count - 1);
            var card = Cards.GetCardAt(generatedIndex);
            Cards.RemoveCardAt(generatedIndex);

            return card;
        }

        public override string ToString()
        {
            return String.Format("CardFabric:{0}", Cards);
        }
    }
}
