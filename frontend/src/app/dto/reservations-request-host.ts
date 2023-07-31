export class HostReservationRequest{
    hostId = ''
    public constructor(obj?: HostReservationRequest){
        if(obj) this.hostId = obj.hostId
    }
}