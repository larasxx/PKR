using System;
using System.Windows;
using System.Windows.Controls;
using PKR.Models;
using PKR.Models; // Ensure correct namespace here

namespace PKR.View
{
    public partial class MainWindow : Window
    {
        private PokerGame _pokerGame;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Initialize PokerGame instance
            _pokerGame = new PokerGame
            {
                BigBlind = 50,
                SmallBlind = 25
            };

            // Add players to the game
            _pokerGame.Players.Add(new Player { Name = "Player 1", Chips = 1000 });
            _pokerGame.Players.Add(new Player { Name = "Player 2", Chips = 1000 });
            _pokerGame.Players.Add(new Player { Name = "Player 3", Chips = 1000 });

            SetInitialBlinds();
            StartNewHand();
        }

        private void SetInitialBlinds()
        {
            if (_pokerGame.Players.Count >= 2)
            {
                _pokerGame.Players[0].IsSmallBlind = true;
                _pokerGame.Players[1].IsBigBlind = true;
            }
            else if (_pokerGame.Players.Count == 1)
            {
                // In the unlikely case you have only one player, set only the small blind
                _pokerGame.Players[0].IsSmallBlind = true;
            }
        }


        private void StartNewHand()
        {
            _pokerGame.ResetDeck();
            _pokerGame.Pot = 0;

            foreach (var player in _pokerGame.Players)
            {
                player.ClearHand();
                player.IsFolded = false;
            }

            _pokerGame.DealHands();
            _pokerGame.CollectBlinds();
            UpdateUI();
        }

        private void UpdateUI()
        {
            PotTextBlock.Text = $"Pot: {_pokerGame.Pot}";

            // Update only as many players as are active in the game
            if (_pokerGame.Players.Count >= 1)
            {
                Player1ChipsTextBlock.Text = $"{_pokerGame.Players[0].Name} Chips: {_pokerGame.Players[0].Chips}";
            }
            else
            {
                Player1ChipsTextBlock.Text = "";  // Clear if not active
            }

            if (_pokerGame.Players.Count >= 2)
            {
                Player2ChipsTextBlock.Text = $"{_pokerGame.Players[1].Name} Chips: {_pokerGame.Players[1].Chips}";
            }
            else
            {
                Player2ChipsTextBlock.Text = "";
            }

            if (_pokerGame.Players.Count >= 3)
            {
                Player3ChipsTextBlock.Text = $"{_pokerGame.Players[2].Name} Chips: {_pokerGame.Players[2].Chips}";
            }
            else
            {
                Player3ChipsTextBlock.Text = "";
            }

            // Repeat similarly if you have text blocks for Player 4, Player 5, etc.
        }


        private void BetButton_Click(object sender, RoutedEventArgs e)
        {
            var currentPlayer = _pokerGame.Players[0];
            int betAmount = 10;
            _pokerGame.PlayerAction(currentPlayer, "bet", betAmount);
            UpdateUI();
        }

        private void PlayerCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlayerCountComboBox.SelectedItem is ComboBoxItem selectedItem && int.TryParse(selectedItem.Content.ToString(), out int playerCount))
            {
                InitializeGame(playerCount);
            }
        }

        private void InitializeGame(int playerCount)
        {
            _pokerGame = new PokerGame
            {
                BigBlind = int.Parse(BigBlindTextBox.Text),
                SmallBlind = int.Parse(SmallBlindTextBox.Text),
                Pot = int.Parse(StartingPotTextBox.Text),
            };

            // Add players
            _pokerGame.Players.Clear();
            for (int i = 1; i <= playerCount; i++)
            {
                _pokerGame.Players.Add(new Player { Name = $"Player {i}", Chips = 1000 });
            }

            SetInitialBlinds();
            StartNewHand();
            UpdateUI();

            // Set a timer for level adjustments
            int levelInterval = int.Parse(LevelIntervalTextBox.Text);
            StartLevelTimer(levelInterval);
        }

        private void StartLevelTimer(int intervalSeconds)
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(intervalSeconds);
            timer.Tick += (s, e) => AdjustBlinds();
            timer.Start();
        }

        private void AdjustBlinds()
        {
            _pokerGame.BigBlind += 10;  // Increment BB
            _pokerGame.SmallBlind += 5; // Increment SB
            UpdateUI();
        }



        private void FoldButton_Click(object sender, RoutedEventArgs e)
        {
            var currentPlayer = _pokerGame.Players[0];
            _pokerGame.PlayerAction(currentPlayer, "fold");
            UpdateUI();
        }


        private void SaveGameHistory_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Game history saved!", "Save Game History", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ViewGameHistory_Click(object sender, RoutedEventArgs e)
        {
            GameHistoryWindow historyWindow = new GameHistoryWindow();
            historyWindow.Show();
        }
    }
}
