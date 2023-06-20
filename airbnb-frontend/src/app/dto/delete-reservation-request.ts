export class DeleteReservationRequestData{
    requestId = ''

    public constructor(obj? : DeleteReservationRequestData){
        if(obj) this.requestId = obj.requestId
    }
}