import { Address } from "./address"

export class UserProfileData {
    firstName: string = ''
    lastName: string = ''
    username: string  = ''
    password: string = ''
    address: Address = new Address()

    public constructor(obj? : UserProfileData){
        if(obj){
            this.firstName = obj.firstName
            this.lastName = obj.lastName
            this.username = obj.username
            this.password = obj.password
            this.address = obj.address
        }
    }
}