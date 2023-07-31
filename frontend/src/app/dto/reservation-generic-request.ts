export class GenericReservationRequest{
    requestId = ''

    public constructor(obj? : GenericReservationRequest){
        if(obj) this.requestId = obj.requestId
    }
}