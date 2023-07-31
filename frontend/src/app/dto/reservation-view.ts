export class ReservationView {
    ReservationId = ''
    HostName = ''
    GuestName = ''
    AccommodationName = ''
    Address = ''
    startDate = ''
    endDate = ''
    GuestCount = 0

    public constructor(obj? : ReservationView){
        if(obj){
            this.ReservationId = obj.ReservationId
            this.HostName = obj.HostName 
            this.GuestName = obj.GuestName
            this.AccommodationName = obj.AccommodationName
            this.Address = obj.Address
            this.startDate = obj.startDate
            this.endDate = obj.endDate
            this.GuestCount = obj.GuestCount
        }
    }
}