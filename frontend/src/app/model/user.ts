import { Address } from "./address"

export class User {
    id : string = ''
    firstName: string = ''
    lastName: string = ''
    username: string  = ''
    password: string = ''
    role : string = ''
    address: Address = new Address()

    public constructor(obj? : User){
        if(obj){
            this.id = obj.id
            this.firstName = obj.firstName
            this.lastName = obj.lastName
            this.username = obj.username
            this.password = obj.password
            this.address = obj.address
            this.role = obj.role
        }
    }
}