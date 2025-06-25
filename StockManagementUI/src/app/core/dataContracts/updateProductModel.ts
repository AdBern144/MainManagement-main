import { CreateProductModel } from "./createProductModel";

export interface UpdateProductModel extends CreateProductModel {
    id: number;
}