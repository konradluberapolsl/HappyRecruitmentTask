import {LocationDto} from "../Locations/LocationDto";

export interface CarLocationDto {
    id: number;
    carId: number;
    location: LocationDto;
    fromDate: string;
    toDate: string | null;
}
