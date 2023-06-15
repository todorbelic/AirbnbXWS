import { Address } from "../model/address"

export class RegistrationRequestData {
    firstName: string = ''
    lastName: string = ''
    username: string  = ''
    password: string = ''
    role : string = ''
    address: Address = new Address()

    public constructor(obj? : RegistrationRequestData){
        if(obj){
            this.firstName = obj.firstName
            this.lastName = obj.lastName
            this.username = obj.username
            this.password = obj.password
            this.address = obj.address
            this.role = obj.role
        }
    }
}