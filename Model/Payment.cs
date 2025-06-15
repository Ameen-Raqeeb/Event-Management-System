using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagmentSystem.Model
{
    // Represents a payment transaction in the system
    class Payment
    {
        // Unique identifier for the payment
        public int Id { get; set; }
        
        // Reference to the purchase this payment is for
        public int Purchase_id { get; set; }
        
        // Type of card used (e.g., Visa, MasterCard)
        public string CardType { get; set; }
        
        // Card number used for payment
        public string CardNumber { get; set; }
        
        // Name of the cardholder
        public string NameOnCard { get; set; }
        
        // Expiration date of the card
        public DateTime ExpiryDate { get; set; }
        
        // Card verification value
        public string Ccv { get; set; }

        // Constructor to create a new payment record
        public Payment(int purchaseId, string cardType, string cardNumber, string nameOnCard, DateTime expiryDate, string ccv)
        {
            this.Purchase_id = purchaseId;
            this.CardType = cardType;
            this.CardNumber = cardNumber;
            this.NameOnCard = nameOnCard;
            this.ExpiryDate = expiryDate;
            this.Ccv = ccv;
        }
    }
}
