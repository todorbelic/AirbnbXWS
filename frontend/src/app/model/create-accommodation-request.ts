import { AccommodationRequest } from "./accommodation-request";

export class CreateAccommodationRequest {
    request: AccommodationRequest = new AccommodationRequest

    public constructor(obj? : CreateAccommodationRequest){
        if(obj){
            this.request = obj.request
        }
    }
}
