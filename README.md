# Console Ludo Board Game

![ludo.gif](ludo.gif)

Console Ludo Board game is a project made during our vocational studies to .NET Developer. The project is a part of the Course Data Access in .NET and were made i the timespan 29 March - 13th of April. Focus of the project were to apply SOLID principles in c# as well as implementing a SQL database with Entity Framework.

Project authors:

Albin Alm -> [github repo](https://github.com/albinalm)

Robin Axelsson -> [github repo](https://github.com/robinaxelsson)

Kristian Jimmefors -> [github repo](https://github.com/Kristianjimmefors)

Robin Ferm -> [github repo](https://github.com/Robin-Ferm)

---

## Main Specifications

To create a Console based Ludo game where you can save, load and create new games (in a database). An AI player can be implemented to be a part of the game.

## User Stories

- As a user I want my pawns to move according to my dice throw
- As a user I want to push another players pawns from the board
- As a user I want to take out two pawns if I get a six
- As a user I want everyone to be limited to the the rules of the game (no cheating allowed)
- As a user I want to be able to play solo against AI-players.
- As a user I want to be able to save and load games.

## Notable class files

- Stephan.cs - handles our AI.
- DatabaseManagement.cs - Static class that serves as our database repository.
- Board.cs - Static class with all the important in-game LINQ-queries for player, board and pawns.
- Pawn.cs - Represents a pawn, holds the important 'Move' function.
- StephanLogs.cs - a Logg class to track Stephans "thought traces".
- RiggedDice.cs - A class used for unit testing, has the same interface as the default dice.
- GamePlay.cs - Game master of the game, tracks whos turn it is and if you are allowed to throw again or take out two pawns.
- GameBuilder.cs - A builder class that configures a GamePlay object.
- InfoDisplay.cs - Manages the game comments on screen. 

## End Result

A graphic console based Ludo with full support for multi player and single player with Bots (Stephans). We also managed to implement the integration with a SQL database and Entity Framework to save and load a game.
