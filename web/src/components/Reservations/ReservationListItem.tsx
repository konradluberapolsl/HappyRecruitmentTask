import React from 'react';
import {ReservationDto} from "../../api/models/Reservations/ReservationDto";
import TableRow from "@mui/material/TableRow";
import TableCell from "@mui/material/TableCell";
import {useNavigate} from "react-router-dom";
import {Button} from "@mui/material";
import {ReservationStatus} from "../../constants/Reservations";
import { format } from 'date-fns';
import {appDateFormat} from "../../constants/Dates";
import {SimpleReservationDto} from "../../api/models/Reservations/SimpleReservationDto";

interface ReservationListItemProps{
    reservation: SimpleReservationDto;
}


const ReservationListItem = ({reservation}: ReservationListItemProps) => {
    const navigate = useNavigate();

    return (
        <TableRow key={reservation.id}>
            <TableCell>{reservation.carModel}</TableCell>
            <TableCell>{format(new Date(reservation.startDate), appDateFormat)}</TableCell>
            <TableCell>{format(new Date(reservation.endDate), appDateFormat)}</TableCell>
            <TableCell>{reservation.endLocationName}</TableCell>
            <TableCell>{ReservationStatus[reservation.status]}</TableCell>
            <TableCell align="center">
                <Button variant="contained" onClick={() => navigate('/')}>Details</Button>
            </TableCell>
        </TableRow>
    );
};

export default ReservationListItem;
