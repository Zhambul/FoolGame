﻿using System;
using System.Windows.Media;

namespace FoolGame.Bll.Card
{
    public class Card : ICard
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }
        public ImageSource CurrentImage { get; set; }

        private ICardAppearanceStrategy _cardAppearance;
        public ICardAppearanceStrategy CardAppearance
        {
            get { return _cardAppearance; }
            set
            {
                _cardAppearance = value;
                CurrentImage = value.GetAppearance(this);
            }
        }
    }
}
