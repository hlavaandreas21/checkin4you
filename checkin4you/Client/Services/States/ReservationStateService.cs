namespace checkin4you.Client.Services.States
{
    public class ReservationStateService
    {
        public List<string>? Rooms { get; private set; }

        public async Task SetRooms(List<string> rooms)
        {
            Rooms = rooms;
        }
    }
}
