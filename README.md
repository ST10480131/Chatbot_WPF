# Chatbot_WPF

## Project Description

CyberSecurity Awareness Chatbot is a WPF desktop application built using C#. The purpose of the application is to teach users about basic cybersecurity topics through a simple chat interface.

The chatbot can answer questions about passwords, phishing, malware, scams, Wi-Fi safety, two-factor authentication, social media safety, kids online safety, and general cybersecurity awareness. It also includes a small quiz feature and memory commands where the bot can remember simple user statements.

## Features

- Modern WPF chat interface
- User and bot message bubbles
- Send button and Enter key support
- Cybersecurity topic responses
- Password safety tips
- Phishing awareness
- Malware and virus explanations
- Scam prevention advice
- Public Wi-Fi safety tips
- Two-factor authentication guidance
- Social media safety advice
- Kids online safety rules
- Cybersecurity quiz
- Simple memory feature
- Automatic cybersecurity tips

## Technologies Used

- C#
- WPF
- XAML
- .NET
- Visual Studio

## How the Application Works

The application starts by displaying a welcome message from the Cybersecurity Awareness Bot. The user types a message into the input box and either clicks the Send button or presses Enter.

The message is sent to the `CyberBotLogic` class, which checks the user input and returns a suitable cybersecurity response. The response is then displayed in the chat window as a bot message.

The main window handles the user interface, while the chatbot logic is handled separately in the `CyberBotLogic` class. This makes the project easier to manage and understand. The bot logic includes topics such as passwords, phishing, malware, scams, Wi-Fi, 2FA, social media safety, awareness, memory, and quizzes.

## Main Files

### MainWindow.xaml

This file contains the design of the application window. It includes:

- The application title
- The chat display area
- A scroll viewer
- A stack panel for chat bubbles
- A text box for user input
- A send button

### MainWindow.xaml.cs

This file contains the code behind the interface. It handles:

- Sending messages
- Reading user input
- Displaying user messages
- Displaying bot messages
- Scrolling to the latest message

### CyberBotLogic.cs

This file contains the chatbot responses and logic. It decides how the bot should respond based on what the user types.

## Future Updates

The CyberSecurity Awareness Chatbot can be improved with the following features in the future:

### 1. Voice Interaction
- Add speech recognition so users can talk to the chatbot.
- Use text-to-speech so the bot can respond with audio.

### 2. Advanced AI Responses
- Improve responses using AI/NLP instead of keyword matching.
- Make conversations more natural and human-like.

  ### 3. Improved UI/UX
- Add themes (dark/light mode switch).
- Add animations for chat bubbles.
- Display timestamps for messages.

  ### 4. Notifications & Alerts
- Provide real-time cybersecurity alerts.
- Notify users about latest threats or scams.

  ### 5. Multi-language Support
- Support different languages (e.g., English, isiXhosa, isiZulu).
- Make the chatbot accessible to more users.

## Example Questions to Ask

You can ask the chatbot questions like:

- What is phishing?
- How do I create a strong password?
- What is malware?
- How do I avoid scams?
- Is public Wi-Fi safe?
- What is 2FA?
- How do I stay safe on social media?
- What is cybersecurity awareness?
- Start quiz
- Help

## How to Run the Project

1. Open the project in Microsoft Visual Studio.
2. Make sure the project is a WPF Application.
3. Check that `MainWindow.xaml`, `MainWindow.xaml.cs`, and `CyberBotLogic.cs` are included in the project.
4. Build the project.
5. Click Start to run the application.

 ## Conclusion

This project provides a strong foundation for a cybersecurity awareness tool, and future updates can transform it into a fully intelligent, interactive, and scalable application.

## Author

Created as a Cybersecurity Awareness WPF Chatbot project.
