import React from 'react';
import {Card, CardActions, CardContent, CardMedia, Checkbox, Typography} from "@mui/material";
import {CarDto} from "../../api/models/Cars/CarDto";

interface SelectableCarCardProps {
    car: CarDto;
    isSelected: boolean;
    onSelectChanged: () => void;
}

const SelectableCarCard = ({car, isSelected, onSelectChanged}: SelectableCarCardProps) => {
    return (
        <Card sx={{ maxWidth: 345 }}>
            <CardMedia
                component="img"
                image={car.model.thumbnail}
                alt="green iguana"
            />
            <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                    {car.model.manufacturer} {car.model.model}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    Mileage: {car.mileage}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    Price per day: {car.model.costPerDay}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    Price per week: {car.model.costPerWeek}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    Price per month: {car.model.costPerMonth}
                </Typography>
                <CardActions>
                    <Checkbox checked={isSelected} onChange={onSelectChanged} />
                </CardActions>
            </CardContent>
        </Card>
    );
};

export default SelectableCarCard;
