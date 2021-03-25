using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poker_Hand_Ranking.Models
{

    // This is just a quick and dirty way of generating a player and the hand
    public class PokerPlayer
    {
        public string PlayerName
        { get; set; }

        public string[] PlayerHand
        { get; set; }

        public PokerPlayer(string playerName)
        {
            // Set the player name
            PlayerName = playerName;

            // Create a deck of cards
            List<string> cardDeck = new List<string>
            {
                "2c",
                "3c",
                "4c",
                "5c",
                "6c",
                "7c",
                "8c",
                "9c",
                "10c",
                "Jc",
                "Qc",
                "Kc",
                "Ac",
                "2d",
                "3d",
                "4d",
                "5d",
                "6d",
                "7d",
                "8d",
                "9d",
                "10d",
                "Jd",
                "Qd",
                "Kd",
                "Ad",
                "2h",
                "3h",
                "4h",
                "5h",
                "6h",
                "7h",
                "8h",
                "9h",
                "10h",
                "Jh",
                "Qh",
                "Kh",
                "Ah",
                "2s",
                "3s",
                "4s",
                "5s",
                "6s",
                "7s",
                "8s",
                "9s",
                "10s",
                "Js",
                "Qs",
                "Ks",
                "As"
            };

            // "Shuffle" the deck
            // There are certainly better ways to do this, but this 
            // one is very easy and will suffice for this example
            cardDeck = cardDeck.OrderBy(i => Guid.NewGuid()).ToList();

            // Take the first five cards from the deck
            PlayerHand = new string[]{ cardDeck[0], cardDeck[1], cardDeck[2], cardDeck[3], cardDeck[4] };
        }
    }
    
}