namespace PlanningPoker.Server;

public interface IStore
{
    Task CreateParticipantAsync(Guid sessionId, string participantId, string name);
    Task<Guid> CreateSessionAsync(string title);
    Task DeleteParticipantAsync(Guid sessionId, string participantId);
    Task<bool> ExistsSessionAsync(Guid sessionId);
    Task<Session?> GetSessionAsync(Guid sessionId);
    Task IncrementParticipantStarsAsync(Guid sessionId, string participantId, int count = 1);
    Task UpdateAllParticipantPointsAsync(Guid sessionId, string points = "");
    Task UpdateParticipantNameAsync(Guid sessionId, string participantId, string name);
    Task UpdateParticipantPointsAsync(Guid sessionId, string participantId, string points);
    Task UpdateSessionStateAsync(Guid sessionId, State state);
    Task UpdateSessionTitleAsync(Guid sessionId, string title);
}