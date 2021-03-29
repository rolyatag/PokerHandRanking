using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Poker_Hand_Ranking.Models
{
    // A poker hand is composed on five card in this exercise
    // The cards are sent in through an array and assumed to be
    // two or three characters each - first one (or two for the 10) is the card
    // and the last one is the suit for that card so 3d or 10c for the Three of Diamonds
    // or the Ten of Clubs, respectively
    // The poker hand will also have a hand rank indicating the type of hand it is
    // In addition, for the purposes of tie breaking, there will be two additional values
    // indicating the first or highest value card and the second value card
    // For example, if a hand is ranked as Two Pairs, it will get a value of as such based on the 
    // HandValues enum. Since two pairs has two pairs of cards, the primary card will be set
    // to higher card value and the secondary to the lower card value
    // Not all ranks have these value. Also, we're not setting a teriary value for the
    // possibility of a two pair tie right now

    public class PokerHand
    {
        // Fields
        private string[] pokerHand;
        private List<Cards> pokerHandCards;
        private Suits pokerHandSuit;
        private HandRanks pokerHandRank;
        private Cards pokerHandPrimaryCard;
        private Cards pokerHandSecondaryCard;

        // Properties
        public List<Cards> PokerHandCards
        { get { return pokerHandCards; }}

        public HandRanks PokerHandRank
        { get { return pokerHandRank; } }

        public Cards PokerHandPrimaryCard
        { get { return pokerHandPrimaryCard; } }

        public Cards PokerHandSecondaryCard
        { get { return pokerHandSecondaryCard; } }

        public PokerHand(string[] currentPokerHand)
        {
            // The expectation is that the class is being 
            // initalized with an array of five cards
            // There really should be more error handling, but this
            // is just supposed to be a quick exercise

            pokerHand = currentPokerHand;
            pokerHandSuit = Suits.Unknown;
            pokerHandRank = HandRanks.Unknown;
            pokerHandPrimaryCard = Cards.Unknown;
            pokerHandSecondaryCard = Cards.Unknown;

            // Now that the poker hand is set we are going
            // to set first the list of cards and the suit            
            SetPokerHandCardsAndSuit();

            // Now we set the rank and card values for primary and secondary cards
            SetPokerHandRankAndValues();
        }

        public override string ToString()
        {
            // This was created to get the results out to the page relatively
            // quickly. Again, this was only done an quick programming exercise.
            StringBuilder printableHand = new StringBuilder();

            printableHand.Append("<h4>You were dealt the following cards: ");

            string separator = "";

            // Need to iterate through the hand and generate a string for output of the hand
            foreach (string Card in pokerHand)
            {               
                // Not the most efficient way to handle something like this,
                // but given the limited time this was supposed to take
                // the simplest way to handle this was to just user a series of 
                // if else statements, otherwise the replacement of "s" would affect
                // every card option as they all have the letter s in them.
                string printCard = Card;
                if (printCard.Contains("c"))
                {
                    printCard = printCard.Replace("c", "<span style='color: black;'>&clubs;</span>");
                }
                else if (printCard.Contains("d"))
                {
                    printCard = printCard.Replace("d", "<span style='color: red;'>&diams;</span>");
                }
                else if (printCard.Contains("h"))
                {
                    printCard = printCard.Replace("h", "<span style='color: red;'>&hearts;</span>");
                }
                else if (printCard.Contains("s"))
                {
                    printCard = printCard.Replace("s", "<span style='color: black;'>&spades;</span>");
                }

                printableHand.Append(separator + printCard);
                separator = "&nbsp;";
            }

            printableHand.AppendLine("</h4>");

            printableHand.Append("<h4>The rank of your hand was determined to be: ");

            // Just an attempt to give it some english sounding phrasing
            switch (PokerHandRank)
            {
                case HandRanks.RoyalFlush:
                    printableHand.Append("Royal Flush in the suit of " + pokerHandSuit.ToString() + "");
                    break;
                case HandRanks.StraightFlush:
                    printableHand.Append(pokerHandPrimaryCard.ToString() + "-high Straight Flush in the suit of " + pokerHandSuit.ToString() + "");
                    break;
                case HandRanks.FourOfaKind:
                    printableHand.Append("Four of a Kind " + pokerHandPrimaryCard.ToString() + "s");
                    break;
                case HandRanks.FullHouse:
                    printableHand.Append("Full House " + pokerHandPrimaryCard.ToString() + "s and " + pokerHandSecondaryCard.ToString() + "s");
                    break;
                case HandRanks.Flush:
                    printableHand.Append("Flush in the suit of " + pokerHandSuit.ToString() + "");
                    break;
                case HandRanks.Straight:
                    printableHand.Append(pokerHandPrimaryCard.ToString() + "-high Straight");
                    break;
                case HandRanks.ThreeOfaKind:
                    printableHand.Append("Three of a Kind " + pokerHandPrimaryCard.ToString() + "s");
                    break;
                case HandRanks.TwoPairs:
                    printableHand.Append("Two Pairs " + pokerHandPrimaryCard.ToString() + "s and " + pokerHandSecondaryCard.ToString() + "s");
                    break;
                case HandRanks.OnePair:
                    printableHand.Append("One Pair of " + pokerHandPrimaryCard.ToString() + "s");
                    break;
                case HandRanks.HighCard:
                    printableHand.Append(pokerHandPrimaryCard.ToString() + "-high Random Cards");
                    break;
            }

            printableHand.AppendLine("</h4>");

            return printableHand.ToString();
        }

        private void SetPokerHandCardsAndSuit()
        {
            // Convert the input array to a list of Cards that will be easier to work with later
            pokerHandCards = new List<Cards>();

            string cardNumber;
            string cardSuit;                       

            // Itterate through each value in the array and figure out what the card and suit are
            foreach (string Card in pokerHand)
            {
                // The assumption is that the array had values
                // like Ah or 2c for Ace of Hearts and Two of Clubs respectively
                // There should only be two or three character array values
                // Could probably use some error handling
                if (Card.Length == 3)
                {
                    // 10 should be the only option here
                    cardNumber = Card.Substring(0, 2).ToUpper();
                    cardSuit = Card.Substring(2, 1).ToLower();
                }
                else 
                {
                    if (Card.Length == 2)
                    {
                        cardNumber = Card.Substring(0, 1).ToUpper();
                        cardSuit = Card.Substring(1, 1).ToLower();
                    } 
                    else
                    {
                        // Should throw an error or catch it here
                        // but again it's a quick exercise so
                        // just assume it's an ace of spades to keep the 
                        // program running without an error
                        cardNumber = "A";
                        cardSuit = "s";
                    }

                }

                // Once the suit is set to mixed, there is no reason to keep checking
                // We are really interested in knowing if this hand is a flush of sorts for later
                if (pokerHandSuit != Suits.Mixed)
                {
                    switch (cardSuit)
                    {
                        case "c":
                            if (pokerHandSuit != Suits.Clubs && pokerHandSuit != Suits.Unknown)
                            {
                                pokerHandSuit = Suits.Mixed;
                            }
                            else
                            {
                                pokerHandSuit = Suits.Clubs;
                            }
                            break;
                        case "d":
                            if (pokerHandSuit != Suits.Diamonds && pokerHandSuit != Suits.Unknown)
                            {
                                pokerHandSuit = Suits.Mixed;
                            }
                            else
                            {
                                pokerHandSuit = Suits.Diamonds;
                            }
                            break;
                        case "h":
                            if (pokerHandSuit != Suits.Hearts && pokerHandSuit != Suits.Unknown)
                            {
                                pokerHandSuit = Suits.Mixed;
                            }
                            else
                            {
                                pokerHandSuit = Suits.Hearts;
                            }
                            break;
                        case "s":
                            if (pokerHandSuit != Suits.Spades && pokerHandSuit != Suits.Unknown)
                            {
                                pokerHandSuit = Suits.Mixed;
                            }
                            else
                            {
                                pokerHandSuit = Suits.Spades;
                            }
                            break;
                        default:
                            // Might make more sense to set to Unknown,
                            // but it would cause potentiall issues with the
                            // check before the switch statement
                            // Technically this is an error condition
                            pokerHandSuit = Suits.Mixed;
                            break;
                    }
                }

                // Check what card it is and add to the list
                switch (cardNumber)
                {
                    case "J":
                        pokerHandCards.Add(Cards.Jack);
                        break;
                    case "Q":
                        pokerHandCards.Add(Cards.Queen);
                        break;
                    case "K":
                        pokerHandCards.Add(Cards.King);
                        break;
                    case "A":
                        pokerHandCards.Add(Cards.Ace);
                        break;
                    default:
                        // Everything else should just be a number
                        // so we can just parse the value
                        // Once again, a good place for error handling
                        pokerHandCards.Add((Cards)Enum.Parse(typeof(Cards), cardNumber));
                        break;
                }

            }

            // Before exiting we want to sort the list
            pokerHandCards.Sort();
        }
        private void SetPokerHandRankAndValues()
        {
            // Since we have a sorted list, knowing the
            // endpoints should help determine the rank
            Cards lowCard = pokerHandCards.FirstOrDefault();
            Cards highCard = pokerHandCards.LastOrDefault();
            
            // The cardSpan calculation is probably not the best approach
            // but since we know the enum is in order, it can be
            // assured that the values are good for now
            int cardSpan = (int)highCard - (int)lowCard;
            
            // distinctCards will help with determining a number of things
            int distinctCards = pokerHandCards.Distinct().Count();

            // If there is only one suit (ie, the suits are not mixed)
            // then the worst case scenario is a Flush
            // so the check will start with the suit
            // Also note, it can't be anything else since there can't be
            // duplicates in the same suit, so it's not any of the other hands
            if (pokerHandSuit != Suits.Mixed)
            {
                // If there are no duplicate cards then the hand is
                // either a Flush, Straight Flush, or Royal Flush
                // If the difference between the highest and lowest card is five then
                // it must be a straight                
                if (cardSpan == 5 && distinctCards == 5)
                {
                    // If the high card is an Ace then it's a Royal Flush
                    // otherwise it's just a Stright Flush
                    if (highCard == Cards.Ace)
                    {
                        pokerHandRank = HandRanks.RoyalFlush;
                    }
                    else
                    {
                        pokerHandRank = HandRanks.StraightFlush;
                    }
                }
                else
                {
                    // If it's not a straight, then it's just a flush
                    // It can't be a Four of a Kind or Full House
                    // which would beat it otherwise
                    pokerHandRank = HandRanks.Flush;
                }

                // We'll set the primary card to the high card
                // and leave the secondary card alone
                pokerHandPrimaryCard = highCard;

                // No need to process further
                return;
            }

            // If the number of distinct cards is two,
            // then the hand is either four of a kind or a full house
            if (distinctCards == 2)
            {
                // We'll just kind of "brute force" this for now
                // If the second, third, and fourth cards in our sorted lists are all equal, then
                // it is Four of a Kind, otherwise it's a Full House
                if (pokerHandCards[1] == pokerHandCards[2] && pokerHandCards[2] == pokerHandCards[3])
                {
                    pokerHandRank = HandRanks.FourOfaKind;

                    // If the first two cards are equal then that is the Four of a Kind
                    // and the primary card is the low card, otherwise the primary card
                    // is the high card because the list is sorted
                    if (pokerHandCards[0] == pokerHandCards[1])
                    {
                        pokerHandPrimaryCard = lowCard;
                        pokerHandSecondaryCard = highCard;
                    }
                    else
                    {
                        pokerHandPrimaryCard = highCard;
                        pokerHandSecondaryCard = lowCard;
                    }
                }
                else
                {
                    pokerHandRank = HandRanks.FullHouse;

                    // If the second and third cards are equal then that is the Three of a Kind
                    // in the Full House, otherwise it's the last three cards
                    if (pokerHandCards[1] == pokerHandCards[2])
                    {
                        pokerHandPrimaryCard = pokerHandCards[1];
                        pokerHandSecondaryCard = highCard;
                    }
                    else
                    {
                        pokerHandPrimaryCard = pokerHandCards[3];
                        pokerHandSecondaryCard = lowCard;
                    }
                }
                
                // No need to process further
                return;
            }

            // If the card span is 5 then it's a straight
            if (cardSpan == 5 && distinctCards == 5)
            {
                pokerHandRank = HandRanks.Straight;
                pokerHandPrimaryCard = highCard;

                // No need to process further
                return;
            }

            // If there are three distinct cards it is either two pair or three of a kind
            if (distinctCards == 3)
            {
                // Similar to how Four of a Kind vs Full House was determined
                if (pokerHandCards[0] == pokerHandCards[1] && pokerHandCards[2] == pokerHandCards[3])
                {
                    pokerHandRank = HandRanks.TwoPairs;
                    pokerHandPrimaryCard = pokerHandCards[2];
                    pokerHandSecondaryCard = pokerHandCards[0];
                }
                else if (pokerHandCards[0] == pokerHandCards[1] && pokerHandCards[3] == pokerHandCards[4])
                {
                    pokerHandRank = HandRanks.TwoPairs;
                    pokerHandPrimaryCard = pokerHandCards[3];
                    pokerHandSecondaryCard = pokerHandCards[0];
                }
                else if (pokerHandCards[1] == pokerHandCards[2] && pokerHandCards[3] == pokerHandCards[4])
                {
                    pokerHandRank = HandRanks.TwoPairs;
                    pokerHandPrimaryCard = pokerHandCards[3];
                    pokerHandSecondaryCard = pokerHandCards[1];
                }
                else
                {
                    // It's Three of a Kind
                    pokerHandRank = HandRanks.ThreeOfaKind;
                    // The middle card is always part of the Three of a Kind
                    pokerHandPrimaryCard = pokerHandCards[2];
                }

                // No need to process further
                return;
            }

            // If there are four distinct cards it's One Pair
            if (distinctCards == 4)
            {
                pokerHandRank = HandRanks.OnePair;

                // Need to determine what the pair is now
                if (pokerHandCards[0] == pokerHandCards[1])
                {
                    pokerHandPrimaryCard = pokerHandCards[0];
                }
                else if (pokerHandCards[1] == pokerHandCards[2])
                {
                    pokerHandPrimaryCard = pokerHandCards[1];
                }
                else if (pokerHandCards[2] == pokerHandCards[3])
                {
                    pokerHandPrimaryCard = pokerHandCards[2];
                }
                else
                {
                    pokerHandPrimaryCard = pokerHandCards[3];
                }
                
                // No need to process further
                return;
            }

            // The only option left is that it's a nothing hand, which is called High Card here

            pokerHandRank = HandRanks.HighCard;
            pokerHandPrimaryCard = highCard;
        }
    }



    #region Enums
    public enum HandRanks
    {
        Unknown,
        HighCard,
        OnePair,
        TwoPairs,
        ThreeOfaKind,
        Straight,
        Flush,
        FullHouse,
        FourOfaKind,
        StraightFlush,
        RoyalFlush
    }

    public enum Cards
    {
        Unknown,
        LowAce,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public enum Suits
    {
        Unknown,
        Clubs,
        Diamonds,
        Hearts,
        Spades,
        Mixed,
    }
    #endregion Enums
}