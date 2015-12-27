using System;
using System.Windows.Media;

namespace FoolGame.Bll.Card
{
    class Card : ICard
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }
        public ImageSource CurrentImage { get; set; }
        public ImageSource BackImage { get; set; }
        public ImageSource FrontImage { get; set; }

        private CardVisibilityState _visibilityState;
        public CardVisibilityState VisibilityState
        {
            get { return _visibilityState; }
            set
            {
                _visibilityState = value;
                CurrentImage = value == CardVisibilityState.Visible ? FrontImage : BackImage;
            }
        }
    }
}
