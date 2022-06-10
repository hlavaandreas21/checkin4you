namespace checkin4you.Client.Services.States
{
    public class ReservationStateService
    {
        public List<string>? Rooms { get; private set; } = new();
        public List<string>? CheckedInReservationIds { get; private set; } = new();

        public void SetCheckedInReservationIds(List<string> checkedInReservationIds)
        {
            if (checkedInReservationIds != null)
            {
                CheckedInReservationIds = checkedInReservationIds;
            }
        }

        public void AddCheckedInReservationId(string IdCheckedInReservation)
        {
            CheckedInReservationIds.Add(IdCheckedInReservation);
        }

        public void SetRooms(List<string> rooms)
        {
            Rooms = rooms;
        }
    }
}
