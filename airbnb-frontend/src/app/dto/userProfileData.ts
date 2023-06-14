import { Address } from "../model/address"

export class UserProfileData {
    id = 0
    firstName = ''
    lastName = ''
    username  = ''
    password  = ''
    address: Address = new Address()

    public constructor(obj? : UserProfileData){
        if(obj){
            this.id = obj.id
            this.firstName = obj.firstName
            this.lastName = obj.lastName
            this.username = obj.username
            this.password = obj.password
            this.address = obj.address
        }
    }
}