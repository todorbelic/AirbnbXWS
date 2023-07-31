export class ReservationRequest{
    hostId = ''
    guestId = ''
    accommodationId = ''
    startDate = ''
    endDate = ''
    guestCount = ''

    public constructor(obj? : ReservationRequest){
        if(obj){
            this.hostId = obj.hostId
            this.guestId = obj.guestId
            this.accommodationId = obj.accommodationId
            this.startDate = obj.startDate
            this.endDate = obj.endDate
            this.guestCount = obj.guestCount
        }
    }
}