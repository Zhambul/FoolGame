﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FoolGame.Bll.Card
{

    public enum CardVisibilityState
    {
        Visible,
        NotVisible
    }
    public enum CardSuit
    {
        Heart,
        Diamond,
        Club,
        Spade
    }

    public enum CardValue
    {
        Six = 6,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
    public interface ICard
    {
        CardSuit Suit { get; set; }
        CardValue Value { get; set; }
        ImageSource CurrentImage { get; set; }
        ImageSource BackImage { get; }
        ImageSource FrontImage { get; }

        CardVisibilityState VisibilityState { get; set; }
    }
}