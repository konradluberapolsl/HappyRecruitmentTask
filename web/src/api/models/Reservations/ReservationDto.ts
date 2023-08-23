import {LocationDto} from "../Locations/LocationService";
import {UserDto} from "../Users/UserDto";
import {CarDto} from "../Cars/CarDto";
import {ReservationStatus} from "../../../constants/Reservations";

export interface ReservationDto {
    id: number;
    startDate: string;
    endDate: string;
    createdDate: string;
    status: ReservationStatus;
    startMileage: number;
    endMileage: number | null;
    totalCost: number | null;
    startLocation: LocationDto;
    endLocation: LocationDto;
    user: UserDto;
    car: CarDto;
}
