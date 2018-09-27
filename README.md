# Keesz

Keesz is an online game played agains 3 friends. It's a parcheesi like game, but
in stead of dice, you use cards. Also, you play in teams (2 against 2) in stead
of individual.

**For technical rant about how to get the project running, scroll down**

## But why?

This project is written to get familiar with Microservices. It uses IdentityServer4
and a lot of helpers from the eShopOnContainers demo project. The individual services
run in docker containers. Optionally you can switch messaging between RabbitMQ and
Azure Service Bus.

IdentityServer is configured to use SQL Server as a data repository, all other services
use MongoDB.

The services are tied together using a Gateway API, which is consumed by the client
(Angular 6 SPA).

## The game

Once again, the game is quite similar to Parcheesi. There is a fixed amount of four players.
Opposite players play as a team. Goal is to bring all eight pawns to their home position.
The first team having all pawns on the home position wins. Each player is only allowed to
move his own pawns, until all his four pawns are in the home position. Only then the player
is allowed to also move his teammate's pawns.

### How to start

Place all four pawns on the start position. These are marked in the corner of the board,
identified by the player's color. A deck of cars is shuffled. The dealer deals 1 card at
a time for each player, untill all players have 5 cards. The first player next to the
dealer will begin his turn.

Once all players have played all their cards, the dealer will draw new cards from the
remainder of the deck in the same way. One card at a time for each player, but now until
all players have four cards. Again, the first player next to the dealer starts his turn.

This last step repeats one more time, which means all 52 cards in the deck are used.
Now the dealer position will advance, so the next player becomes the dealer and the
process will start all over again, dealing a first round of 5 cards.

This continues until on of the teams is the winner.

### Playing cards

Each player plays one of the drawn cards in turn. Once a card is played (and the
corresponding action performed), the turn will advance to the next player. In case
a player is not able to play a card, the player must dispose all cards and wait for
the dealer to draw new cards.

### Special rules

You can hit a different player's pawn by moving the exact amount of steps so you
end up on the position occupied by a different pawn. Players can't be hit in one
of the home positions, or on the entry position with protected state. Once a pawn
it hit, it must be placed back to one of it's start positions. Once that pawn
enters the board, it will (again) receive the protected state (on the entry spot)
until it leaves that spot.

There are four home positions on the board, marked in the same color as the player's
color. Unlike parcheesi, players are not allowed to pass (jump over) pawns in one of
the home positions. It's wise to think about how you organize pawns in the home
position to prevent having to queue pawns because one pawns is blocking entry.

If you play an Ace or King, you are allowed to put a pawn on the entry position (one
single marked spot on the playing field, identified with the same color as the player).
If you place a pawn on the entry position, it's protected. This means, that the pawn
can't but hit. It's even prohibited to pass (jump over) the pawn, for all other players
(even your teammate). It may be an effective hazzard for players to leave your pawn
on the entry position, but you may also bother your teammate. If the pawn has left
the entry position and somehow ends up on the same position (for example by moving
backwards by playing a 4 card) of the entry position, it's not protected anymore.

It is allows to move a pawn 4 steps back positioning it immidiately in front of the
home positions, only when the pawn is in a protected state. Once the pawn has left
the entry position, this privilege is lost.

Once a pawn has entered either one of the home positions, it can only move forward.

### Cards

If it's your turn, you play one of the cards you have at hand. Note that if you can
play a card, you MUST play a card, even if it's not in your advantage. Again, if you
can't play a card (for example when all your playable cards are stuck behind a
protected pawn) you must dispose all your cards at hand.

|  Card | Meaning                                           |
| ----: | :------------------------------------------------ |
|   Ace | Enter the board, or move a pawn 1 position        |
|  King | Enter the board                                   |
| Queen | Move a pawn forward with 12 positions             |
|  Jack | Swap two pawns (not of the same player)           |
|   Ten | Move a pawn forward with 10 positions             |
|  Nine | Move a pawn forward with 9 positions              |
| Eight | Move a pawn forward with 8 positions              |
| Seven | Move one or two pawns with a total of 7 positions |
|   Six | Move a pawn forward with 6 positions              |
|  Five | Move a pawn forward with 5 positions              |
|  Four | Move a pawn backwards with 4 positions            |
| Three | Move a pawn forward with 3 positions              |
|   Two | Move a pawn forward with 2 positions              |

## Technical rant

This project is written using the microservices architecture. It has a gateway API
responsible for communicating witht he microservices, and a client project (Angular)
that's responsible for front-end shizzle.
Next to the microservices is an IdentityServer4 service, for authentication and
authorisation purpose. The identity server relies on a MS SQL Server, all other services
use MongoDB as a data repository. Mongo is running in a container as well. Last but not
least is a container running RabbitMQ for command and event messaging. The SQL Server
is not running in a container.

### Security & Certificates and stuff

In order to run Keesz, you need to add 5 hostnames to your hostfile and point them to
127.0.0.1. The hostnames are www.keesz.int, api.keesz.int, connect.keesz.int, game.keesz.int
and identity.keesz.int.
You can generate a development certificate for these hostnames using the generate-certificate.ps1
powershell script (run elevated). Once succeeded you will receive a confirmation message with a
thumbprint. Copy and past the thumbprint into the commented lines of code in the powershell script
(and change the password) to export a certificate file.

### Identity server - HexMaster.Keesz.Identity

To configure the identity server, you need to provide a sql server connection string.
The database must exist and the user must have enough permissions to run migrations.

```
"ConnectionStrings": {
    "DefaultConnection": "DATA SOURCE={server-name-or-ip};Initial Catalog={database-name};User ID={username};Password={password};"
  }
```

### Connect service - HexMaster.Keesz.Connect

This project (along with all other microservices projects) require additional configuration
which are stored in the user secrets (right click a project in visual studio, choose user secrets)

```
{
  "Kestrel:Certificates:Development:Password": "password-for-dev-certificate",
  "ApplicationSettings:MongoDbConnectionString": "mongodb://username:password@mongo",
  "ApplicationSettings:IdentityServerUrl": "http://url-to-identity-server",
  "ApplicationSettings:EventBus:SubscriptionClientName": "event-bus-client-name (free to define)",
  "ApplicationSettings:EventBus:EventBusUserName": "rabbitmq",
  "ApplicationSettings:EventBus:EventBusRetryCount": "5",
  "ApplicationSettings:EventBus:EventBusPassword": "rabbitmq",
  "ApplicationSettings:EventBus:EventBusConnection": "rabbitmq",
  "ApplicationSettings:EventBus:AzureServiceBusEnabled": "False"
}
```
