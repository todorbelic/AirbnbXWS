export class ReservationView {
    ReservationId : string = ''
    HostName: string = ''
    GuestName : string = ''
    AccommodationName:string=''
    startDate:Date=new Date()
    endDate:Date=new Date()
    GuestCount:number=0
    Address:string=''
   // reservationStatus:any

    public constructor(obj? : ReservationView){
        if(obj){
            this.ReservationId = obj.ReservationId
            this.HostName = obj.HostName
            this.GuestName = obj.GuestName
            this.AccommodationName = obj.AccommodationName
            this.startDate = obj.startDate
            this.endDate = obj.endDate
            this.GuestCount = obj.GuestCount
            this.Address=obj.Address
            //this.reservationStatus = obj.reservationStatus
        }
    }
}

