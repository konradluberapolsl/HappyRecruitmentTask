export interface CreateReservationRequest {
    startDate: string;
    endDate: string;
    startLocationId: number;
    endLocationId: number;
    userId: number;
    carId: number;
}
