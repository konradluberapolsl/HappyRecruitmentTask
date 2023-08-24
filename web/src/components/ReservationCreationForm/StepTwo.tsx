import React, {useCallback, useEffect, useState} from "react";
import {Button, Grid, Stack, Typography} from "@mui/material";
import {CreateReservationRequest} from "../../api/models/Reservations/CreateReservationRequest";
import {getAvailableCarsByLocationAndTimeRange} from "../../api/controllers/AvailabilityClient";
import {CarDto} from "../../api/models/Cars/CarDto";
import SelectableCarCard from "../Cars/SelectableCarCard";

interface StepTwoProps {
    formData: CreateReservationRequest;
    setFormData: React.Dispatch<React.SetStateAction<CreateReservationRequest>>;
    onBack: () => void;
    onSubmit: () => void;
}

const StepTwo: React.FC<StepTwoProps> = ({ formData, setFormData, onBack, onSubmit }) => {
    const [availableCars, setAvailableCars] = useState<CarDto[]>([]);

    const [selectedCarId, setSelectedCarId] = useState<number | undefined>(formData.carId);

    const fetchAvailableCars = useCallback(() => {
        getAvailableCarsByLocationAndTimeRange(formData.startLocationId, formData.endDate, formData.startDate)
            .then((carsVm) =>{
            setAvailableCars(carsVm.cars);
        }).catch();
    }, []);

    useEffect(() => {
        fetchAvailableCars();
    }, [fetchAvailableCars]);


    const handleCardSelect = (cardIndex: number) => {
        setSelectedCarId(cardIndex === selectedCarId ? undefined : cardIndex);

        if (selectedCarId !== undefined){
            setFormData({ ...formData, carId: cardIndex})
        }
        else {
            setFormData({ ...formData, carId: 0})
        }
    };

    return (
        <Grid container
              direction="row"
              justifyContent="center"
              alignItems="center"
              spacing={5}>
            {availableCars.map((car) => (
                <Grid key={car.id} item xs={4}>
                    <SelectableCarCard
                        car={car}
                        isSelected={car.id == selectedCarId}
                        onSelectChanged={() => handleCardSelect(car.id)} />
                </Grid>
            ))}
            <Grid
                item
                xs={12}
                container
                direction="row"
                justifyContent="center"
                alignItems="center"
                spacing={5}
            >
                <Grid item>
                    <Button variant="contained" color="secondary" onClick={onBack}>
                        Back
                    </Button>
                </Grid>
                <Grid item>
                    <Button variant="contained" color="primary" onClick={onSubmit}>
                        Submit
                    </Button>
                </Grid>
            </Grid>
    </Grid>
    );
};

export default StepTwo;
