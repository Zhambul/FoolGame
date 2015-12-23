using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FoolGame.Bll.Card;

namespace FoolGame.Bll.Game
{
    class Game : IGame
    {
        public IPlayer UserPlayer { get; private set; }
        public IPlayer CompPlayer { get; private set; }

        private readonly IDeck _deck;
        private readonly IGameCallback _gameCallback;
        public CardSet TableCards { get; set; }
        public Game(IPlayer userPlayer, IPlayer compPlayer, IDeck deck, IGameCallback gameCallback)
        {
            UserPlayer = userPlayer;
            CompPlayer = compPlayer;
            _deck = deck;
            _gameCallback = gameCallback;
            TableCards = new CardSet();
            UserPlayer.GameRole = GameRole.Defender;
            CompPlayer.GameRole = GameRole.Attacker;
            _gameCallback.OnRoleSwith(UserPlayer.GameRole == GameRole.Attacker);
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
            while (player.CardLimit > player.CardSet.Count && _deck.CardSet.Count != 0)
            {
                var card = _deck.GetNextCard();
                if (userPlayer)
                {
                    card.VisibilityState = CardVisibilityState.Visible;
                }
                player.AddCard(card);
            }
        }

        public void OnMovesEnded(bool switchRoles)
        {
            _gameCallback.OnGetCardsButtonHidden();
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
            OnComputerMove(card);
        }

        private bool UserPlayerDefends(ICard card)
        {
            if (card == null)
            {
                return false;
            }
            var tableCard = TableCards.Cards.Last();
            
            if (card.Suit == _deck.TrumpSuit && tableCard.Suit != _deck.TrumpSuit)
            {
                UserPlayer.CardSet.RemoveCard(card);
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
                    UserPlayer.CardSet.RemoveCard(card);
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
            if (card == null)
            {
                throw new Exception();
            }
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
                _gameCallback.OnGetCardsButtonVisible();
            }
            else
            {
                CompPlayerDefends(card);
            }

        }

        public void CheckWin()
        {
            if (UserPlayer.CardSet.Count == 0)
            {
                MessageBox.Show("Поздравляю! Вы выйграли");
            }
            else if (CompPlayer.CardSet.Count == 0)
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
                UserPlayer.CardSet.AddCard(card);
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
            resultCard.VisibilityState = CardVisibilityState.Visible;
            CompPlayer.CardSet.RemoveCard(resultCard);
            TableCards.Cards.Add(resultCard);

            _gameCallback.OnPassButtonVisible();
        }

        private void GetCardsFromTableForComp()
        {
            while (TableCards.Cards.Count != 0)
            {
                var tableCard = TableCards.Cards.Last();
                tableCard.VisibilityState = CardVisibilityState.NotVisible;
                TableCards.RemoveCard(tableCard);
                CompPlayer.CardSet.AddCard(tableCard);
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
            foreach (var compCard in CompPlayer.CardSet.Cards)
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
                bestCard = GetBestCard(CompPlayer.CardSet.Cards.ToList());
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
            bestCard.VisibilityState = CardVisibilityState.Visible;
            CompPlayer.CardSet.RemoveCard(bestCard);
            TableCards.Cards.Add(bestCard);
        }

        private List<ICard> GetSuitableCardsForAttack()
        {
            return (from compCard in CompPlayer.CardSet.Cards 
                    from tableCard in TableCards.Cards 
                    where compCard.Value == tableCard.Value 
                    select compCard).ToList();
        }
    }
}
