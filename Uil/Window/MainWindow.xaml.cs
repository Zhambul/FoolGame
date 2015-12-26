using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FoolGame.Annotations;
using FoolGame.Bll;
using FoolGame.Bll.Card;
using FoolGame.Bll.CardFabric;
using FoolGame.Bll.Game;
using FoolGame.Bll.Vk;
using VkNet;
using VkNet.Categories;
using VkNet.Enums.Filters;
using VkNet.Exception;

namespace FoolGame.Uil.Window
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window, INotifyPropertyChanged, IDeckChanged, IGameCallback
    {
        #region vars
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

        private Visibility _passButtonVisibility;
        public Visibility PassButtonVisibility
        {
            get { return _passButtonVisibility; }
            set
            {
                _passButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _getCardsButtonVisibility;
        public Visibility GetCardsButtonVisibility
        {
            get { return _getCardsButtonVisibility; }
            set
            {
                _getCardsButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private string _passedCardsCountString;

        public string PassedCardsCountString
        {
            get { return _passedCardsCountString; }
            set
            {
                _passedCardsCountString = value;
                OnPropertyChanged();
            }
        }

        private string _roleText;
        public String RoleText
        {
            get { return _roleText; }
            set
            {
                _roleText = value; 
                OnPropertyChanged();
            }
        }

        private int _passedCardsCountInt;

        private bool _isDraging;
        private IGame _game;
        private ICard _currentCard;
        IPlayer _userPlayer;
        IPlayer _opponentPlayer;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            InitGame();
            PassButtonVisibility = Visibility.Hidden;
            GetCardsButtonVisibility = Visibility.Hidden;
            _game.OnGameStarted();
        }

        private void InitGame()
        {
            IDeck deck = new Deck(new CardCollection(), new CardFabric(new CardIniter()), this);
            _userPlayer = new Player(new CardCollection());
            _opponentPlayer = new Player(new CardCollection());
            _game = new Game(_userPlayer,_opponentPlayer,deck,new CardCollection(),this, new VkHelper());
            
            OpponentCards = _opponentPlayer.CardCollection.Cards;
            Cards = _userPlayer.CardCollection.Cards;
            TableCards = _game.TableCards.Cards;
        }

        public void OnDeckChanged(int countOfCardsInDeck)
        {
            DeckRemainingCards = "Карт в колоде "+ Environment.NewLine + countOfCardsInDeck;
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
                if (_currentCard == null)
                {
                    return;
                }
                _game.OnPlayerMove(_currentCard);
                _currentCard = null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnNotSuitableCard()
        {
            MessageBox.Show("Выбере другую карту");
        }

        public void OnPassButtonVisible()
        {
            _game.CheckWin();
            PassButtonVisibility = Visibility.Visible;
        }

        public void OnGetCardsButtonVisible()
        {
            _game.CheckWin();
            GetCardsButtonVisibility = Visibility.Visible;
        }

        public void OnRoleSwith(bool isAttacking)
        {
            RoleText = isAttacking ? "Вы атакуете" : "Вы защищаетесь";
        }

        public void OnGetCardsButtonHidden()
        {
            GetCardsButtonVisibility = Visibility.Hidden;
        }

        public void OnTrumpCardChosen()
        {
            TrumpCard = null;
        }

        private void PassButton_Click(object sender, RoutedEventArgs e)
        {
            _passedCardsCountInt += _game.Pass();
            PassedCardsCountString = "Карт отбито" + Environment.NewLine + _passedCardsCountInt;
            PassButtonVisibility = Visibility.Hidden;
            _game.OnMovesEnded(true);
        }

        private void GetCardsButton_Click(object sender, RoutedEventArgs e)
        {
            _game.GetCardsFromTableToUser();
            GetCardsButtonVisibility = Visibility.Hidden;
            _game.OnMovesEnded(false);
        }

       
    }
}
