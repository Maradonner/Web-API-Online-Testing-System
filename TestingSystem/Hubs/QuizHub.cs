using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace TestingSystem.Hubs;

public class QuizHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var role = Context.User.FindFirst(ClaimTypes.Role)?.Value;
        if (role != null) await Groups.AddToGroupAsync(Context.ConnectionId, role);
        await base.OnConnectedAsync();
    }

    public async Task SendQuizNotification(int quizId, string quizTitle, string userName)
    {
        await Clients.All.SendAsync("QuizCreated", quizId, quizTitle, userName);
    }
}