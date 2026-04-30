using System;
using System.Collections.Generic;
using System.Linq;

namespace Chatbot_WPF
{
    public class CyberBotLogic
    {
        // ─── Enums ────────────────────────────────────────────────────────────
        private enum Topic
        {
            None, Password, Phishing, Malware, KidsSafety,
            Scam, WiFi, TwoFA, SocialMedia, Awareness, Quiz
        }

        private enum QuizState { Inactive, PasswordQuiz, ContinueQuiz }

        // ─── State ────────────────────────────────────────────────────────────
        private readonly List<string> _memory = new List<string>();
        private readonly Random _random = new Random();
        private Topic _lastTopic = Topic.None;
        private QuizState _quizState = QuizState.Inactive;

        // ─── Entry Point ──────────────────────────────────────────────────────
        public string GetBotResponse(string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message))
                    return "Please type something first.";

                message = message.ToLower().Trim();

                // Quiz answer interception (must check before general routing)
                if (_quizState != QuizState.Inactive)
                {
                    string quizResult = HandleQuizAnswer(message);
                    if (quizResult != null) return quizResult;
                }

                // Memory commands
                if (message.StartsWith("remember"))
                    return HandleRemember(message);

                if (message.Contains("recall") || message.Contains("what do you remember"))
                    return RecallMemory();

                // Route to topic handlers
                string sentiment = DetectSentiment(message);

                if (message.Contains("how are you"))
                    return "I am doing great!\nHow may I help you today?";

                if (message.Contains("hello") || message.Contains("hi"))
                    return HandleGreeting();

                if (message.Contains("help"))
                    return HandleHelp();

                if (message.Contains("quiz"))
                    return StartQuiz();

                if (message.Contains("yes") && _lastTopic != Topic.None)
                    return ContinueConversation();

                if (message.Contains("password"))
                    return sentiment + HandlePassword();

                if (message.Contains("phishing"))
                    return sentiment + HandlePhishing();

                if (message.Contains("malware") || message.Contains("virus"))
                    return sentiment + HandleMalware();

                if (message.Contains("kids safety") || message.Contains("child safety") || message.Contains("children"))
                    return sentiment + HandleKidsSafety();

                if (message.Contains("scam"))
                    return sentiment + HandleScam();

                if (message.Contains("wifi") || message.Contains("wi-fi"))
                    return sentiment + HandleWiFi();

                if (message.Contains("2fa") || message.Contains("two factor") || message.Contains("authentication"))
                    return sentiment + HandleTwoFA();

                if (message.Contains("social media"))
                    return sentiment + HandleSocialMedia();

                if (message.Contains("cybersecurity awareness") || message.Contains("cyber security awareness"))
                    return sentiment + HandleAwareness();

                return sentiment + HandleUnknown();
            }
            catch (Exception ex)
            {
                return "Oops, something went wrong.\nError details: " + ex.Message;
            }
        }

        // ─── Memory ───────────────────────────────────────────────────────────
        private string HandleRemember(string message)
        {
            string fact = message.Replace("remember", "").Trim();
            if (string.IsNullOrWhiteSpace(fact))
                return "Tell me what to remember. Example: remember I struggle with phishing.";

            _memory.Add(fact);
            return $"Got it! I will remember: {fact}\n{GetAutoTip()}";
        }

        private string RecallMemory()
        {
            if (_memory.Count == 0)
                return "I do not remember anything yet.\nTry saying: remember I want to learn about phishing.";

            return "Here is what I remember:\n" + string.Join("\n", _memory.Select(m => "• " + m));
        }

        // ─── Greeting & Help ──────────────────────────────────────────────────
        private string HandleGreeting()
        {
            return "Hello! I am your Cybersecurity Awareness Bot.\n" +
                   "You can ask me about passwords, phishing, malware, scams, Wi-Fi safety,\n" +
                   "2FA, social media safety, kids safety, or cybersecurity awareness.\n" +
                   "Type 'help' to see all topics.";
        }

        private string HandleHelp()
        {
            return "Here are topics you can ask me about:\n" +
                   "• Password safety\n" +
                   "• Phishing attacks\n" +
                   "• Malware and viruses\n" +
                   "• Online scams\n" +
                   "• Public Wi-Fi safety\n" +
                   "• Two-factor authentication (2FA)\n" +
                   "• Social media safety\n" +
                   "• Kids online safety\n" +
                   "• Cybersecurity awareness\n" +
                   "• Cybersecurity quiz\n\n" +
                   "You can also say: remember I want to learn about phishing";
        }

        // ─── Topic Handlers ───────────────────────────────────────────────────
        private string HandlePassword()
        {
            _lastTopic = Topic.Password;
            return "Great question! Passwords are your first line of defence.\n" +
                   "A strong password should be long, unique, and hard to guess.\n" +
                   "It should include uppercase letters, lowercase letters, numbers, and special characters.\n" +
                   "Example: BlueTiger@RunsFast2026\n\n" +
                   "Cyber joke: Using '123456' as a password is like locking your door and leaving the key outside.\n\n" +
                   "Question: Do you use the same password for more than one account?\n" +
                   GetAutoTip();
        }

        private string HandlePhishing()
        {
            _lastTopic = Topic.Phishing;
            return "Phishing is when criminals pretend to be trusted people or companies\n" +
                   "to trick you into clicking a link or sharing private information.\n" +
                   "Warning signs: urgent messages, strange links, spelling mistakes, requests for passwords.\n\n" +
                   "Cyber joke: If an email says 'Click now or your account will explode', your account is probably fine.\n\n" +
                   "Mini task: Before clicking a link, hover over it and check where it really goes.\n" +
                   GetAutoTip();
        }

        private string HandleMalware()
        {
            _lastTopic = Topic.Malware;
            return "Malware means malicious software designed to damage your device, steal data, or spy on you.\n" +
                   "Examples include viruses, spyware, ransomware, and trojans.\n" +
                   "Tip: Avoid unknown downloads and keep your antivirus software updated.\n" +
                   GetAutoTip();
        }

        private string HandleKidsSafety()
        {
            _lastTopic = Topic.KidsSafety;
            return "Kids Online Safety Rules:\n" +
                   "1. Do not share your address, phone number, school name, or passwords online.\n" +
                   "2. Tell a parent or guardian if something online makes you uncomfortable.\n" +
                   "3. Do not befriend strangers online.\n" +
                   "4. Ask permission before downloading apps or games.\n" +
                   "5. Remember: not everyone online is who they say they are.\n\n" +
                   "Cyber joke: A stranger online saying 'trust me' is exactly why we should not trust them.\n" +
                   GetAutoTip();
        }

        private string HandleScam()
        {
            _lastTopic = Topic.Scam;
            return "Online scams are tricks used to steal money or personal information.\n" +
                   "Examples: fake prizes, fake job offers, fake investments, fake banking messages.\n" +
                   "Rule: If it sounds too good to be true, investigate first.\n" +
                   GetAutoTip();
        }

        private string HandleWiFi()
        {
            _lastTopic = Topic.WiFi;
            return "Public Wi-Fi can be risky because attackers may spy on your connection.\n" +
                   "Avoid using public Wi-Fi for banking, shopping, or entering passwords.\n\n" +
                   "Cyber joke: Using public Wi-Fi for banking is like shouting your PIN in a taxi rank.\n" +
                   GetAutoTip();
        }

        private string HandleTwoFA()
        {
            _lastTopic = Topic.TwoFA;
            return "Two-Factor Authentication (2FA) adds an extra security step after your password.\n" +
                   "Even if someone steals your password, they still need the second code to log in.\n" +
                   "Tip: Enable 2FA on your email, banking, and social media accounts.\n" +
                   GetAutoTip();
        }

        private string HandleSocialMedia()
        {
            _lastTopic = Topic.SocialMedia;
            return "Social media safety is important because criminals can use your posts to learn about you.\n" +
                   "Avoid sharing your home address, ID number, school location, or daily routine.\n\n" +
                   "Cyber joke: Hackers love oversharing. Do not give them free snacks.\n" +
                   GetAutoTip();
        }

        private string HandleAwareness()
        {
            _lastTopic = Topic.Awareness;
            return "Cybersecurity awareness means understanding online threats and knowing how to stay safe.\n" +
                   "It covers phishing, malware, scams, ransomware, social engineering,\n" +
                   "safe passwords, and protecting personal information.\n" +
                   "The goal is to build safe habits so you can avoid cyber attacks before they happen.\n" +
                   GetAutoTip();
        }

        private string HandleUnknown()
        {
            return "That is interesting. I am still learning, but I can help you with cybersecurity awareness.\n" +
                   "Try asking:\n" +
                   "  'How do I create a strong password?'\n" +
                   "  'What is phishing?'\n" +
                   "  'How do I avoid online scams?'\n" +
                   "Or type 'help' to see all topics.\n" +
                   GetAutoTip();
        }

        // ─── Quiz ─────────────────────────────────────────────────────────────
        private string StartQuiz()
        {
            _lastTopic = Topic.Quiz;
            _quizState = QuizState.PasswordQuiz;
            return "Cybersecurity Quiz Time!\n\n" +
                   "Which password is strongest?\n" +
                   "A) achumile123\n" +
                   "B) Password2024\n" +
                   "C) Mango!River#Sky92\n\n" +
                   "Type A, B, or C.";
        }

        private string HandleQuizAnswer(string message)
        {
            // Only intercept single-letter answers while a quiz is active
            if (message != "a" && message != "b" && message != "c")
                return null;

            if (_quizState == QuizState.PasswordQuiz)
            {
                _quizState = QuizState.ContinueQuiz;

                if (message == "c")
                    return "Correct! C is the strongest because it is longer and uses\n" +
                           "uppercase letters, lowercase letters, numbers, and symbols.\n" +
                           "Your cybersecurity brain is loading nicely!\n\n" +
                           "Round 2 — which one is safer?\n" +
                           "A) MyName2005\n" +
                           "B) Summer2024\n" +
                           "C) River!Cloud#91Tiger\n\n" +
                           "Type A, B, or C.";

                return "Not quite! The strongest answer is C because it is longer and more complex.\n" +
                       "Hackers would guess A or B before their tea gets cold.\n\n" +
                       "Round 2 — which one is safer?\n" +
                       "A) MyName2005\n" +
                       "B) Summer2024\n" +
                       "C) River!Cloud#91Tiger\n\n" +
                       "Type A, B, or C.";
            }

            if (_quizState == QuizState.ContinueQuiz)
            {
                _quizState = QuizState.Inactive;
                _lastTopic = Topic.None;

                if (message == "c")
                    return "Excellent! C wins again — complexity and length are your best friends.\n" +
                           "Quiz complete! Type 'help' to explore more topics.\n" +
                           GetAutoTip();

                return "Almost! C is still the winner — length plus mixed characters beats a simple word.\n" +
                       "Quiz complete! Type 'help' to explore more topics.\n" +
                       GetAutoTip();
            }

            return null;
        }

        // ─── Conversation Continuation ────────────────────────────────────────
        private string ContinueConversation()
        {
            switch (_lastTopic)
            {
                case Topic.Password:
                    return "Let us continue with passwords.\n\n" +
                           "Best practices recap:\n" +
                           "1. Use a password manager to store unique passwords.\n" +
                           "2. Never reuse passwords across accounts.\n" +
                           "3. Change passwords immediately if a site is breached.\n" +
                           GetAutoTip();

                case Topic.Phishing:
                    return "Let us continue with phishing.\n\n" +
                           "Scenario: You receive an email saying your bank account will close\n" +
                           "unless you click a link immediately.\n\n" +
                           "Best action: Do not click the link. Open the bank app or website yourself\n" +
                           "and check your account safely.\n" +
                           GetAutoTip();

                case Topic.Malware:
                    return "Let us continue with malware.\n\n" +
                           "Best protection steps:\n" +
                           "1. Keep all apps and your OS updated.\n" +
                           "2. Use reputable antivirus software.\n" +
                           "3. Avoid downloading from unknown sources.\n" +
                           "4. Do not open suspicious email attachments.\n" +
                           GetAutoTip();

                case Topic.WiFi:
                    return "Let us continue with Wi-Fi safety.\n\n" +
                           "If you must use public Wi-Fi:\n" +
                           "1. Use a VPN to encrypt your traffic.\n" +
                           "2. Stick to HTTPS websites only.\n" +
                           "3. Turn off auto-connect for open networks.\n" +
                           GetAutoTip();

                case Topic.TwoFA:
                    return "Let us continue with 2FA.\n\n" +
                           "Best 2FA methods (most secure first):\n" +
                           "1. Hardware security key (e.g. YubiKey)\n" +
                           "2. Authenticator app (e.g. Google Authenticator)\n" +
                           "3. SMS code (better than nothing, but least secure)\n" +
                           GetAutoTip();

                default:
                    return "Sure! Which topic would you like to explore further?\n" +
                           "Passwords, phishing, malware, scams, Wi-Fi, 2FA, or social media?";
            }
        }

        // ─── Helpers ──────────────────────────────────────────────────────────
        private string DetectSentiment(string message)
        {
            if (message.Contains("scared") || message.Contains("worried") || message.Contains("afraid"))
                return "Do not worry, I will help you step by step.\n";

            if (message.Contains("happy") || message.Contains("great") || message.Contains("good"))
                return "Nice! I like the positive energy.\n";

            if (message.Contains("confused") || message.Contains("don't understand") || message.Contains("dont understand"))
                return "No stress, I will explain it simply.\n";

            return "";
        }

        private string GetAutoTip()
        {
            string[] tips =
            {
                "Auto tip: Think before you click. Suspicious links are not your friends.",
                "Auto tip: Update your apps regularly. Updates patch security holes.",
                "Auto tip: Never share OTP codes — not even with someone claiming to be from the bank.",
                "Auto tip: Back up your important files. Future you will be grateful.",
                "Auto tip: Use a different password for every account.",
                "Auto tip: Check website addresses carefully before entering personal information.",
                "Auto tip: Lock your devices with a PIN or biometrics when not in use.",
                "Auto tip: Review app permissions. Does that flashlight app really need your contacts?"
            };

            return tips[_random.Next(tips.Length)];

        }
    }
}