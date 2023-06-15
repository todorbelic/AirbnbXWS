import { LogInRequestData } from "./logInRequestData";

export class LogInRequest {
    credentials: LogInRequestData = new LogInRequestData()

    public constructor(obj?: LogInRequest){
        if(obj){
            this.credentials = obj.credentials
        }
    }
}
