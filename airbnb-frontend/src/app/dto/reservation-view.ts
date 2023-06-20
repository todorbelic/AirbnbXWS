export class ReservationView {
    reservationId = ''
    hostName = ''
    guestName = ''
    accommodationName = ''
    address = ''
    startDate = ''
    endDate = ''
    guestCount = 0

    public constructor(obj? : ReservationView){
        if(obj){
            this.reservationId = obj.reservationId
            this.hostName = obj.hostName 
            this.guestName = obj.guestName
            this.accommodationName = obj.accommodationName
            this.address = obj.address
            this.startDate = obj.startDate
            this.endDate = obj.endDate
            this.guestCount = obj.guestCount
        }
    }
}