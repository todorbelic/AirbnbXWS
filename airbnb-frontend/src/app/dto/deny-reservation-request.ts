export class DenyReservationRequest{
   requestId = ''

   public constructor(obj? :DenyReservationRequest){
    if(obj) this.requestId = obj.requestId
   }
}