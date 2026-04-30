using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Chatbot_WPF
{
    public partial class MainWindow : Window
    {
        private readonly CyberBotLogic _bot = new CyberBotLogic();

        public MainWindow()
        {
            InitializeComponent();
            AddBotMessage("Hello! I am your Cybersecurity Awareness Bot.\n" +
                          "Ask me about passwords, phishing, malware, scams,\n" +
                          "Wi-Fi safety, 2FA, social media, or type 'help'.");
        }

        //  Event Handlers 

        private void SendBtn(object sender, RoutedEventArgs e) => SendMessage();

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

        //  Core Send Logic 

        private void SendMessage()
        {
            string text = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(text)) return;

            UserInput.Clear();
            AddUserMessage(text);

            string reply = _bot.GetBotResponse(text);
            AddBotMessage(reply);

            ScrollToBottom();
        }

        //  User Bubble (right-aligned, green filled)

        private void AddUserMessage(string text)
        {
            // Bubble
            var bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(0, 180, 50)),
                CornerRadius = new CornerRadius(16, 16, 2, 16),
                Padding = new Thickness(12, 8, 12, 8),
                MaxWidth = 500
            };

            bubble.Child = new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush(Color.FromRgb(10, 10, 10)),
                FontSize = 14,
                FontWeight = FontWeights.Medium,
                TextWrapping = TextWrapping.Wrap
            };

            // Row — right-aligned with left indent so it never fills full width
            var row = new DockPanel
            {
                Margin = new Thickness(80, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            bubble.HorizontalAlignment = HorizontalAlignment.Right;
            row.Children.Add(bubble);

            ChatPanel.Children.Add(row);
        }

        //  Bot Bubble (left-aligned, dark with green text + avatar) 

        private void AddBotMessage(string text)
        {
            // Avatar circle
            var avatar = new Border
            {
                Width = 34,
                Height = 34,
                CornerRadius = new CornerRadius(17),
                Background = new SolidColorBrush(Color.FromRgb(0, 255, 65)),
                Margin = new Thickness(0, 0, 8, 0),
                VerticalAlignment = VerticalAlignment.Bottom
            };

            avatar.Child = new TextBlock
            {
                Text = "C",
                Foreground = new SolidColorBrush(Color.FromRgb(10, 10, 10)),
                FontWeight = FontWeights.Bold,
                FontSize = 15,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Bubble
            var bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(22, 22, 22)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(45, 45, 45)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(16, 16, 16, 2),
                Padding = new Thickness(12, 8, 12, 8),
                MaxWidth = 500
            };

            bubble.Child = new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 65)),
                FontSize = 14,
                FontFamily = new FontFamily("Consolas"),
                TextWrapping = TextWrapping.Wrap,
                LineHeight = 22
            };

            // Row — left-aligned with right indent
            var row = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 5, 80, 5)
            };

            row.Children.Add(avatar);
            row.Children.Add(bubble);

            ChatPanel.Children.Add(row);
        }

        //  Scroll Helper 

        private void ScrollToBottom()
        {
            ChatScroller.UpdateLayout();
            ChatScroller.ScrollToBottom();
        }
    }
}