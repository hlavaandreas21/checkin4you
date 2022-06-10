namespace checkin4you.Client.Services.States
{
    public class ReservationStateService
    {
        public List<string>? Rooms { get; private set; } = new();
        public List<string>? CheckedInReservationIds { get; private set; } = new();

        public async Task AddCheckedInReservationId(string IdCheckedInReservation)
        {
            CheckedInReservationIds.Add(IdCheckedInReservation);
        }

        public async Task SetRooms(List<string> rooms)
        {
            Rooms = rooms;
        }
    }
}
