export class DenyReservationRequest{
   reservationId = ''

   public constructor(obj? :DenyReservationRequest){
    if(obj) this.reservationId = obj.reservationId
   }
}