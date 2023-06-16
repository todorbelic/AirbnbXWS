export class AccommodationSearchRequest {
    country: string  = ''
    city: string = ''
    streetAddress: string = ''
    numberOfGuests: number = 0
    startDate: string = ''
    endDate: string = ''

    public constructor(obj? : AccommodationSearchRequest){
        if(obj){
            this.country = obj.country
            this.city = obj.city
            this.streetAddress = obj.streetAddress
            this.numberOfGuests = obj.numberOfGuests
            this.endDate = obj.endDate
            this.startDate = obj.startDate
        }
    }
}