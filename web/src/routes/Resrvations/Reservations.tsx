import React, {useCallback, useEffect, useState} from 'react';
import {ReservationDto} from "../../api/models/Reservations/ReservationDto";
import {getReservationsByUserId} from "../../api/controllers/ReservationsClient";
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Title from "../../components/Common/Title";
import ReservationListItem from "../../components/Reservations/ReservationListItem";
import {Container, Paper, TableContainer} from "@mui/material";
import {SimpleReservationDto} from "../../api/models/Reservations/SimpleReservationDto";
import {useSnackbar} from "notistack";
import {userId} from "../../constants/User";


const Reservations = () => {
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [reservations, setReservations] = useState<SimpleReservationDto[]>([]);
    const { enqueueSnackbar } = useSnackbar();

    const fetchReservations = useCallback( () => {
        getReservationsByUserId(userId).then(reservations => {
            setReservations(reservations);
            setIsLoading(false);
        }).catch(() => {
            enqueueSnackbar("Something went wrong", { variant: 'error' });
            setIsLoading(false);
        });
    }, []);


    useEffect(() => {
        fetchReservations();
    }, [fetchReservations]);

    if (isLoading){
        return <div>loading...</div>
    }

    return (
        <Container maxWidth="xl" sx={{ mt: 4, mb: 4 }}>
            <TableContainer component={Paper} sx={{ p: 2, display: 'flex', flexDirection: 'column' }} >
                <Title>Your Reservations</Title>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Car</TableCell>
                            <TableCell>Start Date</TableCell>
                            <TableCell>End Date</TableCell>
                            <TableCell>Return Location</TableCell>
                            <TableCell>Status</TableCell>
                            <TableCell align="right"></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody >
                        {reservations.map((reservation) => (
                            <ReservationListItem key={reservation.id} reservation={reservation} />
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Container>
    );
};

export default Reservations;
