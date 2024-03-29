﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using FoolGame.Bll.Card;
using FoolGame.Bll.CardFabric;
using FoolGame.Bll.Vk;
using FoolGame.Uil.Window;

namespace FoolGame.Bll.Game
{
    class Game : IGame
    {
        public IPlayer UserPlayer { get; private set; }
        public IPlayer CompPlayer { get; private set; }
        private ICardIniter _cardIniter;
        private readonly IDeck _deck;
        private readonly IGameCallback _gameCallback;
        public ICardCollection TableCards { get; set; }
        private readonly ISharable _sharable;
        public Game(IPlayer userPlayer, IPlayer compPlayer, IDeck deck,ICardCollection tableCards, IGameCallback gameCallback, ISharable sharable)
        {
            UserPlayer = userPlayer;
            CompPlayer = compPlayer;
            _deck = deck;
            _gameCallback = gameCallback;
            _sharable = sharable;
            TableCards = tableCards;
            if (new Random().Next(2) == 1)
            {
                UserPlayer.GameRole = GameRole.Defender;
                CompPlayer.GameRole = GameRole.Attacker;
            }
            else
            {
                UserPlayer.GameRole = GameRole.Attacker;
                CompPlayer.GameRole = GameRole.Defender;
            }
            _gameCallback.OnRoleSwith(UserPlayer.GameRole == GameRole.Attacker);
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine(_deck.CardCollection.Count + ",");
            foreach (var tableCard in TableCards.Cards)
            {
                result.AppendLine(String.Format("{0},{1}",tableCard.Suit,tableCard.Value));
            }
            return result.ToString();
        }

        private void SwitchRoles()
        {
            UserPlayer.GameRole = UserPlayer.GameRole == GameRole.Attacker ? GameRole.Defender : GameRole.Attacker;
            CompPlayer.GameRole = CompPlayer.GameRole == GameRole.Attacker ? GameRole.Defender : GameRole.Attacker;
            _gameCallback.OnRoleSwith(UserPlayer.GameRole == GameRole.Attacker);
        }
        public void OnGameStarted()
        {
            GiveCards();
            if (CompPlayer.GameRole == GameRole.Attacker)
            {
                OnComputerMove(null);
            }
        }

        private void GiveCards(IPlayer player, bool userPlayer)
        {
            while (player.CardLimit > player.CardCollection.Count)
            {
                ICard card;
                if (_deck.CardCollection.Count == 0 && _deck.TrumpCard != null)
                {
                    card = _deck.TrumpCard;
                    _deck.TrumpCard = null;
                    _gameCallback.OnTrumpCardChosen();
                }
                else if (_deck.CardCollection.Count == 0)
                {
                    break;
                }
                else { 
                    card = _deck.GetNextCard();
                }
                if (userPlayer)
                {
                    card.CardAppearance = new CardOpenState();
                }
                player.AddCard(card);

            }
        }
        public void OnMovesEnded(bool switchRoles)
        {
            GiveCards();

            if (switchRoles)
            {
                SwitchRoles();
            }
            if (CompPlayer.GameRole == GameRole.Attacker)
            {
                OnComputerMove(null);
            }
        }

        private void GiveCards()
        {
            GiveCards(UserPlayer, true);
            GiveCards(CompPlayer, false);
        }

        public void OnPlayerMove(ICard card)
        {
            bool resume;
            if (UserPlayer.GameRole == GameRole.Attacker)
            {
                resume = UserPlayerAttacks(card);
            }
            else
            {
               resume = UserPlayerDefends(card);
            }
            if (!resume)
            {
                return;
            }
            CheckWin();
            _gameCallback.OnGetCardsButtonHidden();
            OnComputerMove(card);
        }

        private bool UserPlayerDefends(ICard card)
        {
            var tableCard = TableCards.Cards.Last();
            
            if (card.Suit == _deck.TrumpSuit && tableCard.Suit != _deck.TrumpSuit)
            {
                UserPlayer.CardCollection.RemoveCard(card);
                TableCards.Cards.Add(card);
            }
            else if (card.Suit != _deck.TrumpSuit && tableCard.Suit == _deck.TrumpSuit)
            {
                _gameCallback.OnGetCardsButtonVisible();
            }
            else
            {
                if (card.Suit == tableCard.Suit && card.Value > tableCard.Value)
                {
                    UserPlayer.CardCollection.RemoveCard(card);
                    TableCards.Cards.Add(card);
                }
                else
                {
                    _gameCallback.OnNotSuitableCard();
                    return false;
                }
            }
            return true;
        }

        private bool UserPlayerAttacks(ICard card)
        {
            if (CheckIsSuitableCard(card))
            {
                TableCards.AddCard(card);
                UserPlayer.RemoveCard(card);
            }
            else
            {
                _gameCallback.OnNotSuitableCard();
                return false;
            }
            return true;
        }

        private bool CheckIsSuitableCard(ICard card)
        {
            if (TableCards.Cards.Count == 0)
            {
                return true;
            }

            return TableCards.Cards.Any(tableCard => tableCard.Value == card.Value);
        }

        public void OnComputerMove(ICard card)
        {
            if (CompPlayer.GameRole == GameRole.Attacker)
            {
                CompPlayerAttacks(card);
            }
            else
            {
                CompPlayerDefends(card);
            }
            CheckWin();
        }

        public void CheckWin()
        {
            if (UserPlayer.CardCollection.Count == 0 && _deck.CardCollection.Count == 0)
            {
                if (MessageBox.Show("Поздравляю! Вы выйграли", "Поделиться") == MessageBoxResult.OK)
                {
                    VkPassword vkPassword = new VkPassword();
                    if (vkPassword.ShowDialog() == true)
                    {
                        _sharable.Share(vkPassword.Email,vkPassword.Password);
                    }
                }
            }
            else if (CompPlayer.CardCollection.Count == 0 && _deck.CardCollection.Count == 0)
            {
                MessageBox.Show("Вы проиграли");
            } 
        }

        public int Pass()
        {
            int passCards = 0;
            while (TableCards.Count > 0)
            {
                var card = TableCards.GetLastCard();
                TableCards.Cards.Remove(card);
                passCards++;
            }
            return passCards;
        }

        public void GetCardsFromTableToUser()
        {
            while (TableCards.Count > 0)
            {
                var card = TableCards.GetLastCard();
                TableCards.Cards.Remove(card);
                UserPlayer.CardCollection.AddCard(card);
            }
        }

        private void CompPlayerDefends(ICard card)
        {
            var suitableCards = GetSuitableCardsForDefend(card);
            if (suitableCards.Count == 0)
            {
                GetCardsFromTableForComp();
                OnMovesEnded(false);
                return;
            }
            var resultCard = GetBestCard(suitableCards);
            resultCard.CardAppearance = new CardOpenState();
            CompPlayer.CardCollection.RemoveCard(resultCard);
            TableCards.Cards.Add(resultCard);

            _gameCallback.OnPassButtonVisible();
        }

        private void GetCardsFromTableForComp()
        {
            while (TableCards.Cards.Count != 0)
            {
                var tableCard = TableCards.Cards.Last();
                tableCard.CardAppearance = _cardIniter.GetClosedAppearance();
                TableCards.RemoveCard(tableCard);
                CompPlayer.CardCollection.AddCard(tableCard);
            }
        }

        private ICard GetBestCard(List<ICard> suitableCards)
        {
            ICard resultCard = null;
            foreach (var card in suitableCards)
            {
                if (resultCard == null)
                {
                    resultCard = card;
                }
                else
                {
                    if (resultCard.Suit != _deck.TrumpSuit && card.Suit == _deck.TrumpSuit)
                    {
                        continue;
                    }
                    if (resultCard.Suit == _deck.TrumpSuit && card.Suit != _deck.TrumpSuit)
                    {
                        resultCard = card;
                    }
                    else if(resultCard.Value > card.Value)
                    {
                        resultCard = card;
                    }
                }
            }
            return resultCard;
        }

        private List<ICard> GetSuitableCardsForDefend(ICard card)
        {
            var result = new List<ICard>();
            foreach (var compCard in CompPlayer.CardCollection.Cards)
            {
                if (compCard.Suit == _deck.TrumpSuit)
                {
                    if (card.Suit == _deck.TrumpSuit)
                    {
                        if (compCard.Value > card.Value)
                        {
                            result.Add(compCard);
                        }
                    }
                    else
                    {
                        result.Add(compCard);
                    }
                }
                else if (card.Suit == _deck.TrumpSuit)
                {
                    continue;
                }

                if (compCard.Value > card.Value && compCard.Suit == card.Suit)
                {
                    result.Add(compCard);
                }
            }
            return result;
        }


        private void CompPlayerAttacks(ICard card)
        {
            ICard bestCard;

            if (card == null)
            {
                bestCard = GetBestCard(CompPlayer.CardCollection.Cards.ToList());
            }
            else
            {
                var suitableCardsFotAttack = GetSuitableCardsForAttack();
                if (suitableCardsFotAttack.Count == 0)
                {
                    _gameCallback.OnPassButtonVisible();
                    return;
                }
                bestCard = GetBestCard(suitableCardsFotAttack);
            }
            _gameCallback.OnGetCardsButtonVisible();
            bestCard.CardAppearance = new CardOpenState();
            CompPlayer.CardCollection.RemoveCard(bestCard);
            TableCards.Cards.Add(bestCard);
        }

        private List<ICard> GetSuitableCardsForAttack()
        {
            return (from compCard in CompPlayer.CardCollection.Cards 
                    from tableCard in TableCards.Cards 
                    where compCard.Value == tableCard.Value 
                    select compCard).ToList();
        }
    }
}
