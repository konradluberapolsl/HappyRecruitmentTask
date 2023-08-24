import {CarModelDto} from "./CarModelDto";
import {CarStatusDto} from "./CarStatusDto";
import {CarLocationDto} from "./CarLocationDto";

export interface CarDto {
    id: number;
    vin: string;
    model: CarModelDto;
    mileage: number;
    productionDate: string;
    carStatusHistory: CarStatusDto[];
    carLocationHistory: CarLocationDto[];
}
