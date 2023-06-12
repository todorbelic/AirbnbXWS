import { Address } from "./address"

export class Accommodation {
    id : string = ''
    name: string = ''
    address : Address=new Address
    benefits: string[]=[]
    pictures: any[]=[]
    minGuests:number=0
    maxGuests:number=0
    price:number=0
    paymentOption:any


    public constructor(obj? : Accommodation){
        if(obj){
            this.id = obj.id
            this.name = obj.name
            this.address = obj.address
            this.benefits = obj.benefits
            this.pictures = obj.pictures
            this.minGuests = obj.minGuests
            this.maxGuests = obj.maxGuests
            this.price = obj.price
            this.paymentOption = obj.paymentOption
        }
    }
}
