using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FoolGame.Annotations;
using FoolGame.Bll;
using FoolGame.Bll.Card;
using FoolGame.Bll.CardFabric;
using FoolGame.Bll.Game;

namespace FoolGame.Uil.Window
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window, INotifyPropertyChanged, IDeckChanged
    {
        private ObservableCollection<ICard> _opponentCards;
        public ObservableCollection<ICard> OpponentCards
        {
            get { return _opponentCards; }
            set
            {
                _opponentCards = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ICard> _tableCards;
        public ObservableCollection<ICard> TableCards
        {
            get { return _tableCards; }
            set
            {
                _tableCards = value;
                OnPropertyChanged();
            }
        }

        private string _deckRemainingCards;
        public String DeckRemainingCards
        {
            get { return _deckRemainingCards; }
            set
            {
                _deckRemainingCards = value; 
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ICard> _cards;
        public ObservableCollection<ICard> Cards
        {
            get { return _cards; }
            set
            {
                _cards = value; 
                OnPropertyChanged();
            }
        }

        private ImageSource _trumpCard;
        public ImageSource TrumpCard
        {
            get { return _trumpCard; }
            set
            {
                _trumpCard = value; 
                OnPropertyChanged();
            }
        }

        private bool _isDraging;
        private IGame _game;
        private ICard _currentCard;
        IPlayer _userPlayer;
        IPlayer _opponentPlayer;

       
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            TableCards = new ObservableCollection<ICard>();
            InitGame();
            _game.OnGameStarted();
        }

        private void InitGame()
        {
            IDeck deck = new Deck(new CardSet(), new CardFabric(new CardIniter()), this);
            _userPlayer = new Player(new CardSet());
            _opponentPlayer = new Player(new CardSet());
            _game = new Game(_userPlayer,_opponentPlayer,deck);

            OpponentCards = _opponentPlayer.CardSet.Cards;
            Cards = _userPlayer.CardSet.Cards;

        }

        public void OnDeckChanged(int countOfCardsInDeck)
        {
            DeckRemainingCards = countOfCardsInDeck.ToString();
        }

        public void OnTrumpCardSelected(ICard trumpCard)
        {
            TrumpCard = trumpCard.FrontImage;
        }

        private void Card_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDraging && e.LeftButton == MouseButtonState.Pressed)
            {
                var image = (Image)sender;
                _currentCard = (ICard) image.DataContext;
                DragDrop.DoDragDrop(UserListView, _currentCard, DragDropEffects.None);
            }
        }

        private void Card_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _isDraging = true;
        }

        private void Card_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDraging = false;
        }

        private void Table_OnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (_isDraging && mouseEventArgs.LeftButton == MouseButtonState.Released)
            {
                _isDraging = false;
                _userPlayer.RemoveCard(_currentCard);
                Cards.Remove(_currentCard);
                TableCards.Add(_currentCard);
                _currentCard = null;

                if(_userPlayer.CardSet.Count==3)
                _game.OnMovesEnded();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
