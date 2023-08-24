import React, {useCallback, useEffect, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import {ReservationDto} from "../../api/models/Reservations/ReservationDto";
import {getReservationById} from "../../api/controllers/ReservationsClient";
import {ReservationStatus} from "../../constants/Reservations";
import CarModelCard from "../../components/Cars/CarModelCard";
import {Container, Grid, Paper, Typography} from "@mui/material";
import Title from "../../components/Common/Title";
import {format} from "date-fns";
import {appDateFormat} from "../../constants/Dates";
import {useSnackbar} from "notistack";
import PageLoader from "../PageLoader";
import {userId} from "../../constants/User";

const ReservationDetails = () => {
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [reservation, setReservation] = useState<ReservationDto | undefined> (undefined);

    const { reservationId } = useParams();
    const navigate = useNavigate();
    const { enqueueSnackbar } = useSnackbar();

    const fetchReservationDetails = useCallback(() => {
        if (reservationId){
            getReservationById(+reservationId).then((reservationDetails) => {
                setReservation(reservationDetails);
                setIsLoading(false);
            }).catch(() => {
                enqueueSnackbar("Something went wrong", { variant: 'error' });
                setIsLoading(false);
            });
        }
    }, []);


    useEffect(() => {
        fetchReservationDetails();
    }, [fetchReservationDetails]);


    if (isLoading){
        return <PageLoader/>
    }

    if (reservation == undefined || reservation.user.id != userId){
        navigate("/");
        return <div />;
    }

    let summary;

    if (reservation.status == ReservationStatus.Finished){
        summary =
            <>
                <Typography variant="h6">End mileage: {reservation?.endMileage ?? 0}</Typography>
                <Typography variant="h6">Total cost:  {reservation?.totalCost ?? 0}</Typography>
            </>;

    }
    else{
        summary = <Typography variant="h6">Summary will be calculated when the reservation ends</Typography>
    }

    return (
        <Container component={Paper} maxWidth="xl" sx={{ mt: 10, mb: 10, p:2 }}>
            <Title>Your rental details:</Title>
            <Grid
                  container
                  justifyContent="space-evenly"
                  spacing={3}
            >
                <Grid container
                      direction="column"
                      justifyContent="flex-start"
                      alignItems="flex-start"
                      item
                      xs={4}
                >
                    <Grid item xs={2}>
                        <Typography variant='h6'>Start date: {format(new Date(reservation.startDate), appDateFormat)}</Typography>
                    </Grid>
                    <Grid item xs={2}>
                        <Typography variant='h6'>End date: {format(new Date(reservation.endDate), appDateFormat)}</Typography>
                    </Grid>
                    <Grid item xs={2}>
                        <Typography variant='h6'>Status: <b>{(ReservationStatus[reservation.status]).toUpperCase()}</b></Typography>
                    </Grid>
                </Grid>
                    <Grid container
                          direction="column"
                          justifyContent="flex-start"
                          alignItems="flex-start"
                          item
                          xs={4}
                    >
                        <Grid item xs={2}>
                            <Typography variant='h6'>Start location: {reservation.startLocation.name}</Typography>
                        </Grid>
                        <Grid item xs={2}>
                            <Typography variant='h6'>End location:  {reservation.endLocation.name}</Typography>
                        </Grid>
                        <Grid item xs={2}>
                            <Typography variant='h6'>Summary:</Typography>
                            {summary}
                        </Grid>
                    </Grid>
                <Grid item xs={4}>
                    <Typography variant="h5" sx={{mb: 1}}>Car:</Typography>
                    <CarModelCard car={reservation.car.model} />
                </Grid>
            </Grid>
        </Container>
    );
};

export default ReservationDetails;
