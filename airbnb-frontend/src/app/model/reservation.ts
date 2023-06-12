export class Reservation {
    id : string = ''
    hostId: string = ''
    guestId : string = ''
    accommodationId:string=''
    startDate:Date=new Date()
    endDate:Date=new Date()
    guestCount:number=0
    reservationStatus:any

    public constructor(obj? : Reservation){
        if(obj){
            this.id = obj.id
            this.hostId = obj.hostId
            this.guestId = obj.guestId
            this.accommodationId = obj.accommodationId
            this.startDate = obj.startDate
            this.endDate = obj.endDate
            this.guestCount = obj.guestCount
            this.reservationStatus = obj.reservationStatus
        }
    }
}
