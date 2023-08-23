import React from 'react';
import {CarDto} from "../../api/models/Cars/CarDto";
import {Card, CardContent, CardMedia, Typography} from "@mui/material";
import {CarModelDto} from "../../api/models/Cars/CarModelDto";

interface CarInfoProps {
    car: CarModelDto;
}

const CarModelCard = ({ car }: CarInfoProps) => {
    return (
        <Card sx={{ maxWidth: 345 }}>
                <CardMedia
                    component="img"

                    image={car.thumbnail}
                    alt="green iguana"
                />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {car.manufacturer} {car.model}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Price per day: {car.costPerDay}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Price per week: {car.costPerWeek}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Price per month: {car.costPerMonth}
                    </Typography>
                </CardContent>
        </Card>
    );
};

export default CarModelCard;
