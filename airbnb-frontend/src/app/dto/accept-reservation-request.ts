export class AcceptReservationRequest {
    requestId = ''
    public constructor(obj? : AcceptReservationRequest){
        if(obj) this.requestId = obj.requestId
    }
}