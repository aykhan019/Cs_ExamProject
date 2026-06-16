using System;

namespace EntityNamespace
{
    public class Notification
    {
        public Notification(string message, string from, DateTime deliveryDate)
        {
            From = from;
            Message = message;
            DeliveryDate = deliveryDate;
        }
        public string Message { get; set; }
        public string From { get; set; }
        public DateTime DeliveryDate { get; set; }
        public override string ToString()
        {
            return $@"  From : {From}
  Delivery Date : {DeliveryDate.ToLongDateString()}
  Message : {Message}
";

        }
    }
}
