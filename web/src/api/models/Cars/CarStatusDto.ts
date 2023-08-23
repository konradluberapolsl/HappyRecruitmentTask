import {CarStatus} from "../../../constants/Cars";

export interface CarStatusDto {
    id: number;
    carId: number;
    status: CarStatus;
    fromDate: string;
    toDate: string | null;
}
