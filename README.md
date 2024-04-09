# FreePlanningPoker

This is Free Planning Poker, a free tool for software teams to do "planning poker" exercises to estimate the difficulty and length of development tasks. You can probably use it for other purposes if you need, too. It's always going to be free, without limits.

# Architecture

The client is a Blazor SPA, the server is ASP and they communicate exclusively over SignalR (websockets). The server uses Redis as a backplane for SignalR and to store active sessions - this allows the server to scale horizontally.

The deployed isntance is hosted on Railway, which hosts the server in Docker.
