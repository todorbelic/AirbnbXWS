export class CancelReservationRequest {
    reservationId = ''
    public constructor(obj? : CancelReservationRequest){
        if(obj) this.reservationId = obj.reservationId
    }
}