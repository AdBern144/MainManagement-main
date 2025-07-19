import { CreateCustomerModel } from "./createCustomerModel";

export interface UpdateCustomerModel extends CreateCustomerModel {
    id: number;
}