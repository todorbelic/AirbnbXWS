export class Address {
    country : string = ''
    city: string = ''
    streetAddress : string = ''

    public constructor(obj? : Address){
        if(obj){
            this.country = obj.country
            this.city = obj.city
            this.streetAddress = obj.streetAddress
        }
    }
}