import { ReservationRequest } from "./reservation-request";

export class SendReservationRequest{
    request : ReservationRequest = new ReservationRequest()
    
    public constructor(obj? : SendReservationRequest){
        if(obj) this.request = obj.request
    }
}