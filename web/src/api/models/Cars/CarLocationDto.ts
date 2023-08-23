import {LocationDto} from "../Locations/LocationService";

export interface CarLocationDto {
    id: number;
    carId: number;
    location: LocationDto;
    fromDate: string;
    toDate: string | null;
}
