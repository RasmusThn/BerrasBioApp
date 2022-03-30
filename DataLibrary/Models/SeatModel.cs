namespace DataLibrary.Models
{
    /// <summary>
    /// Mostly used for Entity and seedData.
    /// 
    /// </summary>
    public class SeatModel
    {
        public int Id { get; set; }
        public int InternalSeatNumber { get; set; }
        public bool IsBooked { get; set; } = false;
        public int SaloonModelId { get; set; }
        public int BookingModelId { get; set; } = -1;

        public SeatModel()
        {

        }
        /// <summary>
        /// Sets the passed in values to the property with the same name
        /// </summary>
        /// <param name="interalSeatNumber"></param>
        /// <param name="saloonModelId"></param>
        public SeatModel(int interalSeatNumber, int saloonModelId)
        {
            InternalSeatNumber = interalSeatNumber;
            SaloonModelId = saloonModelId;
        }
    }
}