import {ReservationStatus} from "../../../constants/Reservations";

export interface SimpleReservationDto {
    id: number;
    startDate: string;
    endDate: string;
    status: ReservationStatus;
    startLocationName: string;
    endLocationName: string;
    carModel: string;
}
