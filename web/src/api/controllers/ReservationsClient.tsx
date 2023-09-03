import Client from "../Client";
import {ReservationDto} from "../models/Reservations/ReservationDto";
import {CreateReservationRequest} from "../models/Reservations/CreateReservationRequest";
import {SimpleReservationDto} from "../models/Reservations/SimpleReservationDto";

const controllerName = "Reservations";

const createReservation = async (request: CreateReservationRequest) : Promise<ReservationDto> => {
    return (await Client.post(`${controllerName}`, request)).data;
};

const getReservationById = async (reservationId: number) : Promise<ReservationDto> => {
    return (await Client.get(`${controllerName}/${reservationId}`)).data;
}

const getReservationsByUserId = async (userId: number) : Promise<SimpleReservationDto[]> => {
    return (await Client.get(`${controllerName}/user/${userId}`)).data;
}

const getUsersReservations = async () : Promise<SimpleReservationDto[]> => {
    return (await Client.get(`${controllerName}/user`)).data;
}

export { createReservation, getReservationById, getReservationsByUserId, getUsersReservations }
