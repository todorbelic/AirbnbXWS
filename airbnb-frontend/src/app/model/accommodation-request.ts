import { Address } from "./address"

export class AccommodationRequest {
    hostId: string = ''
    name: string = ''
    address: Address = new Address
    benefits: string[] = []
    pictures: string[] = []
    minGuests: number = 0
    maxGuests: number = 0
    basePrice: number = 0
    paymentOption: any

    public constructor(obj? : AccommodationRequest){
        if(obj){
            this.hostId = obj.hostId
            this.name = obj.name
            this.address = obj.address
            this.benefits = obj.benefits
            this.pictures = obj.pictures
            this.minGuests = obj.minGuests
            this.maxGuests = obj.maxGuests
            this.basePrice = obj.basePrice
            this.paymentOption = obj.paymentOption
        }
    }
}
