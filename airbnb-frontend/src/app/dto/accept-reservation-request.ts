export class AcceptReservationRequest {
    reservationId = ''
    public constructor(obj? : AcceptReservationRequest){
        if(obj) this.reservationId = obj.reservationId
    }
}