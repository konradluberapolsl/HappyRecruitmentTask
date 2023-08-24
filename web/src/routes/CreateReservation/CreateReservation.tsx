import React from 'react';
import {Container, Paper} from "@mui/material";
import ReservationCreationForm from "../../components/ReservationCreationForm/ReservationCreationForm";
import Title from "../../components/Common/Title";

const CreateReservation = () => {
    return (
        <Container component={Paper} maxWidth="xl" sx={{ mt: 10, mb: 10, p:2 }}>
            <Title>Create reservation</Title>
            <ReservationCreationForm/>
        </Container>

    );
};

export default CreateReservation;
