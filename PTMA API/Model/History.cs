namespace PTMA_API.Model
{
    public class History
    {
        public int Id { get; set; }
        public DateTime date {  get; set; }
        public int BusNumber { get; set; }
        public string BusName { get; set; }
        public History(int busNumber,string busName)
        {
            date = DateTime.Now;
            BusNumber = busNumber;
            BusName = busName;

            
        }
    }
}
