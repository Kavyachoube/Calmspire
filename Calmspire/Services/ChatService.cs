using CalmSpire.Data;
using CalmSpire.Models;
using Microsoft.EntityFrameworkCore;

namespace CalmSpire.Services
{
    public class ChatService
    {
        private readonly CalmSpireDbContext _context;
        private readonly IConfiguration _configuration;

        public ChatService(CalmSpireDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> GetResponseAsync(string message)
        {
            // Mock AI responses for now - this can be replaced with actual AI integration
            var responses = new[]
            {
                "I understand you're going through a difficult time. Remember that it's okay to feel this way, and seeking support is a sign of strength.",
                "That sounds challenging. Have you tried any mindfulness exercises or breathing techniques? They can help manage stress and anxiety.",
                "Thank you for sharing that with me. How are you feeling right now? Is there anything specific you'd like to talk about?",
                "It's great that you're taking steps to care for your mental health. Small daily practices can make a big difference over time.",
                "I hear you. Sometimes just talking about our feelings can help us process them better. What's been on your mind lately?",
                "That's a positive step forward. Remember to be patient with yourself as you work through this journey of self-improvement.",
                "Have you considered keeping a gratitude journal? Focusing on positive aspects of your day, even small ones, can help shift your perspective.",
                "Stress can affect us in many ways. Are you getting enough sleep and taking care of your physical health as well?",
                "Thank you for trusting me with your thoughts. Remember that professional counselors are also available if you need additional support."
            };

            // Simple keyword-based response selection
            var lowerMessage = message.ToLower();

            if (lowerMessage.Contains("anxious") || lowerMessage.Contains("anxiety") || lowerMessage.Contains("worried"))
            {
                return "I understand you're feeling anxious. Try taking slow, deep breaths - inhale for 4 counts, hold for 4, exhale for 4. This can help calm your nervous system. Would you like to talk about what's causing these feelings?";
            }

            if (lowerMessage.Contains("sad") || lowerMessage.Contains("depressed") || lowerMessage.Contains("down"))
            {
                return "I'm sorry you're feeling this way. It's important to acknowledge these feelings rather than push them away. Have you been able to do any activities you usually enjoy lately? Even small steps can help.";
            }

            if (lowerMessage.Contains("stressed") || lowerMessage.Contains("overwhelmed"))
            {
                return "Stress can feel overwhelming, but you're taking a positive step by reaching out. Try breaking down your concerns into smaller, manageable parts. What's one small thing you could do today to reduce stress?";
            }

            if (lowerMessage.Contains("grateful") || lowerMessage.Contains("thankful") || lowerMessage.Contains("appreciation"))
            {
                return "It's wonderful that you're focusing on gratitude! Practicing gratitude regularly can improve mental well-being and help us notice positive moments even during difficult times.";
            }

            // Default response
            var random = new Random();
            return responses[random.Next(responses.Length)];
        }

        public async Task<ChatMessage> SaveChatMessageAsync(int userId, string message, string response)
        {
            var chatMessage = new ChatMessage
            {
                UserId = userId,
                Message = message,
                Response = response,
                CreatedAt = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            return chatMessage;
        }

        public async Task<List<ChatMessage>> GetChatHistoryAsync(int userId, int limit = 50)
        {
            return await _context.ChatMessages
                .Where(cm => cm.UserId == userId)
                .OrderByDescending(cm => cm.CreatedAt)
                .Take(limit)
                .OrderBy(cm => cm.CreatedAt)
                .ToListAsync();
        }
    }
}