namespace PlanningPoker.Server;

public class InMemoryStore : IStore
{
    static readonly Dictionary<Guid, Session> _sessions = [];

    public Task CreateParticipantAsync(Guid sessionId, string participantId, string name)
    {
        var session = _sessions[sessionId];
        
        _sessions[sessionId] = session with {
            Participants = [..session.Participants, new(participantId, name, "", 0) ]
        };

        return Task.CompletedTask;
    }

    public Task<Guid> CreateSessionAsync(string title)
    {
        Guid newGuid;

        do
        {
            newGuid = Guid.NewGuid();
        }
        while (_sessions.ContainsKey(newGuid));

        _sessions.Add(newGuid, new(title, [], State.Hidden));

        return Task.FromResult(newGuid);
    }

    public Task DeleteParticipantAsync(Guid sessionId, string participantId)
    {
        var session = _sessions[sessionId];
        
        _sessions[sessionId] = session with {
            Participants = session.Participants.Where(p => p.ParticipantId != participantId).ToArray()
        };

        return Task.CompletedTask;
    }

    public Task<bool> ExistsSessionAsync(Guid sessionId)
    {
        return Task.FromResult(_sessions.ContainsKey(sessionId));
    }

    public Task<Session?> GetSessionAsync(Guid sessionId)
    {
        return Task.FromResult(_sessions.TryGetValue(sessionId, out var session) ? session : null);
    }

    public Task IncrementParticipantStarsAsync(Guid sessionId, string participantId, int count = 1)
    {
        var session = _sessions[sessionId];
        
        _sessions[sessionId] = session with {
            Participants =
                session.Participants
                .Select(p =>
                    p.ParticipantId == participantId
                    ? p with { Stars = p.Stars + count }
                    : p
                )
                .ToArray()
        };

        return Task.CompletedTask;
    }

    public Task UpdateAllParticipantPointsAsync(Guid sessionId, string points = "")
    {
        var session = _sessions[sessionId];
        
        _sessions[sessionId] = session with {
            Participants = session.Participants.Select(p => p with { Points = points}).ToArray()
        };

        return Task.CompletedTask;
    }

    public Task UpdateParticipantNameAsync(Guid sessionId, string participantId, string name)
    {
        var session = _sessions[sessionId];
        
        _sessions[sessionId] = session with {
            Participants =
                session.Participants
                .Select(p =>
                    p.ParticipantId == participantId
                    ? p with { Name = name }
                    : p
                )
                .ToArray()
        };

        return Task.CompletedTask;
    }

    public Task UpdateParticipantPointsAsync(Guid sessionId, string participantId, string points)
    {
        var session = _sessions[sessionId];
        
        _sessions[sessionId] = session with {
            Participants =
                session.Participants
                .Select(p =>
                    p.ParticipantId == participantId
                    ? p with { Points = points }
                    : p
                )
                .ToArray()
        };

        return Task.CompletedTask;
    }

    public Task UpdateSessionStateAsync(Guid sessionId, State state)
    {
        var session = _sessions[sessionId];
        
        _sessions[sessionId] = session with {
            State = state
        };

        return Task.CompletedTask;
    }

    public Task UpdateSessionTitleAsync(Guid sessionId, string title)
    {
        var session = _sessions[sessionId];
        
        _sessions[sessionId] = session with {
            Title = title
        };

        return Task.CompletedTask;
    }
}