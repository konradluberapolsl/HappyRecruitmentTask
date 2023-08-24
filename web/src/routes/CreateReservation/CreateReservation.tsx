import React from 'react';
import {Container, Paper, Typography} from "@mui/material";
import ReservationCreationForm from "../../components/ReservationCreationForm/ReservationCreationForm";

const CreateReservation = () => {
    return (
        <Container component={Paper} maxWidth="xl" sx={{ mt: 10, mb: 10, p:2 }}>
            <Typography variant="h4" sx={{mb: 2}}>Create reservation</Typography>
            <ReservationCreationForm/>
        </Container>

    );
};

export default CreateReservation;
