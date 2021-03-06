<br>
<h1 align = "center">
<b> BlackjackTeamProject </b>
</h1>

<p align = "center">
This application allows the user to play a game of BlackJack with up to four players against an AI operated dealer </p>
<p align = "center"> Created July 30th, 2021 </p>

<p align = "center">
 By Matthew, David, Chris, and Andrew
 </p>

--------------------

## 📖  Description

Team Project. This is our attempt to make a multiplayer BlackJack game with an AI operated dealer. David and Andrew worked on front end while Matthew and Chris bounced between front and backend. Players set up a local multiplayer for BlackJack, selecting the number of players, then select a number of rounds. Hitting 'start' will start the game with the set amount of players an an AI dealer. Players are trying to get to the score of 21, or as close as they can to it. Each player takes their turn either asking for another card (Hit me), or stopping and passing their turn to the next player (Stand).
Dealer will pull cards until their score is 17 or higher and stop, then the winner is decided based on the highest score. Hitting 'Next round' will begin the next round.
In the case of a tie, the tying players will receive half a point.
The cards are valued at their face values, except face cards, which are worth 10 points. Aces act the most different, they are either worth 1 point or 10. If the player with the Ace is under 22 points with the addition of the Ace, the Ace is worth 10 more points, otherwise the Ace is worth 1 point.
--------------------

## 🛠️ Technologies Used

This project uses the following technologies:

- C# v7.3.0
- .NET Core v3.1.0
- ASP .NET MVC
- ASP .NET Core Razor Pages
- Javascript
- JQuery
- Entity Framework Core

-------------------

<details>
<summary>The game</summary>

| Gameplay function | Input | Output |
| :------------- | :------------- | :------------- |
| Multiplayer: multiple players can play in the same instance | choose the amount of players you want from the dropdown menu | the players will be given cards and the game will start |
| Cards: hovering over the cards with your mouse will highlight the card to make it easier to read |  |  |
| Dealer: the AI player in the game | hit the "Stand" button to let the AI make it's play | It will pull cards until the cards reach a value of 17+ points |
| Betting: The player can bet a certain amount of chips on their current hand to |  |  |
|  |  |  |
|  |  |  |

</details>

-------------------

## 🐛 Known Bugs

| Error | Handled | Solution |
| :------------- | :------------- | :------------- |
|  |  | 
|  |  | 
|  |  | 
|  |  | 
|  |  | 

-------------------

## 🔧 Setup & Requirements

#### To run this project locally you will need:

- **ASP .NET Core :** You can check if you have .NET Core by running `dotnet --version` in the command line. If you do not have .NET Core please find more information and download [here](https://dotnet.microsoft.com/download/dotnet-core)
- **JQuery :**  
#### To Download:

Go to my GitHub repository here, [https://github.com/CommaderDavid/BlackJackTeamProject](https://github.com/CommaderDavid/BlackJackTeamProject), and click on the green 'Code' button to clone the repository, Open with GitHub Desktop OR Download the ZIP file

#### Running/viewing application:

1. Once you have opened the code in your preferred text editor you will need to navigate to the 'BlackJackTeamProject/BlackJackTeamProject' folder (`cd BlackJackTeamProject`) in the command line and run `dotnet run` or `dotnet watch run`.
2. At this point you should be able to click on the link to the local server's url path to view the compiled project. 

--------------------------

## 📫 Support and contact details

If you run into any problems or have any questions please contact me via [email](mailto:andrew.m.mickel@gmail.com).

---------------------------

## 📘 License

MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Copyright (c) 2021 Andrew Mickel, Chris Ramer, David Boedigheimer, and Matthew LeDoux.
